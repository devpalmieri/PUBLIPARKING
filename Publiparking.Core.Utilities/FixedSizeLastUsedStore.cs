using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Utilities
{
    /// <summary>
    /// Cache in memoria di un numero fisso di elementi, una volta raggiunto il limite
    /// ad ogni nuovo elemento aggiunto verrà sostituito un elemento esistente.
    /// </summary>
    /// <typeparam name="TKey">Chiave</typeparam>
    /// <typeparam name="TVal">Valore</typeparam>
    /// <remarks>
    /// Non vengono sostituiti in base alla frequenza di utilizzo ma semplicemente uno alla volta, sequenzialmente, a partire dal primo.
    /// </remarks>
    /// <example>
    /// Esempio: <br />
    ///         var test = new Batch.RivestizioniAnagrafiche.FizedSizeLastUsedStore<int, string>(4);
    ///         for (int i = 0; i< 4; ++i)
    ///         {
    ///             test.AddItem(i, i.ToString());
    ///         }
    ///         for (int i = 4; i< 6; ++i)
    ///         {
    ///             test.AddItem(i, i.ToString());
    ///         }
    /// </example>
    public class FixedSizeLastUsedStore<TKey, TVal>
    {
        public class StringCaseSensitiveComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return 0 == String.Compare(x, y, false);
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }

        public class StringCaseInsensitiveComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return 0 == String.Compare(x, y, false);
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }

        private readonly IDictionary<TKey, TVal> _store;
        private readonly IList<TKey> _keyOrderList;
        private readonly int _storeSize;
        private readonly IEqualityComparer<TKey> _keyComparer;

        private int _currIndex;

        public FixedSizeLastUsedStore(int storeSize = 30, IEqualityComparer<TKey> keyComparer = null)
        {
            _storeSize = storeSize;
            _store = new Dictionary<TKey, TVal>(storeSize);
            _keyOrderList = new List<TKey>(storeSize);
            _keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;

            _currIndex = 0;
        }

        public int StoreSize { get { return _storeSize; } }

        // Sempre <= StoreSize
        public int CurrStoreCount()
        {
            return _store.Count();
        }

        public void StoreItem(TKey k, TVal val)
        {
            if (_store.ContainsKey(k))
            {
                // La chiave già esiste, sotituisco con l'eventuale nuovo valore
                _store[k] = val;
            }
            else if (_store.Count < _storeSize)
            {
                // Ancora non abbiamo riempito tutti gli elementi, aggiungiamo il nuovo:
                _store[k] = val;
                _keyOrderList.Add(k);
            }
            else
            {
                // Siamo pieni, sostituiamone uno, li
                // sostituiamo in sequenza
                _store.Remove(_keyOrderList[_currIndex]);
                _keyOrderList[_currIndex] = k;
                _store[k] = val;
                _currIndex = (_currIndex + 1) % _storeSize;
                // N.B.: Adesso _store.Count == _storeSize == _keyOrderList.Count
            }
        }

        public bool ContainsKey(TKey key)
        {
            return _store.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TVal value)
        {
            return _store.TryGetValue(key, out value);
        }
    }
}
