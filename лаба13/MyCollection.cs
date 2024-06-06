using System;
using System.Collections;
using System.Collections.Generic;
using Library;
using lab12dot7;
namespace lab12dot7
{
    public class MyCollection<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        private const int DefaultCapacity = 10;
        private KeyValuePair<TKey, TValue>?[] _items;
        private int _count;

        public MyCollection()
        {
            _items = new KeyValuePair<TKey, TValue>?[DefaultCapacity];
            _count = 0;
        }

        public MyCollection(int capacity)
        {
            _items = new KeyValuePair<TKey, TValue>?[capacity];
            _count = 0;
        }

        public MyCollection(MyCollection<TKey, TValue> c)
        {
            _items = new KeyValuePair<TKey, TValue>?[c._items.Length];
            _count = c._count;
            Array.Copy(c._items, _items, c._items.Length);
        }

        public void Add(TKey key, TValue value)
        {
            if (_count == _items.Length)
                Resize();

            int index = GetIndex(key);
            while (_items[index].HasValue)
            {
                index = (index + 1) % _items.Length;
            }
            _items[index] = new KeyValuePair<TKey, TValue>(key, value);
            _count++;
        }

        public bool Remove(TKey key)
        {
            int index = GetIndex(key);
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[index].HasValue && EqualityComparer<TKey>.Default.Equals(_items[index].Value.Key, key))
                {
                    _items[index] = null;
                    _count--;
                    return true;
                }
                index = (index + 1) % _items.Length;
            }
            return false;
        }

        private void Resize()
        {
            var newItems = new KeyValuePair<TKey, TValue>?[_items.Length * 2];
            foreach (var item in _items)
            {
                if (item.HasValue)
                {
                    int index = GetIndex(item.Value.Key, newItems.Length);
                    while (newItems[index].HasValue)
                    {
                        index = (index + 1) % newItems.Length;
                    }
                    newItems[index] = item;
                }
            }
            _items = newItems;
        }

        private int GetIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % _items.Length;
        }

        private int GetIndex(TKey key, int length)
        {
            return Math.Abs(key.GetHashCode()) % length;
        }

        public int Count => _count;
        public bool IsReadOnly => false;
        public void Clear() => _items = new KeyValuePair<TKey, TValue>?[_items.Length];

        public bool ContainsKey(TKey key)
        {
            int index = GetIndex(key);
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[index].HasValue && EqualityComparer<TKey>.Default.Equals(_items[index].Value.Key, key))
                    return true;
                index = (index + 1) % _items.Length;
            }
            return false;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int index = GetIndex(item.Key);
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[index].HasValue && EqualityComparer<KeyValuePair<TKey, TValue>>.Default.Equals(_items[index].Value, item))
                    return true;
                index = (index + 1) % _items.Length;
            }
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = GetIndex(item.Key);
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[index].HasValue && EqualityComparer<KeyValuePair<TKey, TValue>>.Default.Equals(_items[index].Value, item))
                {
                    _items[index] = null;
                    _count--;
                    return true;
                }
                index = (index + 1) % _items.Length;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = GetIndex(key);
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[index].HasValue && EqualityComparer<TKey>.Default.Equals(_items[index].Value.Key, key))
                {
                    value = _items[index].Value.Value;
                    return true;
                }
                index = (index + 1) % _items.Length;
            }
            value = default(TValue);
            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (TryGetValue(key, out var value))
                    return value;
                throw new KeyNotFoundException();
            }
            set
            {
                if (ContainsKey(key))
                {
                    int index = GetIndex(key);
                    for (int i = 0; i < _items.Length; i++)
                    {
                        if (_items[index].HasValue && EqualityComparer<TKey>.Default.Equals(_items[index].Value.Key, key))
                        {
                            _items[index] = new KeyValuePair<TKey, TValue>(key, value);
                            return;
                        }
                        index = (index + 1) % _items.Length;
                    }
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                var keys = new List<TKey>();
                foreach (var item in _items)
                {
                    if (item.HasValue)
                        keys.Add(item.Value.Key);
                }
                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var values = new List<TValue>();
                foreach (var item in _items)
                {
                    if (item.HasValue)
                        values.Add(item.Value.Value);
                }
                return values;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var item in _items)
            {
                if (item.HasValue)
                    yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public void Add(int v, KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (var item in _items)
            {
                if (item.HasValue)
                {
                    array[arrayIndex] = item.Value;
                    arrayIndex++;
                }
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }
    }
}
