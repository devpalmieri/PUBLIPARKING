using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    /// <summary>
    /// Context
    /// </summary>
    public partial class dbEnte
    {
#if DEBUG
        // Da usare in DEBUG per controllare se e quando vengono disposti i contesti.
        // Attualmente non lo vengono mai!
        // ...ma come si può fare a disporli nei Controller MVC DOPO che le viste sono state mostrate?
        // (purtroppo il contesto viene usato anche nelle viste, non si dovrebbe, ma è stato fatto...)
        // Risposta: si deve fare in "OnResultExecuted" da aggiungere a BaseController così:
        // protected override void OnResultExecuted(ResultExecutedContext filterContext)
        // {
        //     dbContext.Dispose();
        //     base.OnResultExecuted(filterContext);
        // }

        //public new void Dispose()
        //{
        //    base.Dispose();
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //}
#endif

        // Indica se il context deve essere in sola lettura. Utilizzato per incrementare le prestazioni di accesso al DB
        bool _isReadOnly = false;
        // ID Struttura utilizato nel salvataggio delle modifiche sul DB
        int _idStruttura;
        // ID Risorsa utilizato nel salvataggio delle modifiche sul DB
        int _idRisorsa;

        public int IdStruttura { get { return _idStruttura; } set { _idStruttura = value; } }
        public int IdRisorsa { get { return _idRisorsa; } set { _idRisorsa = value; } }

        private bool _bValidateEntities = true;
        public bool ValidateEntities { get { return _bValidateEntities; } set { _bValidateEntities = value; } }

        /// <summary>
        /// Lista dei contribuenti su cui il context può lavorare.
        /// Se null, lavora su tutti, altrimenti mostra i dati dei soli contribuenti indicati.
        /// </summary>
        public List<Decimal> idContribuenteDefaultList { get; set; }

        // Contiene le string con il comando UPDATE del FLAG_ON_OFF per tutte le entiy che implementano la SoftDelete
        private static Dictionary<Type, string> _updateCache = new Dictionary<Type, string>();
        // Mappatura tra le entity del DB e i Tipi Classe
        private static Dictionary<Type, EntitySetBase> _mappingCache = new Dictionary<Type, EntitySetBase>();
        // Nome del campo FLAG_ON_OFF
        private static String FLAG_ON_OFF = "flag_on_off";

        static ILogger _logger = null;
        public static ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = LoggerFactory.getInstance().getLogger<NLogger>(nameof(dbEnte));
                }
                return _logger;
            }
        }

        /// <summary>
        /// Consente la creazione di un nuovo context partendo da una stringa di connessione.
        /// Se la risorse e la struttura di Default vengono lasciate a 0, La loro gestione sarà manuale.
        /// </summary>
        /// <param name="p_connectingString">Stringa di connessione che il Context dovrà utilizzare</param>
        /// <param name="p_idStruttura">Struttura di default da utilizzare nella gestione STATO dei record</param>
        /// <param name="p_idRisorsa">Risorsa di default da utilizzare nella gestione STATO dei record</param>
        /// <param name="p_isReadOnly">Se TRUE il context verrà creato in sola lettura per incrementare le performance</param>
        /// <param name="p_idContribuenteDefaultList">Se diverso da NULL, contiene l'elenco dei contribuenti su cui il context può lavorare</param>
        public dbEnte(string p_connectingString, int p_idStruttura, int p_idRisorsa, bool p_isReadOnly, List<Decimal> p_idContribuenteDefaultList, bool bValidateEntities = true)
            : base(p_connectingString)
        {
            //Salva i valori di default
            _idStruttura = p_idStruttura;
            _idRisorsa = p_idRisorsa;
            _isReadOnly = p_isReadOnly;
            _bValidateEntities = bValidateEntities;

            if (p_idContribuenteDefaultList == null)
            {
                this.idContribuenteDefaultList = new List<Decimal>();
            }
            else
            {
                this.idContribuenteDefaultList = p_idContribuenteDefaultList;
            }

            //Se sola lettura, disabilita il tracciamento delle modifiche per aumentare le performance
            if (p_isReadOnly)
            {
                base.Configuration.AutoDetectChangesEnabled = false;
            }
        }

        public void SetUserStatus()
        {
            List<DbEntityEntry> v_invalidList = getListOfInvalid();
            SetUserStatus(v_invalidList);
        }

        public void SetUserStatus(List<DbEntityEntry> v_invalidList)
        {
            if (v_invalidList.Count == 0)
            {
                //Aggiorna i campi per saper chi ha apportato le modifiche alle entità (Solo se sono stati indicati risorsa e struttura di default)
                if (_idStruttura > 0 && _idRisorsa > 0)
                {
                    foreach (var v_entry in ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified)
                                          .Where(r => r.Entity.GetType().GetInterface(typeof(IGestioneStato).FullName, true) != null))
                    {
                        ((IGestioneStato)v_entry.Entity).SetUserStato(_idStruttura, _idRisorsa);
                    }
                }
                else
                {
                    Trace.TraceInformation("ATTENZIONE!: _idRisorsa={0}, _idStruttura={1}", _idRisorsa, _idStruttura);
                    Logger.LogMessage($"ATTENZIONE!: _idRisorsa={_idRisorsa}, _idStruttura={_idStruttura}", EnLogSeverity.Warn);
                }
            }
        }

        private readonly static List<DbEntityEntry> EmptyListOfInvalid = new List<DbEntityEntry>();

        public override int SaveChanges()
        {
            return SaveChanges(false);
        }
        public override async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync(false);
        }

        /// <summary>
        /// override del SaveChange() di default. Il metodo controlla se l'entità implementa l'interfaccia ISoftDeleted, in
        /// caso positivo, quando viene effettuata una cancellazione viene invocato il metodo Softdelete 
        /// </summary>
        /// <param name="forceIgnoreInvalidList">Forza a non effettuare la validazione delle entità non valide anche se la proprietà ValidateEntities è true</param>
        /// <returns>Restituisce il numero di entity salvate</returns>
        public int SaveChanges(bool forceIgnoreInvalidList)
        {
            try
            {
                //Se di sola lettura, non procede con il salvataggio
                if (_isReadOnly)
                    return 0;

                ////Prima di procedere verifica che tutte le entity siano valide
                //List<DbEntityEntry> v_invalidList = forceIgnoreInvalidList ? EmptyListOfInvalid : getListOfInvalid();
                //if (v_invalidList.Count == 0)
                //{
                //    SetUserStatus(v_invalidList);
                //
                //    //Gestione della cancellazione logica per le entity che la implementano
                //    int v_count = 0;
                //    foreach (var v_entry in ChangeTracker.Entries()
                //              .Where(p => p.State == EntityState.Deleted)
                //              .Where(r => r.Entity.GetType().GetInterface(typeof(ISoftDeleted).FullName, true) != null))
                //    {
                //        SoftDelete(v_entry);
                //        v_count++;
                //    }
                //    return base.SaveChanges() + v_count;
                //}
                //else
                //{
                //    throw new ArgumentException("not valid entities", string.Empty);
                //}
                // Prima di procedere verifica che tutte le entity siano valide
                getListOfInvalidOrThrow(forceIgnoreInvalidList);
                int v_count = SetStatus(EmptyListOfInvalid);
                return base.SaveChanges() + v_count;
            }
            catch (DbEntityValidationException dbEx)
            {
                OnDbEntityValidationException(dbEx);

                throw;
            }
        }

        /// <summary>
        /// override del SaveChange() di default. Il metodo controlla se l'entità implementa l'interfaccia ISoftDeleted, in
        /// caso positivo, quando viene effettuata una cancellazione viene invocato il metodo Softdelete 
        /// </summary>
        /// <param name="forceIgnoreInvalidList">Forza a non effettuare la validazione delle entità non valide anche se la proprietà ValidateEntities è true</param>
        /// <returns>Restituisce il numero di entity salvate</returns>
        public async Task<int> SaveChangesAsync(bool forceIgnoreInvalidList)
        {
            //Se di sola lettura, non procede con il salvataggio
            if (_isReadOnly)
                return 0;

            try
            {
                // Prima di procedere verifica che tutte le entity siano valide
                getListOfInvalidOrThrow(forceIgnoreInvalidList);
                int v_count = SetStatus(EmptyListOfInvalid);
                return await base.SaveChangesAsync() + v_count;
            }
            catch (DbEntityValidationException dbEx)
            {
                OnDbEntityValidationException(dbEx);

                throw;
            }
        }

        private void OnDbEntityValidationException(DbEntityValidationException dbEx)
        {
            foreach (var v_validationErrors in dbEx.EntityValidationErrors)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Entity validation errors");
                foreach (var v_validationError in v_validationErrors.ValidationErrors)
                {
                    string msg = $"---> Class: {v_validationErrors.Entry.Entity.GetType().FullName}, ---> Property: {v_validationError.PropertyName}, Error: {v_validationError.ErrorMessage}";
                    Trace.TraceError(msg);
                    sb.AppendLine(msg);
                }
                Logger.LogMessage(sb.ToString(), EnLogSeverity.Error);
            }
        }

        public static void LogDbCtxEntityValidationException(Exception ex, ILogger logger)
        {
            DbEntityValidationException eve = ex as DbEntityValidationException;
            if (eve != null)
            {
                logger.LogMessage("Entity validation exceptions: ", EnLogSeverity.Error);

                var vaErrs = eve.EntityValidationErrors.Select(x =>
                        x.ValidationErrors.Select(v => $"{x.Entry.Entity.GetType().FullName}: {v.PropertyName} - {v.ErrorMessage}"))
                    .ToList();
                foreach (var lvErr in vaErrs)
                {
                    foreach (var err in lvErr)
                    {
                        logger.LogMessage(err, EnLogSeverity.Error);
                    }
                }
            }
        }

        private int SetStatus(List<DbEntityEntry> v_invalidList)
        {
            //Aggiorna i campi per saper chi ha apportato le modifiche alle entità (Solo se sono stati indicati risorsa e struttura di default)
            //if (_idStruttura > 0 && _idRisorsa > 0)
            //{
            //    foreach (var v_entry in ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified)
            //                          .Where(r => r.Entity.GetType().GetInterface(typeof(IGestioneStato).FullName, true) != null))
            //    {
            //        ((IGestioneStato)v_entry.Entity).SetUserStato(_idStruttura, _idRisorsa);
            //    }
            //}
            SetUserStatus(v_invalidList);

            //Gestione della cancellazione logica per le entity che la implementano
            int v_count = 0;
            foreach (var v_entry in ChangeTracker.Entries()
                      .Where(p => p.State == EntityState.Deleted)
                      .Where(r => r.Entity.GetType().GetInterface(typeof(ISoftDeleted).FullName, true) != null))
            {
                SoftDelete(v_entry);
                v_count++;
            }
            return v_count;
        }

        /// <summary>
        /// implementa la Soft Delete. Effettua l'update del campo flag_on_off settandolo a 0
        /// </summary>
        /// <param name="p_entry"></param>       
        private void SoftDelete(DbEntityEntry p_entry)
        {
            //Costruisce il comando di UPDATE per il tipo di entity dato.
            //Le stringhe, una volta create, vengono mantenute in cache per motivi di performance
            Type v_entryEntityType = p_entry.Entity.GetType();
            string v_sql = string.Empty;
            if (!_updateCache.ContainsKey(v_entryEntityType))
            {
                IList<string> keyList = GetPrimaryKeyNames(v_entryEntityType);
                string tableName = GetTableName(v_entryEntityType);
                int keyListCount = keyList.Count();
                if (keyListCount == 1)
                {
                    // v_sql = string.Format("UPDATE {0} SET " + FLAG_ON_OFF + " = 0 WHERE {1} = @id", GetTableName(v_entryEntityType), GetPrimaryKeyName(v_entryEntityType));
                    v_sql = $"UPDATE {tableName} SET {FLAG_ON_OFF} = 0 WHERE {keyList[0]} = @id";
                    _updateCache.Add(v_entryEntityType, v_sql);
                }
                else if (keyListCount > 1)
                {
                    // N.B.: in caso di più chiavi primarie NON conserviamo in cache la query!
                    // Inoltre imposto v_sql a null in modo che non esegue la query di update che vale solo per una chiave primaria
                    // NOTA: si può generalizzare la query per effetuare il soft delete su più chiavi primarie,
                    //       ma è meglio non farlo per motivi di efficienza (non puoi più usare
                    //       _updateCache come ora)
                    v_sql = null;

                    if (tableName == nameof(Data.join_ente_ente_gestito))
                    {
                        var idEnte = p_entry.OriginalValues[nameof(Data.join_ente_ente_gestito.id_ente)];
                        var idEnteGestito = p_entry.OriginalValues[nameof(Data.join_ente_ente_gestito.id_ente_gestito)];
                        string l_sql = $"UPDATE {tableName} SET {FLAG_ON_OFF} = 0 WHERE {nameof(Data.join_ente_ente_gestito.id_ente)} = @idEnte AND {nameof(Data.join_ente_ente_gestito.id_ente_gestito)} = @idEnteGestito";

                        Database.ExecuteSqlCommand(l_sql, new SqlParameter("@idEnte", idEnte), new SqlParameter("@idEnteGestito", idEnteGestito));
                    }
                    else
                    {
                        throw new Exception("Impossibile cancellare il record: più chiavi primarie presenti!");
                    }
                }
                else
                {
                    throw new Exception("Nessuna chiave primaria nella tabella " + tableName);
                }
            }
            else
            {
                _updateCache.TryGetValue(v_entryEntityType, out v_sql);
            }

            if (v_sql != null)
            {
                //Esegue il comando di UPDATE per l'entity
                Database.ExecuteSqlCommand(v_sql, new SqlParameter("@id", p_entry.OriginalValues[GetPrimaryKeyName(v_entryEntityType)]));
            }

            // Previene la cancellazione fisica del dato            
            p_entry.State = EntityState.Detached;
        }


        /// <summary>
        /// Restituisce il nome della tabella che contiene l'entity
        /// </summary>
        /// <param name="v_type"></param>
        /// <returns></returns>
        private string GetTableName(Type v_type)
        {
            EntitySetBase v_entity = GetEntitySet(v_type);
            return v_entity.Name.ToString();
        }

        /// <summary>
        /// Restituisce la chiave primaria dell'entity
        /// </summary>
        /// <param name="v_type"></param>
        /// <returns></returns>
        public string GetPrimaryKeyName(Type v_type)
        {
            EntitySetBase v_entity = GetEntitySet(v_type);
            return v_entity.ElementType.KeyMembers[0].Name;
        }

        /// <summary>
        /// Restituisce le chiavi primarie dell'entity
        /// </summary>
        /// <param name="v_type"></param>
        /// <returns></returns>
        public IList<string> GetPrimaryKeyNames(Type v_type)
        {
            EntitySetBase v_entity = GetEntitySet(v_type);
            return v_entity.ElementType.KeyMembers.Select(x => x.Name).ToList();
        }

        /// <summary>
        /// Restituisce l'entity di base al quale appartiene il tipo
        /// </summary>
        /// <param name="v_type">Tipo del quale si vuole avere l'entity</param>
        /// <returns></returns>
        private EntitySetBase GetEntitySet(Type v_type)
        {

            if (!_mappingCache.ContainsKey(v_type))
            {
                ObjectContext v_octx = ((IObjectContextAdapter)this).ObjectContext;

                string v_typeName = ObjectContext.GetObjectType(v_type).Name;
                EntitySetBase v_baseEntity = v_octx.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)
                                                    .GetItems<EntityContainer>()
                                                    .SelectMany(c => c.BaseEntitySets
                                                    .Where(e => e.Name == v_typeName))
                                                    .FirstOrDefault();

                if (v_baseEntity == null)
                    throw new ArgumentException("Entity type not found in GetTableName", v_typeName);

                _mappingCache.Add(v_type, v_baseEntity);
            }

            return _mappingCache[v_type];
        }

        /// <summary>
        /// Restituisce la lista delle entità non valide all'interno del context
        /// </summary>
        /// <returns></returns>
        public List<DbEntityEntry> getListOfInvalid()
        {
            // --- Se _bValidateEntities==false non valida! ---
            List<DbEntityEntry> v_invalidList = _bValidateEntities ?
                    (from a in this.ChangeTracker.Entries().Where(a => a.State == System.Data.Entity.EntityState.Modified || a.State == System.Data.Entity.EntityState.Added)
                     where (a.Entity is IValidator && !(a.Entity as IValidator).IsValid)
                     select a).ToList()
                     : EmptyListOfInvalid;
            return v_invalidList;
        }

        public List<DbEntityEntry> getListOfInvalidOrThrow(bool forceIgnoreInvalidList)
        {
            List<DbEntityEntry> v_invalidList = forceIgnoreInvalidList ? EmptyListOfInvalid : getListOfInvalid();
            if (v_invalidList.Count > 0)
            {
                // N.B.: il savechanges se la lista non è vuota lancia eccezione generica, ma
                //       sarebbe meglio creare una eccezione "custom"
                throw new Exception("not valid entities");
                //SE TI CAPITA DI FINIRE QUA E NON CAPIRE QUALI SIANO I CAMPI INVALIDI DELL'ENTITA' CHE STAI MODIFICANDO
                //UTILIZZA dbContext.SaveChanges(true) e finirà nel catch (DbEntityValidationException dbEx) del SaveChanges
                //Oppure controllare il metodo public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
                //dell'entità che si sta salvando
            }

            return v_invalidList;
        }

        public IList<DbEntityEntry> GetModifiedEntityList()
        {
            IList<DbEntityEntry> modifiedChanges = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();

            return modifiedChanges;
        }

        public IList<DbEntityEntry> GetAddedEntityList()
        {
            IList<DbEntityEntry> modifiedChanges = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();

            return modifiedChanges;
        }

        public class EntityInspectDBG
        {
            public string EntityName { get; set; }
            public string PrimaryKeys { get; set; }
            public class FieldInfo
            {
                public string FieldName { get; set; }
                public string OriginalValue { get; set; }
                public string CurrValue { get; set; }
            }
            public IList<FieldInfo> Fields { get; set; }
        }
        public IList<EntityInspectDBG> InspectModifiedEntityListDBG()
        {
            return InspectAddedOrModifiedEntityListDBG(false);
        }
        public IList<EntityInspectDBG> InspectAddedEntityListDBG()
        {
            return InspectAddedOrModifiedEntityListDBG(true);
        }
        private IList<EntityInspectDBG> InspectAddedOrModifiedEntityListDBG(bool bAdded)
        {
            IList<EntityInspectDBG> retList = new List<EntityInspectDBG>();

            IList<DbEntityEntry> modifiedEntities = bAdded ? GetAddedEntityList() : GetModifiedEntityList();
            foreach (var change in modifiedEntities)
            {
                EntityInspectDBG inspc = new EntityInspectDBG();

                inspc.EntityName = change.Entity.GetType().Name;

                string pkVals = null;

                // TODO: mettere in funzione a se (GetPrimaryKeysValue)
                {
                    var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(change.Entity);
                    var pks = objectStateEntry.EntityKey.EntityKeyValues;
                    if (pks.Length == 1)
                    {
                        pkVals = pks[0].Value?.ToString();
                    }
                    else if (pks.Length > 1)
                    {
                        StringBuilder sb = new StringBuilder();
                        int iMax = pks.Length;
                        for (int i = 0; i < iMax - 1; ++i)
                        {
                            sb.Append(pks[i]?.ToString() ?? "<NULL>");
                            sb.Append(",");
                        }
                        sb.Append(pks[iMax]?.ToString() ?? "<NULL>");
                    }
                }
                pkVals = pkVals ?? "<NULL>";
                inspc.PrimaryKeys = pkVals;

                inspc.Fields = new List<EntityInspectDBG.FieldInfo>();
                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var nfo = new EntityInspectDBG.FieldInfo
                    {
                        FieldName = prop,
                        OriginalValue = change.OriginalValues[prop].ToString(),
                        CurrValue = change.CurrentValues[prop].ToString()
                    };
                    inspc.Fields.Add(nfo);
                }

                retList.Add(inspc);
            }
            return retList;
        }
    }
}
