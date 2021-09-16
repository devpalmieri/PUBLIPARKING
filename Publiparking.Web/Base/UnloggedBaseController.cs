using Italia.Spid.Authentication.IdP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Publiparking.Core.Data.BD;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Core.Utility.Log;
using Publiparking.Web.Classes;
using Publiparking.Web.Classes.Consts;
using Publiparking.Web.Classes.Enumerator;
using Publiparking.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Publiparking.Web.Base
{
    public class UnloggedBaseController : BaseController
    {
        protected readonly string _PATH_IMG_PREFIX = "/Content/img/";
        protected readonly string _PATH_PUBLIPARKING_PREFIX = "/Publiparking/";
        protected readonly string _PATH_PRIVACY_PREFIX = "/Documents/Privacy/";
        public IHttpContextAccessor UnLogHttpContextAccessor { get; set; }
        public IConfiguration ConfigurationRoot { get; set; }
        private readonly IMemoryCache _memoryCache;
        //protected IConfiguration _configurationRoot;
        public IMemoryCache memoryCache { get; set; }
        public UnloggedBaseController()
        {

            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            IConfiguration _configurationRoot = ConfigurationData.Configuration;
            this.StartMode = _configurationRoot.GetSection("StartMode").Get<StartMode>();
            this.SpidConfig = _configurationRoot.GetSection("SpidConfig").Get<SpidConfig>();
            this.UnLogHttpContextAccessor = _httpContextAccessor;
            this.memoryCache = _memoryCache;

            _dbServer = StartDatabaseSetting.DbServerGenerale;
            _dbName = StartDatabaseSetting.DbNameGenerale;
            _dbUserName = StartDatabaseSetting.DbUserName;
            _dbPassWord = StartDatabaseSetting.DbPassword;
            _idStruttura = StartDatabaseSetting.IdStruttura;
            _idRisorsa = StartDatabaseSetting.IdRisorsa;

            InitializeIdentityProviderList(ConfigurationData.IsDevelopment).GetAwaiter().GetResult();

        }
        static ILogger _logger = null;

        public static ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = LoggerFactory.getInstance().getLogger<NLogger>(nameof(DbParkContext));
                }
                return _logger;
            }
        }

        #region Actions
        [HttpPost]
        public JsonResult GetEntiByTipo(int IdTipoEnte)
        {
            var listEnti = AnagraficaEnteBD.GetParkList(dbContextGeneraleReadOnly)
                .Where(x => x.id_tipo_ente == IdTipoEnte)
                .Select(d => new { id_ente = d.id_ente, descrizione_ente = d.descrizione_ente });
            return Json(listEnti);


        }

        [HttpPost]
        public IActionResult GetComuni(string pIniziali, string pAttivi)
        {
            List<Core.Data.SqlServer.Entities.ser_comuni> v_list;

            try
            {
                if (pAttivi == "0")
                {
                    if (pIniziali.Length > 2)
                    {
                        v_list = SerComuniBD.GetList(dbContextGeneraleReadOnly)
                                            .Where(d => d.des_comune != null && d.des_comune.ToUpper().Contains(pIniziali.ToUpper()))
                                            .OrderBy(d => d.des_comune)
                                            .Take(100)
                                            .ToList();
                    }
                    else
                    {
                        v_list = SerComuniBD.GetList(dbContextGeneraleReadOnly)
                                            .Where(d => d.des_comune != null && d.des_comune.ToUpper().StartsWith(pIniziali.ToUpper()))
                                            .OrderBy(d => d.des_comune)
                                            .Take(100)
                                            .ToList();
                    }
                }
                else
                {
                    if (pIniziali.Length > 2)
                    {
                        v_list = SerComuniBD.GetListAttivi(dbContextGeneraleReadOnly)
                                        .Where(d => d.des_comune.ToUpper().Contains(pIniziali.ToUpper()))
                                        .OrderBy(d => d.des_comune)
                                        .Take(100)
                                        .ToList();
                    }
                    else
                    {
                        v_list = SerComuniBD.GetListAttivi(dbContextGeneraleReadOnly)
                                            .Where(d => d.des_comune.ToUpper().StartsWith(pIniziali.ToUpper()))
                                            .OrderBy(d => d.des_comune)
                                            .Take(100)
                                            .ToList();
                    }

                }
                string v_result = "{\"suggestions\":[";

                int v_conta = 0;

                foreach (Core.Data.SqlServer.Entities.ser_comuni v_item in v_list)
                {
                    if (v_conta++ != 0)
                    {
                        v_result += ",";
                    }

                    v_result += "{ \"value\": \"" + Classes.PSStringHelper.FormatStringForWS(v_item.des_comune.ToString()) + "\", \"data\": \"" + Classes.PSStringHelper.FormatStringForWS(v_item.cod_comune.ToString()) + "\" }";
                }

                v_result += "]}";

                return new ContentResult
                {
                    Content = v_result,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }

            catch (SqlException sx)
            {
                var message = sx.Errors[0].Message;
                Logger.LogException(string.Format("Errore in fase di caricamento dei comuni: {0}.", sx.Message), sx, EnLogSeverity.Error);
                return null;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    Logger.LogException(string.Format("L'Entità di tipo {0} in stato {1} presenta i seguenti errori: ", eve.Entry.Entity.GetType().Name, eve.Entry.State), e, EnLogSeverity.Error);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Logger.LogException(string.Format(" - Proprietà: {0}, Errore: {1}", ve.PropertyName, ve.ErrorMessage), e, EnLogSeverity.Error);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(string.Format("Errore in fase di caricamento dei comuni: {0}.", ex.Message), ex, EnLogSeverity.Error);
                return null;
            }

        }
        public IActionResult GetStati(string pIniziali)
        {
            pIniziali = pIniziali ?? String.Empty;

            List<ser_stati_esteri_new> v_list;
            v_list = SerStatiEsteriNewBD.GetList(dbContextGeneraleReadOnly)
                                        .Where(d => d.denominazione_stato.ToUpper().Contains(pIniziali.ToUpper()) && d.data_inizio_validita != d.data_fine_validita) //Luigi ha voluto questo controllo che le date fossero diverse
                                        .OrderBy(d => d.denominazione_stato)
                                        .Take(100)
                                        .ToList();

            string v_result = "{\"suggestions\":[";

            int v_conta = 0;

            foreach (ser_stati_esteri_new v_item in v_list)
            {
                if (v_conta++ != 0)
                {
                    v_result += ",";
                }

                v_result += "{ \"value\": \"" + PSStringHelper.FormatStringForWS(v_item.denominazione_stato.ToString()) + "\", \"data\": \"" + PSStringHelper.FormatStringForWS(v_item.denominazione_stato.ToString()) + "\" }";
            }

            v_result += "]}";

            return new ContentResult
            {
                Content = v_result,
                ContentType = "application/json",
                StatusCode = 200
            };
        }

        #endregion Actions
        public string buildDocPrivacyUrl(string nome_file)
        {
            string specificPath = nome_file, defaultPath = HttpUtility.HtmlEncode(nome_file);
            if (!nome_file.StartsWith(_PATH_IMG_PREFIX))
            {
                specificPath = String.Format("~{0}{1}", _PATH_PUBLIPARKING_PREFIX, Sessione.GetCurrentEnte() != null ? Sessione.GetCurrentEnte().id_ente.ToString() : "default");
                defaultPath = String.Format("~{0}{1}", _PATH_PUBLIPARKING_PREFIX, "default");
                specificPath += String.Format("/{0}", nome_file);
                defaultPath += String.Format("/{0}", nome_file);
            }
            else
            {
                specificPath = String.Format("~{0}", specificPath);
                defaultPath = String.Format("~{0}", defaultPath);
            }

            //TODO Luigi: MapPath non ritorna percorso fisico dove è copiato il sito sotto wwwroot
            //if (System.IO.File.Exists(this.HttpContext.Server.MapPath(specificPath)))
            //{
            return Url.Content(specificPath);
            //}
            //else if (System.IO.File.Exists(this.HttpContext.Server.MapPath(defaultPath)))
            //{
            //    return Url.Content(HttpUtility.HtmlEncode(defaultPath));
            //}

            return "";
        }
        #region SPID
        private async Task InitializeIdentityProviderList(bool isDebug)
        {
            string listIDPFile = "idpConfigDataList.json";
            if (!isDebug)
                listIDPFile = "idpConfigDataList_PROD.json";
            // Recupero i metadata degli IDP
            List<IdentityProviderMetaData> idpMetadataList = null;
            string idpMetadataListUrl = this.SpidConfig.IdpMetadataListUrl;
            //TODO: Da utilizzare quando AgID renderà disponibile la lista degli IDP
            if (!string.IsNullOrWhiteSpace(idpMetadataListUrl))
            {
                //idpMetadataList = await IdentityProvidersList.GetIdpMetaDataListAsync(idpMetadataListUrl);
            }


            //Ora viene recuperata dal database
            List<IdentityProviderConfigData> idpConfigDataList = GetSpidIDPLIst();
            //Salvo la lista degli IDP in Sessione
            string modOp = StartMode.DefaultHome;
            if (modOp.ToLower() == "homeutente")
            {
                Sessione.SetListIDP(idpConfigDataList);
            }
            // Inizializzo la lista degli IDP
            IdentityProvidersList.IdentityProvidersListFactory(idpMetadataList, idpConfigDataList);
        }

        private void CreateConfigDataListJsonFile(bool isDebug)
        {
            List<IdentityProviderConfigData> idpConfigDataList = null;
            string listIDPFile = "idpConfigDataList.json";
            if (!isDebug)
                listIDPFile = "idpConfigDataList_PROD.json";
            if (isDebug)
            {
                idpConfigDataList = new List<IdentityProviderConfigData>
                    {
                        new IdentityProviderConfigData()
                        {
                            EntityId = "http://localhost:8088",
                            OrganizationName = "Spid test",
                            OrganizationDisplayName = "Spid test",
                            OrganizationUrl = "http://localhost:8088/",
                            SingleSignOnServiceUrl ="http://localhost:8088/sso",
                            SingleLogoutServiceUrl = "http://localhost:8088/slo",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://validator.spid.gov.it",
                            OrganizationName = "Spid Validator",
                            OrganizationDisplayName = "Spid Validator",
                            OrganizationUrl = "https://validator.spid.gov.it",
                            SingleSignOnServiceUrl ="https://validator.spid.gov.it/samlsso",
                            SingleLogoutServiceUrl = "https://validator.spid.gov.it/samlsso",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://loginspid.aruba.it",
                            OrganizationName = "ArubaPEC S.p.A.",
                            OrganizationDisplayName = "ArubaPEC S.p.A.",
                            OrganizationUrl = "https://www.pec.it/",
                            SingleSignOnServiceUrl ="https://loginspid.aruba.it/ServiceLoginWelcome",
                            SingleLogoutServiceUrl = "https://loginspid.aruba.it/ServiceLogoutRequest",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://idptest.spid.gov.it",
                            OrganizationName = "AgID Spid",
                            OrganizationDisplayName = "AGID",
                            OrganizationUrl = "https://idp.spid.gov.it",
                            SingleSignOnServiceUrl ="https://idptest.spid.gov.it/sso",
                            SingleLogoutServiceUrl = "https://idptest.spid.gov.it/slo",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://identity.infocert.it",
                            OrganizationName = "InfoCert S.p.A.",
                            OrganizationDisplayName = "InfoCert S.p.A.",
                            OrganizationUrl = "https://www.infocert.it",
                            SingleSignOnServiceUrl ="https://identity.infocert.it/spid/samlsso",
                            SingleLogoutServiceUrl = "https://identity.infocert.it/spid/samlslo",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://spid.intesa.it",
                            OrganizationName = "IN.TE.S.A. S.p.A.",
                            OrganizationDisplayName = "Intesa S.p.A.",
                            OrganizationUrl = "https://www.intesa.it/",
                            SingleSignOnServiceUrl ="https://spid.intesa.it/Time4UserServices/services/idp/AuthnRequest/",
                            SingleLogoutServiceUrl = "https://spid.intesa.it/Time4UserServices/services/idp/SingleLogout",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://id.lepida.it/idp/shibboleth",
                            OrganizationName = "Lepida S.p.A.",
                            OrganizationDisplayName = "Lepida S.p.A.",
                            OrganizationUrl = "https://www.lepida.it/",
                            SingleSignOnServiceUrl ="https://id.lepida.it/idp/profile/SAML2/POST/SSO",
                            SingleLogoutServiceUrl = "https://id.lepida.it/idp/profile/SAML2/POST/SLO",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },

                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://id.lepida.it/idp/shibboleth",
                            OrganizationName = "Lepida S.p.A.",
                            OrganizationDisplayName = "Lepida S.p.A.",
                            OrganizationUrl = "https://www.lepida.it/",
                            SingleSignOnServiceUrl ="https://id.lepida.it/idp/profile/SAML2/POST/SSO",
                            SingleLogoutServiceUrl = "https://id.lepida.it/idp/profile/SAML2/POST/SLO",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://idp.namirialtsp.com/idp",
                            OrganizationName = "Namirial",
                            OrganizationDisplayName = "Namirial S.p.a. Trust Service Provider",
                            OrganizationUrl = "https://www.namirialtsp.com",
                            SingleSignOnServiceUrl ="https://idp.namirialtsp.com/idp/profile/SAML2/POST/SSO",
                            SingleLogoutServiceUrl = "https://idp.namirialtsp.com/idp/profile/SAML2/POST/SLO",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://spidposte.test.poste.it",
                            OrganizationName = "Poste Italiane SpA IDP DI TEST",
                            OrganizationDisplayName = "Poste Italiane SpA IDP DI TEST",
                            OrganizationUrl = "https://spidposte.test.poste.it",
                            SingleSignOnServiceUrl ="https://spidposte.test.poste.it/jod-fs/ssoservicepost",
                            SingleLogoutServiceUrl = "https://spidposte.test.poste.it/jod-fs/sloservicepost",
                            SubjectNameIdRemoveText = "SPID-", // We need to remove it from Subject Name ID otherwise subsequent logout will fail
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://posteid.poste.it",
                            OrganizationName = "Poste Italiane SpA",
                            OrganizationDisplayName = "Poste Italiane SpA",
                            OrganizationUrl = "https://www.poste.it",
                            SingleSignOnServiceUrl ="https://posteid.poste.it/jod-fs/ssoservicepost",
                            SingleLogoutServiceUrl = "https://posteid.poste.it/jod-fs/sloserviceresponsepost",
                            SubjectNameIdRemoveText = "SPID-", // We need to remove it from Subject Name ID otherwise subsequent logout will fail
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://spid.register.it",
                            OrganizationName = "Register.it S.p.A.",
                            OrganizationDisplayName = "Register.it S.p.A.",
                            OrganizationUrl = "https//www.register.it",
                            SingleSignOnServiceUrl ="https://spid.register.it/login/sso",
                            SingleLogoutServiceUrl = "https://spid.register.it/login/singleLogout",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://identity.sieltecloud.it",
                            OrganizationName = "Sielte S.p.A.",
                            OrganizationDisplayName = "Sielte S.p.A.",
                            OrganizationUrl = "http://www.sielte.it",
                            SingleSignOnServiceUrl ="https://identity.sieltecloud.it/simplesaml/saml2/idp/SSO.php",
                            SingleLogoutServiceUrl = "https://identity.sieltecloud.it/simplesaml/saml2/idp/SLS.php",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'",
                            NowDelta = -2 // TODO: Only for Sielte, still valid and required?
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://login.id.tim.it/affwebservices/public/saml2sso",
                            OrganizationName = "TI Trust Technologies srl",
                            OrganizationDisplayName = "Trust Technologies srl",
                            OrganizationUrl = "https://www.trusttechnologies.it",
                            SingleSignOnServiceUrl ="https://login.id.tim.it/affwebservices/public/saml2sso",
                            SingleLogoutServiceUrl = "https://login.id.tim.it/affwebservices/public/saml2slo",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://idptest.spid.gov.it",
                            OrganizationName = "AgID Spid",
                            OrganizationDisplayName = "AGID",
                            OrganizationUrl = "https://idp.spid.gov.it",
                            SingleSignOnServiceUrl ="https://idptest.spid.gov.it/sso",
                            SingleLogoutServiceUrl = "https://idptest.spid.gov.it/slo",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        }
                    };

            }
            else
            {
                idpConfigDataList = new List<IdentityProviderConfigData>
                    {
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://validator.spid.gov.it",
                            OrganizationName = "Spid Validator",
                            OrganizationDisplayName = "Spid Validator",
                            OrganizationUrl = "https://validator.spid.gov.it",
                            SingleSignOnServiceUrl ="https://validator.spid.gov.it/samlsso",
                            SingleLogoutServiceUrl = "https://validator.spid.gov.it/samlsso",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://loginspid.aruba.it",
                            OrganizationName = "ArubaPEC S.p.A.",
                            OrganizationDisplayName = "ArubaPEC S.p.A.",
                            OrganizationUrl = "https://www.pec.it/",
                            SingleSignOnServiceUrl ="https://loginspid.aruba.it/ServiceLoginWelcome",
                            SingleLogoutServiceUrl = "https://loginspid.aruba.it/ServiceLogoutRequest",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://identity.infocert.it",
                            OrganizationName = "InfoCert S.p.A.",
                            OrganizationDisplayName = "InfoCert S.p.A.",
                            OrganizationUrl = "https://www.infocert.it",
                            SingleSignOnServiceUrl ="https://identity.infocert.it/spid/samlsso",
                            SingleLogoutServiceUrl = "https://identity.infocert.it/spid/samlslo",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://spid.intesa.it",
                            OrganizationName = "IN.TE.S.A. S.p.A.",
                            OrganizationDisplayName = "Intesa S.p.A.",
                            OrganizationUrl = "https://www.intesa.it/",
                            SingleSignOnServiceUrl ="https://spid.intesa.it/Time4UserServices/services/idp/AuthnRequest/",
                            SingleLogoutServiceUrl = "https://spid.intesa.it/Time4UserServices/services/idp/SingleLogout",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://id.lepida.it/idp/shibboleth",
                            OrganizationName = "Lepida S.p.A.",
                            OrganizationDisplayName = "Lepida S.p.A.",
                            OrganizationUrl = "https://www.lepida.it/",
                            SingleSignOnServiceUrl ="https://id.lepida.it/idp/profile/SAML2/POST/SSO",
                            SingleLogoutServiceUrl = "https://id.lepida.it/idp/profile/SAML2/POST/SLO",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },

                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://id.lepida.it/idp/shibboleth",
                            OrganizationName = "Lepida S.p.A.",
                            OrganizationDisplayName = "Lepida S.p.A.",
                            OrganizationUrl = "https://www.lepida.it/",
                            SingleSignOnServiceUrl ="https://id.lepida.it/idp/profile/SAML2/POST/SSO",
                            SingleLogoutServiceUrl = "https://id.lepida.it/idp/profile/SAML2/POST/SLO",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://idp.namirialtsp.com/idp",
                            OrganizationName = "Namirial",
                            OrganizationDisplayName = "Namirial S.p.a. Trust Service Provider",
                            OrganizationUrl = "https://www.namirialtsp.com",
                            SingleSignOnServiceUrl ="https://idp.namirialtsp.com/idp/profile/SAML2/POST/SSO",
                            SingleLogoutServiceUrl = "https://idp.namirialtsp.com/idp/profile/SAML2/POST/SLO",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://posteid.poste.it",
                            OrganizationName = "Poste Italiane SpA",
                            OrganizationDisplayName = "Poste Italiane SpA",
                            OrganizationUrl = "https://www.poste.it",
                            SingleSignOnServiceUrl ="https://posteid.poste.it/jod-fs/ssoservicepost",
                            SingleLogoutServiceUrl = "https://posteid.poste.it/jod-fs/sloserviceresponsepost",
                            SubjectNameIdRemoveText = "SPID-", // We need to remove it from Subject Name ID otherwise subsequent logout will fail
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://spid.register.it",
                            OrganizationName = "Register.it S.p.A.",
                            OrganizationDisplayName = "Register.it S.p.A.",
                            OrganizationUrl = "https//www.register.it",
                            SingleSignOnServiceUrl ="https://spid.register.it/login/sso",
                            SingleLogoutServiceUrl = "https://spid.register.it/login/singleLogout",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://identity.sieltecloud.it",
                            OrganizationName = "Sielte S.p.A.",
                            OrganizationDisplayName = "Sielte S.p.A.",
                            OrganizationUrl = "http://www.sielte.it",
                            SingleSignOnServiceUrl ="https://identity.sieltecloud.it/simplesaml/saml2/idp/SSO.php",
                            SingleLogoutServiceUrl = "https://identity.sieltecloud.it/simplesaml/saml2/idp/SLS.php",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'",
                            NowDelta = -2 // TODO: Only for Sielte, still valid and required?
                        },
                        new IdentityProviderConfigData()
                        {
                            EntityId = "https://login.id.tim.it/affwebservices/public/saml2sso",
                            OrganizationName = "TI Trust Technologies srl",
                            OrganizationDisplayName = "Trust Technologies srl",
                            OrganizationUrl = "https://www.trusttechnologies.it",
                            SingleSignOnServiceUrl ="https://login.id.tim.it/affwebservices/public/saml2sso",
                            SingleLogoutServiceUrl = "https://login.id.tim.it/affwebservices/public/saml2slo",
                            SubjectNameIdRemoveText = string.Empty,
                            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
                            NowDelta = 0
                        }
                    };
            }


        }
        private List<IdentityProviderConfigData> GetSpidIDPLIst()
        {
            List<IdentityProviderConfigData> results = new List<IdentityProviderConfigData>();
            List<string> listAmbienti = null;
            if (ConfigurationData.IsDevelopment)
            {
                listAmbienti = new List<string>
                    { {SpidConsts.SPID_IDP_ALL},{SpidConsts.SPID_IDP_TEST} };
            }
            else
            {
                listAmbienti = new List<string>
                    { {SpidConsts.SPID_IDP_ALL},{SpidConsts.SPID_IDP_PRODUZIONE} };
            }
            DatabaseConfig dbConfig = new DatabaseConfig();
            DbParkContext context = dbConfig.GetGeneraleReadOnlyCtx();

            List<tab_idpspid> listSpidIDP = TabIdpSpidBD.GetSpidIDPList(listAmbienti, context);
            if (listSpidIDP != null)
            {
                results = (from c in listSpidIDP
                           select new IdentityProviderConfigData()
                           {
                               EntityId = c.EntityId,
                               OrganizationName = c.OrganizationName,
                               OrganizationDisplayName = c.OrganizationDisplayName,
                               OrganizationUrl = c.OrganizationUrl,
                               SingleSignOnServiceUrl = c.SingleSignOnServiceUrl,
                               SingleLogoutServiceUrl = c.SingleLogoutServiceUrl,
                               SubjectNameIdRemoveText = c.SubjectNameIdRemoveText,
                               DateTimeFormat = c.DateTimeFormat,
                               NowDelta = c.NowDelta.HasValue ? c.NowDelta.Value : 0,
                               logo = c.logo,
                               Description = c.Description,
                               Certificate = c.certificate,
                               Url_Metadata = c.url_metadata
                           }
                             ).ToList();
            }
            return results;
        }
        #endregion SPID


    }
}
