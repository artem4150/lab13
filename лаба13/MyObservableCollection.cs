using System;
using System.Collections.Generic;
using Library;
using lab12dot7;
namespace lab12dot7
{
    public class MyObservableCollection<TKey, TValue> : MyCollection<TKey, TValue>
    {
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        public MyObservableCollection() : base() { }
        public MyObservableCollection(int capacity) : base(capacity) { }
        public MyObservableCollection(MyCollection<TKey, TValue> c) : base(c) { }

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Добавлен", key, value));
        }

        public new bool Remove(TKey key)
        {
            bool result = base.Remove(key);
            if (result)
            {
                CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Удален", key, default(TValue)));
            }
            return result;
        }

        internal void Add(int v, Journal journal)
        {
            throw new NotImplementedException();
        }

        public new TValue this[TKey key]
        {
            get => base[key];
            set
            {
                base[key] = value;
                CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs("Обновлен", key, value));
            }
        }
    }

    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class CollectionHandlerEventArgs : EventArgs
    {
        private string v;
        private object key;
        private object value;

        public string ChangeType { get; set; }
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public CollectionHandlerEventArgs(string changeType, TKey key, TValue value)
        {
            ChangeType = changeType;
            Key = key;
            Value = value;
        }

        public CollectionHandlerEventArgs(string v, object key, object value)
        {
            this.v = v;
            this.key = key;
            this.value = value;
        }

        public override string ToString()
        {
            return $"{ChangeType}: Ключ = {Key}, Значение = {Value}";
        }
    }

    public class TValue
    {
    }
}