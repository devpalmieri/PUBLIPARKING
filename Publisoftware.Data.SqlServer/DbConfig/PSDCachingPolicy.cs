# if false
using EFCache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.DbConfig
{
    /// <summary>
    /// Classe che incapsula PSDCachingPolicy - evita di aggiungere un riferimento all'assembly "EFCache.dll"
    /// </summary>
    public class EFCachingPolicy
    {
        readonly PSDCachingPolicy _cachePolicy;
        public PSDCachingPolicy CachePolicy { get { return _cachePolicy; } }

        /// <summary>
        /// Custom 2nd level cache policy
        /// </summary>
        /// <param name="logCached">Log a msg to inform that a query will be cached</param>
        /// <param name="includedEntities">Mandatory: array of entities to be cached in the 2nd level cache</param>
        /// <example>
        /// var psdCahePolicy = new EFCachingPolicy(true, new string[] {
        ///        nameof(tab_parametri_portale),
        ///        nameof(tab_esecuzione_applicazioni),
        ///        nameof(anagrafica_tipo_avv_pag),
        ///        nameof(tab_modalita_rate_avvpag),
        ///        nameof(tab_tipo_voce_contribuzione),
        ///        nameof(anagrafica_voci_contribuzione)
        ///        //, (...)
        ///    });
        /// </example>
        public EFCachingPolicy(bool logCached, string[] includedEntities)
        {
            _cachePolicy = new PSDCachingPolicy(logCached, includedEntities);
        }

        public void SetCacheableEntities(string[] includedEntities)
        {
            _cachePolicy.SetCachedEntities(includedEntities);
        }
    }

    public class PSDCachingPolicy : CachingPolicy
    {
        private string[] _includedEntities;
        readonly Action<string> _logCb;

        /// <summary>
        /// Custom 2nd level cache policy
        /// </summary>
        /// <param name="logCached">Log a msg to inform that a query will be cached</param>
        /// <param name="includedEntities">Mandatory: array of entities to be cached in the 2nd level cache</param>
        /// <example>
        /// var cahePolicy = new PSDCachingPolicy(true, new string[] {
        ///        nameof(tab_parametri_portale),
        ///        nameof(tab_esecuzione_applicazioni),
        ///        nameof(anagrafica_tipo_avv_pag),
        ///        nameof(tab_modalita_rate_avvpag),
        ///        nameof(tab_tipo_voce_contribuzione),
        ///        nameof(anagrafica_voci_contribuzione)
        ///        //, (...)
        ///    });
        /// </example>
        internal PSDCachingPolicy(bool logCached, string[] includedEntities) : base()
        {
            if (includedEntities == null)
            {
                throw new ArgumentException(nameof(includedEntities));
            }
            _includedEntities = includedEntities;

            if (logCached)
            {
                // TODO: levare "intreccio" con "PSDDBEFInterceptor" e aggiungere un ILog a questa classe....
                _logCb = PSDDBEFInterceptor.WriteLogNCB;
            }
            else
            {
                _logCb = _ => { };
            }
        }

        public void SetCachedEntities(string[] includedEntities)
        {
            if (includedEntities == null)
            {
                throw new ArgumentException(nameof(includedEntities));
            }
            _includedEntities = includedEntities.ToArray();
        }

        protected override bool CanBeCached(System.Collections.ObjectModel.ReadOnlyCollection<System.Data.Entity.Core.Metadata.Edm.EntitySetBase> affectedEntitySets, string sql, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (affectedEntitySets.Where(x => _includedEntities.Contains(x.Name)).Any())
            {
                _logCb("Query can be cached");
                return true;
            }
            else
            {
#if USE_DEFAULT_CACHING_POLICY
                bToBeCached = base.CanBeCached(affectedEntitySets, sql, parameters);
#else
                _logCb("Query must not be cached");
                return false;
#endif
            }
        }
    }
}
#endif
