using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Publiparking.Web.Configuration;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Web.Classes.Consts;
using Publiparking.Web.Classes.Enumerator;

namespace Publiparking.Web.Base
{
    public class BaseController : Controller
    {
        private readonly DatabaseConfig _databaseConfigSettings;
        protected string _dbServer;
        protected string _dbName;
        protected string _dbUserName;
        protected string _dbPassWord;
        protected int _idStruttura;
        protected int _idRisorsa;
        public string Ambiente { get; set; }
        public StartMode StartMode { get; set; }
        public SpidConfig SpidConfig { get; set; }
        public BaseController()
        {
            this.Ambiente = Environment.GetEnvironmentVariable(ParkConsts.ASPNETCORE_ENVIRONMENT);
        }

        private DbParkContext _dbContext = null;

        public DbParkContext dbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = DbParkContextFactory.getContext(_dbServer, _dbName, _dbUserName, _dbPassWord, _idStruttura, _idRisorsa);

                }
                return _dbContext;
            }
        }

        public void resetContext()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }

        private DbParkContext _dbContextReadOnly = null;

        public DbParkContext dbContextReadOnly
        {
            get
            {
                if (_dbContextReadOnly == null)
                {
                    _dbContextReadOnly = DbParkContextFactory.getContext(_dbServer, _dbName, _dbUserName, _dbPassWord, _idStruttura, _idRisorsa, -1, true);

                }
                return _dbContextReadOnly;
            }
        }

        public void resetReadOnlyContext()
        {
            if (_dbContextReadOnly != null)
            {
                _dbContextReadOnly.Dispose();
                _dbContextReadOnly = null;
            }
        }

        private DbParkContext _dbContextGenerale = null;


        public DbParkContext dbContextGenerale
        {
            get
            {
                if (_dbContextGenerale == null)
                {
                    _dbContextGenerale = StartDatabaseSetting.GetGeneraleCtx();
                }
                return _dbContextGenerale;
            }
        }
        public void resetContextGenerale()
        {
            if (_dbContextGenerale != null)
            {
                _dbContextGenerale.Dispose();
                _dbContextGenerale = null;
            }
        }
        private DbParkContext _dbContextGeneraleReadOnly = null;
        public DbParkContext dbContextGeneraleReadOnly
        {
            get
            {
                if (_dbContextGeneraleReadOnly == null)
                {
                    _dbContextGeneraleReadOnly = StartDatabaseSetting.GetGeneraleReadOnlyCtx();
                }
                return _dbContextGeneraleReadOnly;
            }
        }
        public void resetContextGeneraleReadOnly()
        {
            if (_dbContextGeneraleReadOnly != null)
            {
                _dbContextGeneraleReadOnly.Dispose();
                _dbContextGeneraleReadOnly = null;
            }
        }
        #region MESSAGE
        /// <summary>
        /// Visualizza la modale
        /// per i messaggi
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="isPost"></param>
        public void ShowMessageBox(string message, MessageTypeEnum type, bool isPost = true)
        {
            string alert = "info";
            if (type == MessageTypeEnum.error)
                alert = "error";
            else if (type == MessageTypeEnum.warning)
                alert = "warning";
            else if (type == MessageTypeEnum.success)
                alert = "success";
            if (isPost)
            {
                TempData["ModalMessage"] = message;
                TempData["ModalTypeMessage"] = alert;
            }
            else
            {
                ViewBag.ModalMessage = message;
                ViewBag.ModalTypeMessage = alert;
            }
        }
        #endregion MESSAGE
    }

}
