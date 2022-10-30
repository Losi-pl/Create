using System.Collections;
using System.Diagnostics;



[DebuggerDisplay("{DebuggerDisplay,nq}")]
[DebuggerTypeProxy(typeof(DictionaryView<,>.Proxy))]
class DictionaryView<TKey, TValue>
{
    public IDictionary<TKey, TValue> dictionary;

    public DictionaryView(IDictionary<TKey, TValue> dictionary)
    {
        this.dictionary = dictionary;
    }

    private string DebuggerDisplay => "Count = " + dictionary.Count;

    private class Proxy
    {
        private DictionaryView<TKey, TValue> myhashtable;
        public Proxy(DictionaryView<TKey, TValue> myhashtable)
        {
            this.myhashtable = myhashtable;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePairs[] Keys
        {
            get
            {
                KeyValuePairs[] keys = new KeyValuePairs[myhashtable.dictionary.Count];

                int i = 0;
                foreach (TKey key in myhashtable.dictionary.Keys)
                {
                    keys[i] = new KeyValuePairs(myhashtable.dictionary, key, myhashtable.dictionary[key]);
                    i++;
                }
                return keys;
            }
        }
    }

    [DebuggerDisplay("{value}", Name = "{key}")]
    class KeyValuePairs
    {
        private IDictionary<TKey, TValue> dictionary;
        private TKey key;
        private TValue value;
        public KeyValuePairs(IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            this.value = value;
            this.key = key;
            this.dictionary = dictionary;
        }

        public TKey Key
        {
            get { return key; }
            set
            {
                TValue tempValue = dictionary[key]!;
                dictionary.Remove(key);
                key = value;
                dictionary.Add(key, tempValue);
            }
        }

        public TValue Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                dictionary[key] = this.value;
            }
        }
    }
}

[DebuggerDisplay("{DebuggerDisplay,nq}")]
[DebuggerTypeProxy(typeof(ReadOnlyDictionaryView<,>.Proxy))]
class ReadOnlyDictionaryView<TKey, TValue>
{
    public IDictionary<TKey, TValue> dictionary;

    public ReadOnlyDictionaryView(IDictionary<TKey, TValue> dictionary)
    {
        this.dictionary = dictionary;
    }

    private string DebuggerDisplay { get { return "Count = " + dictionary.Count; } }

    private class Proxy
    {
        private ReadOnlyDictionaryView<TKey, TValue> dictionary;
        public Proxy(ReadOnlyDictionaryView<TKey, TValue> dic)
        {
            this.dictionary = dic;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePairs[] Keys
        {
            get
            {
                KeyValuePairs[] keys = new KeyValuePairs[dictionary.dictionary.Count];

                int i = 0;
                foreach (TKey key in dictionary.dictionary.Keys)
                {
                    keys[i] = new KeyValuePairs(key!, dictionary.dictionary[key]);
                    i++;
                }
                return keys;
            }
        }
    }

    [DebuggerDisplay("{value}", Name = "{key}")]
    class KeyValuePairs
    {
        private TKey key;
        private TValue value;
        public KeyValuePairs(TKey key, TValue value)
        {
            this.value = value;
            this.key = key;
        }

        public TKey Key => key;

        public TValue Value => value;
    }
}