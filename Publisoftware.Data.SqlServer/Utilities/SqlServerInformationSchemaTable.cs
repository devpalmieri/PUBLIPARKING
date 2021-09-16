using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    //  TABLE_CATALOG nvarchar(128)
    //  TABLE_SCHEMA    nvarchar(128)
    //  TABLE_NAME sysname -- praticamente (se ricordo bene) nvarchar(128)
    //  TABLE_TYPE  varchar(10)
    public class SqlServerInformationSchemaTable
    {
        public string TABLE_CATALOG { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string TABLE_TYPE { get; set; }

        public static SqlServerInformationSchemaTable GetInformationSchemaTableFirst(string tablename, DbContext ctx)
        {
            SqlServerInformationSchemaTable t = ctx.Database.SqlQuery<SqlServerInformationSchemaTable>($@"SELECT * 
                   FROM INFORMATION_SCHEMA.TABLES 
                   WHERE TABLE_SCHEMA = 'dbo' 
                     AND TABLE_NAME = '{tablename}'")
                .FirstOrDefault();
            return t;
        }

        // Esempio creazione tabella:
        //        int rowsAffected = ctx.Database.ExecuteSqlCommand(
        //    "SET ANSI_NULLS ON;SET QUOTED_IDENTIFIER ON;" +
        //    $@"IF NOT EXISTS (SELECT * 
        //               FROM INFORMATION_SCHEMA.TABLES 
        //               WHERE TABLE_SCHEMA = 'dbo' 
        //                 AND TABLE_NAME = '{_nameOfTable_TAB_TOPONIMI_NORMALIZZATI_idEnte}') 
        //	CREATE TABLE [dbo].[{_nameOfTable_TAB_TOPONIMI_NORMALIZZATI_idEnte}](
        //		[id_tab_toponimi_normalizzati] [int] IDENTITY(1,1) NOT NULL,
        //		[id_ente] [int] NULL,
        //		[id_ente_gestito] [int] NULL,
        //		[cod_comune] [varchar](255) NULL,
        //		[cap_comune] [varchar](255) NULL,
        //		[id_toponimo_originale] [int] NULL,
        //		[indirizzo_originale] [varchar](255) NULL,
        //		[frazione_originale] [varchar](255) NULL,
        //		[tipo_toponimo_originale] [varchar](255) NULL,
        //		[toponimo_originale] [varchar](255) NULL,
        //		[tipo_toponimo_corretto_automaticamente] [varchar](255) NULL,
        //		[toponimo_corretto_automaticamente] [varchar](255) NULL,
        //		[toponimo_rimanente] [varchar](255) NULL,
        //		[tipo_toponimo_corretto_manualmente] [varchar](255) NULL,
        //		[toponimo_corretto_manualmente] [varchar](255) NULL,
        //		[tipo_toponimo_normalizzato] [varchar](255) NULL,
        //		[toponimo_normalizzato] [varchar](255) NULL,
        //		[numero_civico] [int] NULL,
        //		[sigla_civico] [varchar](255) NULL,
        //		[frazione] [varchar](255) NULL,
        //		[condominio] [varchar](255) NULL,
        //		[edificio] [varchar](255) NULL,
        //		[piano] [varchar](255) NULL,
        //		[scala] [varchar](255) NULL,
        //		[interno] [varchar](255) NULL,
        //		[fonte] [varchar](255) NULL,
        //		[id_toponimo_assegnato] [int] NULL,
        //		[note] [varchar](255) NULL,
        //		[cod_stato] [varchar](255) NULL,
        //		[flag_on_off] [varchar](255) NULL,
        //	 CONSTRAINT [PK_{_nameOfTable_TAB_TOPONIMI_NORMALIZZATI_idEnte}_x] PRIMARY KEY CLUSTERED 
        //	(
        //		[id_tab_toponimi_normalizzati] ASC
        //	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        //	) ON [PRIMARY]
        //");
    } // class
}
