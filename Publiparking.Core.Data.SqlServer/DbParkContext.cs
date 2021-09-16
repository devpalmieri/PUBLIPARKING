using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer
{
    public static class DbConnectionManager
    {
        public static List<DbConnection> GetAllConnections()
        {
            List<DbConnection> result;
            using (StreamReader r = new StreamReader("myjsonfile.json"))
            {
                string json = r.ReadToEnd();
                result = DbConnection.FromJson(json);
            }
            return result;
        }

        public static string GetConnectionString(string dbName)
        {
            return GetAllConnections().FirstOrDefault(c => c.Name == dbName)?.Dbconnection;
        }
    }
    public class DbConnection
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dbconnection")]
        public string Dbconnection { get; set; }

        public static List<DbConnection> FromJson(string json) => JsonConvert.DeserializeObject<List<DbConnection>>(json, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }


    public static class DbManager
    {

        public static string DbName;

        public static string GetDbConnectionString(string dbName)
        {
            return DbConnectionManager.GetConnectionString(dbName);
        }
    }
    public partial class DbParkContext : DbContext
    {
        public DbParkContext(DbContextOptions<DbParkContext> options)
            : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                var dbConnectionString = _connectionstring;
                optionsBuilder.UseSqlServer(dbConnectionString);
            }
        }
        public virtual DbSet<Causali> Causali { get; set; }
        public virtual DbSet<anagrafica_ente> anagrafica_ente { get; set; }
        public virtual DbSet<anagrafica_pagopa_intermediari> anagrafica_pagopa_intermediari { get; set; }
        public virtual DbSet<anagrafica_pagopa_stazioni> anagrafica_pagopa_stazioni { get; set; }
        public virtual DbSet<anagrafica_risorse> anagrafica_risorse { get; set; }
        public virtual DbSet<anagrafica_ruolo_mansione> anagrafica_ruolo_mansione { get; set; }
        public virtual DbSet<anagrafica_stato_carrello> anagrafica_stato_carrello { get; set; }
        public virtual DbSet<anagrafica_strutture_aziendali> anagrafica_strutture_aziendali { get; set; }
        public virtual DbSet<join_applicazioni_secondarie> join_applicazioni_secondarie { get; set; }
        public virtual DbSet<join_utenti_enti> join_utenti_enti { get; set; }
        public virtual DbSet<ser_comuni> ser_comuni { get; set; }
        public virtual DbSet<ser_province> ser_province { get; set; }
        public virtual DbSet<ser_regioni> ser_regioni { get; set; }
        public virtual DbSet<tab_abilitazione> tab_abilitazione { get; set; }
        public virtual DbSet<tab_cc_riscossione> tab_cc_riscossione { get; set; }
        public virtual DbSet<tab_domande_segrete> tab_domande_segrete { get; set; }
        public virtual DbSet<tab_funzionalita> tab_funzionalita { get; set; }
        public virtual DbSet<tab_applicazioni> tab_applicazioni { get; set; }
        public virtual DbSet<tab_idpspid> tab_idpspid { get; set; }
        public virtual DbSet<tab_pagine> tab_pagine { get; set; }
        public virtual DbSet<tab_procedure> tab_procedure { get; set; }
        public virtual DbSet<tab_registro_spid> tab_registro_spid { get; set; }
        public virtual DbSet<tab_tassonomia_pagopa> tab_tassonomia_pagopa { get; set; }
        public virtual DbSet<tab_tipo_ente> tab_tipo_ente { get; set; }
        public virtual DbSet<tab_toponimi> tab_toponimi { get; set; }
        public virtual DbSet<tab_utenti> tab_utenti { get; set; }
        public virtual DbSet<ser_stati_esteri> ser_stati_esteri { get; set; }
        public virtual DbSet<ser_stati_esteri_new> ser_stati_esteri_new { get; set; }
        public virtual DbSet<Operatori> Operatori { get; set; }
        public virtual DbSet<tab_ambiente> tab_ambiente { get; set; }
        public virtual DbSet<tab_path_portale> tab_path_portale { get; set; }

        public virtual DbSet<tab_abilitazione_menu> tab_abilitazione_menu { get; set; }
        public virtual DbSet<tab_menu_primo_livello> tab_menu_primo_livello { get; set; }
        public virtual DbSet<tab_menu_secondo_livello> tab_menu_secondo_livello { get; set; }
        public virtual DbSet<tab_menu_terzo_livello> tab_menu_terzo_livello { get; set; }
        public virtual DbSet<Abbonamenti> Abbonamenti { get; set; }
        public virtual DbSet<AbbonamentiPeriodici> AbbonamentiPeriodici { get; set; }
        public virtual DbSet<AbbonamentiRinnovi> AbbonamentiRinnovi { get; set; }

        public virtual DbSet<FasceTariffeAbbonamenti> FasceTariffeAbbonamenti { get; set; }
        public virtual DbSet<Tariffe> Tariffe { get; set; }
        public virtual DbSet<TariffeAbbonamenti> TariffeAbbonamenti { get; set; }
        public virtual DbSet<TariffeFlat> TariffeFlat { get; set; }
        public virtual DbSet<TitoliSMS> TitoliSMS { get; set; }
        public virtual DbSet<TitoliSMSTarga> TitoliSMSTarga { get; set; }
        public virtual DbSet<translog> translog { get; set; }
        public virtual DbSet<translog_pyng> translog_pyng { get; set; }

        public virtual DbSet<Cellulari> Cellulari { get; set; }
        public virtual DbSet<RicaricheConfermate> RicaricheConfermate { get; set; }
        public virtual DbSet<SMSIn> SMSIn { get; set; }
        public virtual DbSet<SMSOut> SMSOut { get; set; }
        public virtual DbSet<Stalli> Stalli { get; set; }
        public virtual DbSet<tab_registro_cookie> tab_registro_cookie { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Operatori>(entity =>
            {
                entity.Property(e => e.cognome).IsUnicode(false);

                entity.Property(e => e.dataCambioPassword).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.frequenzaFoto).HasDefaultValueSql("((10))");

                entity.Property(e => e.matricola).IsUnicode(false);

                entity.Property(e => e.noGpsOperazioni).HasDefaultValueSql("((-1))");

                entity.Property(e => e.nome).IsUnicode(false);

                entity.Property(e => e.password)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'098f6bcd4621d373cade4e832627b4f6')");

                entity.Property(e => e.username).IsUnicode(false);
            });

            modelBuilder.Entity<Causali>(entity =>
            {
                entity.Property(e => e.articolo).IsUnicode(false);

                entity.Property(e => e.attivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.codice).IsUnicode(false);

                entity.Property(e => e.descBreve).IsUnicode(false);

                entity.Property(e => e.subCodice).IsUnicode(false);
            });
            modelBuilder.Entity<anagrafica_ente>(entity =>
            {
                entity.HasKey(e => e.id_ente)
                    .HasName("pk_anagrafica_ente");

                entity.HasComment("Elenco enti");

                entity.Property(e => e.CBILL)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.application_code_pagopa)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.aux_digit_pagopa)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cap).IsUnicode(false);

                entity.Property(e => e.cod_ente)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cod_fiscale).IsUnicode(false);

                entity.Property(e => e.codice_concessione)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.codice_ente).IsUnicode(false);

                entity.Property(e => e.codice_ente_pagopa)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.codice_segregazione_pagopa)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.codice_struttura_ente_pagopa)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.descrizione_ente).IsUnicode(false);

                entity.Property(e => e.email).IsUnicode(false);

                entity.Property(e => e.flag_Tipo_rendicontazione)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_idrico)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_presenze)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_sosta)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_sportello)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_tipo_gestione_pagopa)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.indirizzo).IsUnicode(false);

                entity.Property(e => e.indirizzo_ip_db).IsUnicode(false);

                entity.Property(e => e.nome_db)
                    .IsUnicode(false)
                    .HasComment("Database settoriale che contiene i dati dell'ente");

                entity.Property(e => e.p_iva).IsUnicode(false);

                entity.Property(e => e.password_db).IsUnicode(false);

                entity.Property(e => e.pec).IsUnicode(false);

                entity.Property(e => e.tel1).IsUnicode(false);

                entity.Property(e => e.tel2).IsUnicode(false);

                entity.Property(e => e.tp_entity_id).IsUnicode(false);

                entity.Property(e => e.url_ente).IsUnicode(false);

                entity.Property(e => e.user_name_db).IsUnicode(false);

                entity.HasOne(d => d.cod_comuneNavigation)
                    .WithMany(p => p.anagrafica_ente)
                    .HasForeignKey(d => d.cod_comune)
                    .HasConstraintName("FK_anagrafica_ente_ser_comuni");

                entity.HasOne(d => d.cod_provinciaNavigation)
                    .WithMany(p => p.anagrafica_ente)
                    .HasForeignKey(d => d.cod_provincia)
                    .HasConstraintName("FK_anagrafica_ente_ser_province");

                entity.HasOne(d => d.cod_regioneNavigation)
                    .WithMany(p => p.anagrafica_ente)
                    .HasForeignKey(d => d.cod_regione)
                    .HasConstraintName("FK_anagrafica_ente_ser_regioni");

                entity.HasOne(d => d.id_risorsa_webNavigation)
                    .WithMany(p => p.anagrafica_ente)
                    .HasForeignKey(d => d.id_risorsa_web)
                    .HasConstraintName("FK_anagrafica_ente_anagrafica_risorse");

                entity.HasOne(d => d.id_tipo_enteNavigation)
                    .WithMany(p => p.anagrafica_ente)
                    .HasForeignKey(d => d.id_tipo_ente)
                    .HasConstraintName("FK_anagrafica_ente_tab_tipo_ente");
            });

            modelBuilder.Entity<anagrafica_pagopa_intermediari>(entity =>
            {
                entity.HasKey(e => e.id_intermediario)
                    .HasName("PK__anagrafi__B2F2A659F3E9BCB1");

                entity.Property(e => e.cod_connettore_ftp).IsUnicode(false);

                entity.Property(e => e.cod_connettore_pdd).IsUnicode(false);

                entity.Property(e => e.cod_intermediario).IsUnicode(false);

                entity.Property(e => e.denominazione).IsUnicode(false);

                entity.HasOne(d => d.id_enteNavigation)
                    .WithMany(p => p.anagrafica_pagopa_intermediari)
                    .HasForeignKey(d => d.id_ente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ente");
            });
            modelBuilder.Entity<anagrafica_pagopa_stazioni>(entity =>
            {
                entity.HasKey(e => e.id_stazione)
                    .HasName("PK__anagrafi__8660C6B38501FF7C");

                entity.Property(e => e.cod_stazione).IsUnicode(false);

                entity.Property(e => e.password).IsUnicode(false);

                entity.HasOne(d => d.id_intermediarioNavigation)
                    .WithMany(p => p.anagrafica_pagopa_stazioni)
                    .HasForeignKey(d => d.id_intermediario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_intermediario");
            });

            modelBuilder.Entity<anagrafica_risorse>(entity =>
            {
                entity.HasKey(e => e.id_risorsa)
                    .HasName("pk_anagrafica_risorse");

                entity.Property(e => e.cod_fiscale)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cod_stato)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cognome).IsUnicode(false);

                entity.Property(e => e.data_cessazione)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.email).IsUnicode(false);

                entity.Property(e => e.flag_interna_esterna)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_ispettore)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_lavorazione_istanze)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.indirizzo).IsUnicode(false);

                entity.Property(e => e.livello_inquadramento).IsUnicode(false);

                entity.Property(e => e.matricola).IsUnicode(false);

                entity.Property(e => e.nome).IsUnicode(false);

                entity.Property(e => e.password).IsUnicode(false);

                entity.Property(e => e.pec).IsUnicode(false);

                entity.Property(e => e.sesso)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.tel_casa).IsUnicode(false);

                entity.Property(e => e.tel_cellulare).IsUnicode(false);

                entity.Property(e => e.titolo).IsUnicode(false);

                entity.Property(e => e.username).IsUnicode(false);

                entity.HasOne(d => d.cod_comune_nasNavigation)
                    .WithMany(p => p.anagrafica_risorse)
                    .HasForeignKey(d => d.cod_comune_nas)
                    .HasConstraintName("FK_anagrafica_risorse_ser_comuni");

                entity.HasOne(d => d.id_ente_appartenenzaNavigation)
                    .WithMany(p => p.anagrafica_risorse)
                    .HasForeignKey(d => d.id_ente_appartenenza)
                    .HasConstraintName("FK_anagrafica_risorse_anagrafica_ente");

                entity.HasOne(d => d.id_ruolo_mansioneNavigation)
                    .WithMany(p => p.anagrafica_risorse)
                    .HasForeignKey(d => d.id_ruolo_mansione)
                    .HasConstraintName("FK_anagrafica_risorse_anagrafica_ruolo_mansione");

                entity.HasOne(d => d.id_struttura_statoNavigation)
                    .WithMany(p => p.anagrafica_risorse)
                    .HasForeignKey(d => d.id_struttura_stato)
                    .HasConstraintName("FK_anagrafica_risorse_anagrafica_strutture_aziendali");
            });
            modelBuilder.Entity<anagrafica_risorse>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<anagrafica_ruolo_mansione>(entity =>
            {
                entity.HasKey(e => e.id_ruolo_mansione)
                    .HasName("pk_anagrafica_ruolo_mansione_1");

                entity.Property(e => e.cod_ruolo_mansione).IsUnicode(false);

                entity.Property(e => e.descr_ruolo_mansione).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });
            modelBuilder.Entity<anagrafica_ruolo_mansione>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<anagrafica_stato_carrello>(entity =>
            {
                entity.HasKey(e => e.id_anagrafica_stato)
                    .HasName("pk_anagrafica_stato_carrello");

                entity.Property(e => e.cod_stato).IsUnicode(false);

                entity.Property(e => e.desc_stato).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });
            modelBuilder.Entity<anagrafica_stato_carrello>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<anagrafica_strutture_aziendali>(entity =>
            {
                entity.HasKey(e => e.id_struttura_aziendale)
                    .HasName("pk_anagrafica_strutture_aziendali");

                entity.Property(e => e.cap).IsUnicode(false);

                entity.Property(e => e.codice_struttura_aziendale).IsUnicode(false);

                entity.Property(e => e.descr_struttura).IsUnicode(false);

                entity.Property(e => e.email).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.indirizzo).IsUnicode(false);

                entity.Property(e => e.provincia).IsUnicode(false);

                entity.Property(e => e.telefono).IsUnicode(false);

                entity.Property(e => e.tipo_struttura)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.id_ente_appartenenzaNavigation)
                    .WithMany(p => p.anagrafica_strutture_aziendali)
                    .HasForeignKey(d => d.id_ente_appartenenza)
                    .HasConstraintName("FK_anagrafica_strutture_aziendali_anagrafica_ente");

                entity.HasOne(d => d.id_risorsaNavigation)
                    .WithMany(p => p.anagrafica_strutture_aziendali)
                    .HasForeignKey(d => d.id_risorsa)
                    .HasConstraintName("FK_anagrafica_strutture_aziendali_anagrafica_risorse");
            });
            modelBuilder.Entity<anagrafica_strutture_aziendali>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<join_applicazioni_secondarie>(entity =>
            {
                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((1))")
                    .IsFixedLength(true);
            });
            modelBuilder.Entity<join_applicazioni_secondarie>().HasQueryFilter(post => post.flag_on_off == "1");



            modelBuilder.Entity<tab_applicazioni>(entity =>
            {
                entity.Property(e => e.actionName).IsUnicode(false);

                entity.Property(e => e.codice).IsUnicode(false);

                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.icona).IsUnicode(false);

                entity.Property(e => e.label_menu).IsUnicode(false);

                entity.Property(e => e.parametri_url).IsUnicode(false);

                entity.Property(e => e.tipo_applicazione)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.tooltip).IsUnicode(false);

                entity.HasOne(d => d.id_tab_funzionalitaNavigation)
                    .WithMany(p => p.tab_applicazioni)
                    .HasForeignKey(d => d.id_tab_funzionalita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tab_applicazioni_tab_funzionalita");

                entity.HasOne(d => d.id_tab_pagineNavigation)
                    .WithMany(p => p.tab_applicazioni)
                    .HasForeignKey(d => d.id_tab_pagine)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tab_applicazioni_tab_pagine");
            });
            modelBuilder.Entity<tab_applicazioni>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<join_utenti_enti>(entity =>
            {
                entity.Property(e => e.cod_stato)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_on_off).IsUnicode(false);
            });
            modelBuilder.Entity<join_utenti_enti>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<ser_comuni>(entity =>
            {
                entity.HasKey(e => e.cod_comune)
                    .HasName("pk_ser_comuni");

                entity.Property(e => e.cod_comune).ValueGeneratedNever();

                entity.Property(e => e.cap_comune).IsUnicode(false);

                entity.Property(e => e.cod_catasto).IsUnicode(false);

                entity.Property(e => e.cod_istat)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.des_comune).IsUnicode(false);

                entity.HasOne(d => d.cod_provinciaNavigation)
                    .WithMany(p => p.ser_comuni)
                    .HasForeignKey(d => d.cod_provincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ser_comuni_ser_province");

                entity.HasOne(d => d.cod_regioneNavigation)
                    .WithMany(p => p.ser_comuni)
                    .HasForeignKey(d => d.cod_regione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ser_comuni_ser_regioni");

                entity.Property(e => e.cap_comune).IsRequired(false);
                entity.Property(e => e.cod_istat).IsRequired(false);
                entity.Property(e => e.cod_catasto).IsRequired(false);
                entity.Property(e => e.des_comune).IsRequired(false);
            });

            modelBuilder.Entity<ser_province>(entity =>
            {
                entity.HasKey(e => e.cod_provincia)
                    .HasName("pk_ser_province");

                entity.HasComment("Tabella dele province italiane");

                entity.Property(e => e.cod_provincia).ValueGeneratedNever();

                entity.Property(e => e.cod_istat)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.des_provincia).IsUnicode(false);

                entity.Property(e => e.sig_provincia).IsUnicode(false);

                entity.HasOne(d => d.cod_regioneNavigation)
                    .WithMany(p => p.ser_province)
                    .HasForeignKey(d => d.cod_regione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ser_province_ser_regioni");
            });

            modelBuilder.Entity<ser_regioni>(entity =>
            {
                entity.HasKey(e => e.cod_regione)
                    .HasName("pk_ser_regioni");

                entity.HasComment("Tabella delle regioni italiane");

                entity.Property(e => e.cod_istat)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.des_regione).IsUnicode(false);
            });

            modelBuilder.Entity<ser_stati_esteri>(entity =>
            {
                entity.Property(e => e.desc_stato).IsUnicode(false);

                entity.Property(e => e.sigla_stato).IsUnicode(false);
            });

            modelBuilder.Entity<ser_stati_esteri_new>(entity =>
            {
                entity.Property(e => e.codice_riferimento).IsUnicode(false);

                entity.Property(e => e.codice_stato).IsUnicode(false);

                entity.Property(e => e.denominazione_stato).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.sigla_stato).IsUnicode(false);
            });
            modelBuilder.Entity<ser_stati_esteri_new>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_abilitazione>(entity =>
            {
                entity.HasKey(e => e.id_tab_abilitazione)
                    .HasName("PK_tab_abilitazione_new");

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });
            modelBuilder.Entity<tab_abilitazione>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_cc_riscossione>(entity =>
            {
                entity.Property(e => e.ABI_CC)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CAB_CC)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.DES_COMUNE).IsUnicode(false);

                entity.Property(e => e.Flag_modalita_pagamento_standard)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Flag_riversamenti_anticipati)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Flag_tipo_rendicontazione)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IBAN).IsUnicode(false);

                entity.Property(e => e.NUMERO_CONTO_CONTABILE_CC_RISCOSSIONE).IsUnicode(false);

                entity.Property(e => e.NUMERO_CONTO_CONTABILE_SPESE_RISCOSSIONE).IsUnicode(false);

                entity.Property(e => e.aut_cc).IsUnicode(false);

                entity.Property(e => e.aut_cc_pagopa).IsUnicode(false);

                entity.Property(e => e.bic_swift).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_recupero_spese_riscossione)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_registro)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_tipo_cc)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.intestazione_cc).IsUnicode(false);

                entity.Property(e => e.intestazione_cc1_bollettino).IsUnicode(false);

                entity.Property(e => e.intestazione_cc2_bollettino).IsUnicode(false);

                entity.Property(e => e.num_cc).IsUnicode(false);

                entity.Property(e => e.num_cc_new).IsUnicode(false);

                entity.Property(e => e.periodicita_esportazione_pagamenti)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.periodicita_rendicontazione)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.periodicita_rettifica_rendicontazione)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.tipo_riversamento)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });
            modelBuilder.Entity<tab_cc_riscossione>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_domande_segrete>(entity =>
            {
                entity.Property(e => e.domanda).IsUnicode(false);
            });

            modelBuilder.Entity<tab_funzionalita>(entity =>
            {
                entity.Property(e => e.codice).IsUnicode(false);

                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.icona).IsUnicode(false);

                entity.HasOne(d => d.id_tab_procedureNavigation)
                    .WithMany(p => p.tab_funzionalita)
                    .HasForeignKey(d => d.id_tab_procedure)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tab_funzionalita_tab_procedure");
            });
            modelBuilder.Entity<tab_funzionalita>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_idpspid>(entity =>
            {
                entity.Property(e => e.DateTimeFormat)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('yyyy''-''MM''-''dd''T''HH'':''mm'':''ss''.''fff''Z''')");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EntityId).IsUnicode(false);

                entity.Property(e => e.OrganizationDisplayName).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.OrganizationUrl).IsUnicode(false);

                entity.Property(e => e.SingleLogoutServiceUrl).IsUnicode(false);

                entity.Property(e => e.SingleSignOnServiceUrl).IsUnicode(false);

                entity.Property(e => e.SubjectNameIdRemoveText).IsUnicode(false);

                entity.Property(e => e.ambiente)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A')");

                entity.Property(e => e.certificate).IsUnicode(false);

                entity.Property(e => e.flag_on_off).IsUnicode(false);

                entity.Property(e => e.logo).IsUnicode(false);

                entity.Property(e => e.url_metadata).IsUnicode(false);
            });
            modelBuilder.Entity<tab_idpspid>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_pagine>(entity =>
            {
                entity.Property(e => e.actions).IsUnicode(false);

                entity.Property(e => e.controller).IsUnicode(false);

                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.tipo).IsUnicode(false);
            });
            modelBuilder.Entity<tab_pagine>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_procedure>(entity =>
            {
                entity.Property(e => e.codice).IsUnicode(false);

                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.icona).IsUnicode(false);
            });
            modelBuilder.Entity<tab_procedure>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_registro_spid>(entity =>
            {
                entity.Property(e => e.codice_fiscale).IsUnicode(false);

                entity.Property(e => e.codice_spid).IsUnicode(false);

                entity.Property(e => e.email).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.mobile).IsUnicode(false);
            });
            modelBuilder.Entity<tab_registro_spid>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_tassonomia_pagopa>(entity =>
            {
                entity.Property(e => e.Codice_tassonomia_pagopa).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });
            modelBuilder.Entity<tab_tassonomia_pagopa>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_tipo_ente>(entity =>
            {
                entity.HasKey(e => e.id_tipo_ente)
                    .HasName("pk_tab_tipo_ente");

                entity.Property(e => e.cod_tipo_ente)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.desc_tipo_ente)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.prefisso_codice)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<tab_toponimi>(entity =>
            {
                entity.HasKey(e => e.id_toponimo)
                    .HasName("pk_tab_toponimi_new");

                entity.Property(e => e.cap_toponimo)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cod_fonte).IsUnicode(false);

                entity.Property(e => e.cod_stato)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.cod_toponimo).IsUnicode(false);

                entity.Property(e => e.desc_toponimo).IsUnicode(false);

                entity.Property(e => e.descrizione_toponimo).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_val_toponimo)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.frazione_toponimo).IsUnicode(false);
            });
            modelBuilder.Entity<tab_toponimi>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_utenti>(entity =>
            {
                entity.HasKey(e => e.id_utente)
                    .HasName("PK_login_utente");

                entity.Property(e => e.cod_stato)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.codice_fiscale).IsUnicode(false);

                entity.Property(e => e.codice_reset_password).IsUnicode(false);

                entity.Property(e => e.codice_spid).IsUnicode(false);

                entity.Property(e => e.cognome).IsUnicode(false);

                entity.Property(e => e.email).IsUnicode(false);

                entity.Property(e => e.email_spid).IsUnicode(false);

                entity.Property(e => e.flag_on_off).IsUnicode(false);

                entity.Property(e => e.nazionalita).IsUnicode(false);

                entity.Property(e => e.nome).IsUnicode(false);

                entity.Property(e => e.nome_utente).IsUnicode(false);

                entity.Property(e => e.p_iva).IsUnicode(false);

                entity.Property(e => e.password_utente).IsUnicode(false);

                entity.Property(e => e.pref_fisso).IsUnicode(false);

                entity.Property(e => e.pref_mobile).IsUnicode(false);

                entity.Property(e => e.ragione_sociale).IsUnicode(false);

                entity.Property(e => e.risposta_domanda_segreta).IsUnicode(false);

                entity.Property(e => e.telefono_fisso).IsUnicode(false);

                entity.Property(e => e.telefono_mobile).IsUnicode(false);

                entity.Property(e => e.telefono_mobile_spid).IsUnicode(false);

                entity.Property(e => e.tipo_utente).IsUnicode(false);
            });
            modelBuilder.Entity<tab_utenti>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_ambiente>(entity =>
            {
                entity.Property(e => e.cod_ambiente)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.descrizione).IsUnicode(false);
            });
            modelBuilder.Entity<tab_path_portale>(entity =>
            {
                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.path_download).IsUnicode(false);

                entity.Property(e => e.path_image).IsUnicode(false);

                entity.Property(e => e.path_upload).IsUnicode(false);

                entity.HasOne(d => d.id_tab_ambienteNavigation)
                    .WithMany(p => p.tab_path_portale)
                    .HasForeignKey(d => d.id_tab_ambiente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tab_path_portale_tab_ambiente");
            });
            modelBuilder.Entity<tab_path_portale>().HasQueryFilter(post => post.flag_on_off == "1");
            modelBuilder.Entity<anagrafica_ente>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_abilitazione_menu>(entity =>
            {
                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });
            modelBuilder.Entity<tab_abilitazione_menu>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_menu_primo_livello>(entity =>
            {
                entity.Property(e => e.action).IsUnicode(false);

                entity.Property(e => e.codice).IsUnicode(false);

                entity.Property(e => e.controller).IsUnicode(false);

                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_link)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_visibile)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.tipo_menu).IsUnicode(false);

                entity.Property(e => e.tooltip).IsUnicode(false);
            });
            modelBuilder.Entity<tab_menu_primo_livello>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_menu_secondo_livello>(entity =>
            {
                entity.Property(e => e.action).IsUnicode(false);

                entity.Property(e => e.codice).IsUnicode(false);

                entity.Property(e => e.controller).IsUnicode(false);

                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_link)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_visibile)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.tipo_menu).IsUnicode(false);

                entity.Property(e => e.tooltip).IsUnicode(false);

                entity.HasOne(d => d.id_tab_menu_primo_livelloNavigation)
                    .WithMany(p => p.tab_menu_secondo_livello)
                    .HasForeignKey(d => d.id_tab_menu_primo_livello)
                    .HasConstraintName("FK_tab_menu_secondo_livello_tab_menu_primo_livello");
            });
            modelBuilder.Entity<tab_menu_secondo_livello>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_menu_terzo_livello>(entity =>
            {
                entity.Property(e => e.action).IsUnicode(false);

                entity.Property(e => e.controller).IsUnicode(false);

                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_link)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.flag_visibile)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.tipo_menu).IsUnicode(false);

                entity.Property(e => e.tooltip).IsUnicode(false);

                entity.HasOne(d => d.id_tab_menu_secondo_livelloNavigation)
                    .WithMany(p => p.tab_menu_terzo_livello)
                    .HasForeignKey(d => d.id_tab_menu_secondo_livello)
                    .HasConstraintName("FK_tab_menu_terzo_livello_tab_menu_secondo_livello");
            });
            modelBuilder.Entity<tab_menu_terzo_livello>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<Cellulari>(entity =>
            {
                entity.Property(e => e.codiceVerifica).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1')")
                    .IsFixedLength(true);

                entity.Property(e => e.numero).IsUnicode(false);
            });
            modelBuilder.Entity<Cellulari>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<FasceTariffeAbbonamenti>(entity =>
            {
                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.unitaMisura).IsUnicode(false);

                entity.HasOne(d => d.idTariffaAbbonamentoNavigation)
                    .WithMany(p => p.FasceTariffeAbbonamenti)
                    .HasForeignKey(d => d.idTariffaAbbonamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FasceTariffeAbbonamenti_TariffeAbbonamenti");
            });
            modelBuilder.Entity<FasceTariffeAbbonamenti>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<RicaricheConfermate>(entity =>
            {
                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1')")
                    .IsFixedLength(true);

                entity.HasOne(d => d.idRicaricaAbbonamentoNavigation)
                    .WithMany(p => p.RicaricheConfermate)
                    .HasForeignKey(d => d.idRicaricaAbbonamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RicaricheAbbonamento_translog");

                entity.HasOne(d => d.idSMSOutNavigation)
                    .WithMany(p => p.RicaricheConfermate)
                    .HasForeignKey(d => d.idSMSOut)
                    .HasConstraintName("FK_RicaricheConfermate_SMSOut");
            });
            modelBuilder.Entity<RicaricheConfermate>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<SMSIn>(entity =>
            {
                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1')")
                    .IsFixedLength(true);

                entity.Property(e => e.notaElaborazione).IsUnicode(false);

                entity.Property(e => e.numeroDestinatario).IsUnicode(false);

                entity.Property(e => e.numeroMittente).IsUnicode(false);

                entity.Property(e => e.testo).IsUnicode(false);
            });
            modelBuilder.Entity<SMSIn>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<SMSOut>(entity =>
            {
                entity.HasKey(e => e.idSMSOut)
                    .HasName("PK_SMSOut_1");

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1')")
                    .IsFixedLength(true);

                entity.Property(e => e.numeroDestinatario).IsUnicode(false);

                entity.Property(e => e.numeroMittente).IsUnicode(false);

                entity.Property(e => e.testo).IsUnicode(false);

                entity.HasOne(d => d.idSMSInNavigation)
                    .WithMany(p => p.SMSOut)
                    .HasForeignKey(d => d.idSMSIn)
                    .HasConstraintName("FK_SMSOut_SMSIn");
            });
            modelBuilder.Entity<SMSOut>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<Stalli>(entity =>
            {
                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1')")
                    .IsFixedLength(true);

                entity.Property(e => e.numero).IsUnicode(false);

                entity.Property(e => e.ubicazione).IsUnicode(false);

                entity.HasOne(d => d.idTariffaNavigation)
                    .WithMany(p => p.Stalli)
                    .HasForeignKey(d => d.idTariffa)
                    .HasConstraintName("FK_Stalli_Tariffe");
            });
            modelBuilder.Entity<Stalli>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<Tariffe>(entity =>
            {
                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });
            modelBuilder.Entity<Tariffe>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<TariffeAbbonamenti>(entity =>
            {
                entity.Property(e => e.descrizione).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });
            modelBuilder.Entity<TariffeAbbonamenti>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<TariffeFlat>(entity =>
            {
                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.idTariffaFlat).ValueGeneratedOnAdd();

                entity.HasOne(d => d.idTariffaNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.idTariffa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TariffeFlat_Tariffe");
            });
            modelBuilder.Entity<TariffeFlat>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<TitoliSMS>(entity =>
            {
                entity.Property(e => e.codice).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.idCellulareNavigation)
                    .WithMany(p => p.TitoliSMS)
                    .HasForeignKey(d => d.idCellulare)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TitoliSMS_Cellulari");

                entity.HasOne(d => d.idStalloNavigation)
                    .WithMany(p => p.TitoliSMS)
                    .HasForeignKey(d => d.idStallo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TitoliSMS_Stalli");
            });
            modelBuilder.Entity<TitoliSMS>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<TitoliSMSTarga>(entity =>
            {
                entity.Property(e => e.codice).IsUnicode(false);

                entity.Property(e => e.codice_titolo).IsUnicode(false);

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.idCellulareNavigation)
                    .WithMany(p => p.TitoliSMSTarga)
                    .HasForeignKey(d => d.idCellulare)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TitoliSMSTarga_Cellulari");

                entity.HasOne(d => d.idStalloNavigation)
                    .WithMany(p => p.TitoliSMSTarga)
                    .HasForeignKey(d => d.idStallo)
                    .HasConstraintName("FK_TitoliSMSTarga_Stalli");
            });
            modelBuilder.Entity<TitoliSMSTarga>().HasQueryFilter(post => post.flag_on_off == "1");
            modelBuilder.Entity<translog>(entity =>
            {
                entity.HasKey(e => e.tlRecordID)
                    .HasName("PK__translog__287222B930E33A54");

                entity.Property(e => e.tlRecordID).ValueGeneratedNever();

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.tlCurrency).IsUnicode(false);

                entity.Property(e => e.tlID).IsUnicode(false);

                entity.Property(e => e.tlLicenseNo).IsUnicode(false);

                entity.Property(e => e.tlPDM_TicketNo_union).IsUnicode(false);

                entity.Property(e => e.tlPSAM).IsUnicode(false);

                entity.Property(e => e.tlParkingSpaceNo).IsUnicode(false);

                entity.Property(e => e.tlValidThru).IsUnicode(false);
            });
            modelBuilder.Entity<translog>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<translog_pyng>(entity =>
            {
                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1')")
                    .IsFixedLength(true);

                entity.Property(e => e.tlCurrency).IsUnicode(false);

                entity.Property(e => e.tlID).IsUnicode(false);

                entity.Property(e => e.tlLicenseNo).IsUnicode(false);

                entity.Property(e => e.tlPSAM).IsUnicode(false);

                entity.Property(e => e.tlParkingSpaceNo).IsUnicode(false);

                entity.Property(e => e.tlServiceCarSender).IsUnicode(false);

                entity.Property(e => e.tlValidThru).IsUnicode(false);
            });

            modelBuilder.Entity<translog_pyng>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<tab_registro_cookie>(entity =>
            {
                entity.Property(e => e.consenso).IsUnicode(false);

                entity.Property(e => e.data_stato).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.flag_on_off)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.headers).IsUnicode(false);

                entity.Property(e => e.indirizzo_ip).IsUnicode(false);

                entity.Property(e => e.session_id).IsUnicode(false);
            });
            modelBuilder.Entity<tab_registro_cookie>().HasQueryFilter(post => post.flag_on_off == "1");

            modelBuilder.Entity<AbbonamentiPeriodici>().HasQueryFilter(post => post.flag_on_off == "1");
            modelBuilder.Entity<Abbonamenti>().HasQueryFilter(post => post.flag_on_off == "1");
            modelBuilder.Entity<AbbonamentiRinnovi>().HasQueryFilter(post => post.flag_on_off == "1");

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
