#if false
using EFCache;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Common;
using Publisoftware.Data;
using System.Data.Entity.Infrastructure.Interception;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;

namespace Publisoftware.Data.DbConfig
{
    public class PSDDBConfiguration : DbConfiguration
    {
        public static PSDDBConfiguration Self { get; private set; } = null;

        private static readonly InMemoryCache _imCache = new InMemoryCache();
        public static int IMCacheCount { get { return _imCache.Count; } }

        public PSDDBConfiguration()
            //: this(true, true, new Data.DbConfig.EFCachingPolicy(true, new string[] { }))
            : this(false, false, new Data.DbConfig.EFCachingPolicy(true, new string[] { }))
        {
        }

        EFCachingPolicy _psdCachingPolicy;
        public EFCachingPolicy EFCachingPolicy { get { return _psdCachingPolicy; } }

        /// <summary>
        /// EF Custom Configuration
        /// </summary>
        /// <param name="logQueries">Set to true to log EF generated queries</param>
        /// <param name="cachingPolicy">Non null to use a second level cache, null otherwise</param>
        public PSDDBConfiguration(bool logQueries, bool logItemInCache, EFCachingPolicy psdCachingPolicy)
        {
            if (Self != null)
            {
                throw new Exception("PSDDBConfiguration deve essere instanziato solo una volta!");
            }
            Self = this;

            if (logQueries)
            {
                DbInterception.Add(new PSDDBEFInterceptor());
            }

            _psdCachingPolicy = psdCachingPolicy;
            if (psdCachingPolicy != null)
            {
                var cachingPolicy = psdCachingPolicy.CachePolicy;

                var transactionHandler = new CacheTransactionHandler(_imCache);
                AddInterceptor(transactionHandler);

                //var cachingPolicy = new CachingPolicy();
                //var cachingPolicy = new PSDCachingPolicy(logQueries);
                if (logItemInCache)
                {
                    PSDDBEFInterceptor.SetOnCommandLoggedCB(() =>
                    {
                        return String.Concat("Items in cache: ", _imCache.Count);
                    });
                }

                //Loaded +=
                //    (sender, e) => e.ReplaceService<DbProviderServices>(
                //        (s, _) => new CachingProviderServices(s, transactionHandler, cachingPolicy));
                Loaded +=
                    (sender, e) => e.ReplaceService<DbProviderServices>(
                        (s, _) => new CachingProviderServices(s, transactionHandler, cachingPolicy));

            }
        }

        public static void SetCacheableEntities(string[] includeEntities)
        {
            if (Self.EFCachingPolicy != null)
            {
                Self.EFCachingPolicy.SetCacheableEntities(includeEntities);
            }
            else
            {
                throw new Exception("No cache set for PSDDBConfiguration");
            }
        }
    }
}
#endif
