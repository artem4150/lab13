using System;
using System.Collections.Generic;

using Library;
using lab12dot7;
namespace lab12dot7
{
    public class Journal
    {
        private List<JournalEntry> _entries = new List<JournalEntry>();

        public Journal()
        {
        }

        public Journal(int v1, string v2, int v3)
        {
        }

        public void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            _entries.Add(new JournalEntry("Коллекция", args.ChangeType, args.ToString()));
        }

        public void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            _entries.Add(new JournalEntry("Коллекция", args.ChangeType, args.ToString()));
        }

        public override string ToString()
        {
            string result = "Записи журнала:\n";
            foreach (var entry in _entries)
            {
                result += entry.ToString() + "\n";
            }
            return result;
        }
    }

    public class JournalEntry
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public string ItemDetails { get; set; }

        public JournalEntry(string collectionName, string changeType, string itemDetails)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ItemDetails = itemDetails;
        }

        public override string ToString()
        {
            return $"Коллекция: {CollectionName}, Изменение: {ChangeType}, Элемент: {ItemDetails}";
        }
    }
}