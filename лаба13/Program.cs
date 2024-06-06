using System;
using Library;
using lab12dot7;
namespace lab12dot7
{
    class Program
    {
        static void Main(string[] args)
        {
            MyObservableCollection<int, LibraryItem> collection1 = new MyObservableCollection<int, LibraryItem>();
            MyObservableCollection<int, LibraryItem> collection2 = new MyObservableCollection<int, LibraryItem>();

            Journal journal1 = new Journal();
            Journal journal2 = new Journal();

            collection1.CollectionCountChanged += journal1.OnCollectionCountChanged;
            collection1.CollectionReferenceChanged += journal1.OnCollectionReferenceChanged;
            collection2.CollectionReferenceChanged += journal2.OnCollectionReferenceChanged;

            collection1.Add(1, new Book(1, "Книга1", "Автор1"));
            collection1.Add(2, new Journal(2, "Журнал1", 1));
            collection2.Add(3, new Book(3, "Книга2", "Автор2"));
            collection2.Add(4, new Journal(4, "Журнал2", 2));

            collection1.Remove(1);
            collection2[3] = new Journal(3, "Журнал3", 3);

            Console.WriteLine("Журнал 1:");
            Console.WriteLine(journal1.ToString());
            Console.WriteLine("Журнал 2:");
            Console.WriteLine(journal2.ToString());
        }
    }
}
