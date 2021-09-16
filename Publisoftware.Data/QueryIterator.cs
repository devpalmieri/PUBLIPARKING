using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class QueryIterator
    {
        public interface IIterateItemCbArg<T> where T : class
        {
            dbEnte Ctx { get; }
            T Item { get; }
            int ItemIndex { get; }
            int RecordCount { get; }
        }

        private class IterateItemCbArg<T> : IIterateItemCbArg<T> where T : class
        {
            public dbEnte Ctx { get; set; }
            public T Item { get; set; }
            public int ItemIndex { get; set; }
            public int RecordCount { get; set; }
        }

        public class PercProgressArgs
        {
            public double Perc { get; set; }
        }

        private readonly int _idStruttura;
        private readonly int _idRisorsa;
        private readonly Action<PercProgressArgs> _progressCb;
        private readonly DBInfos _dbInfos;

        private int _saveEveryCount;

        public QueryIterator(
            DBInfos dbInfos,
            int idStruttura,
            int idRisorsa,
            Action<PercProgressArgs> progressCb)
        {
            _idStruttura = idStruttura;
            _idRisorsa = idRisorsa;
            _progressCb = progressCb;
            _dbInfos = dbInfos;
            _saveEveryCount = 50;
        }

        public int SaveEveryCount
        {
            get { return _saveEveryCount; }
            set { if (value < 1) throw new ArgumentException(nameof(value)); _saveEveryCount = value; }
        }

        private void Dispose<T>(IterateItemCbArg<T> itArg) where T : class
        {
            if (itArg.Ctx != null)
            {
                itArg.Ctx.Dispose();
                itArg.Ctx = null;

                GC.Collect();
                GC.WaitForFullGCComplete();
                GC.WaitForPendingFinalizers();
            }
        }

        public void IterateAll<T>(
            Func<dbEnte, IQueryable<T>> asNoTrackingQueryCb,
            Action<IIterateItemCbArg<T>> itemActionCb,
            bool bSaveChanges) where T : class
        {
            IterateAll(asNoTrackingQueryCb, itemActionCb, bSaveChanges, CancellationToken.None);
        }

        public void IterateAll<T>(
            Func<dbEnte, IQueryable<T>> asNoTrackingQueryCb,
            Action<IIterateItemCbArg<T>> itemActionCb,
            bool bSaveChanges,
            CancellationToken cTok) where T : class
        {
            cTok.ThrowIfCancellationRequested();

            var itArg = new IterateItemCbArg<T>()
            {
                Ctx = null
            };
            int recordNum = 0;

            try
            {
                itArg.Ctx = _dbInfos.GetCtx(_idStruttura, _idRisorsa);

                IQueryable<T> query = asNoTrackingQueryCb(itArg.Ctx);
                cTok.ThrowIfCancellationRequested();

                itArg.RecordCount = query.Count();
                PercProgressArgs progPerc = new PercProgressArgs
                {
                    Perc = 0.0
                };
                if (itArg.RecordCount == 0)
                {
                    progPerc.Perc = 100;
                    _progressCb(progPerc);
                    return;
                }

                cTok.ThrowIfCancellationRequested();

                var items = query
                    //.AsNoTracking()
                    .ToList();
                int iMax = itArg.RecordCount;
                for (int i = 0; i < iMax; ++i)
                {
                    cTok.ThrowIfCancellationRequested();

                    itArg.Item = items[i];
                    itArg.ItemIndex = recordNum;

                    itemActionCb(itArg);
                    if (bSaveChanges)
                    {
                        itArg.Ctx.Entry(itArg.Item).State = System.Data.Entity.EntityState.Modified;
                    }

                    ++recordNum;
                    if (recordNum % _saveEveryCount == 0)
                    {

                        if (bSaveChanges)
                        {
                            itArg.Ctx.SaveChanges();
                        }
                        Dispose(itArg);
                        itArg.Ctx = _dbInfos.GetCtx(_idStruttura, _idRisorsa);

                        progPerc.Perc = i * (100.0 / iMax);
                        _progressCb(progPerc);
                    }
                }

                cTok.ThrowIfCancellationRequested();
                if (bSaveChanges)
                {
                    itArg.Ctx.SaveChanges();
                }
                progPerc.Perc = 100.0;
                _progressCb(progPerc);
            }
            finally
            {
                Dispose(itArg);
            }
        }
    }
}
