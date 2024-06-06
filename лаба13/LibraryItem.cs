using System;
using Library;
using lab12dot7;
namespace Library
{
    public abstract class LibraryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        protected LibraryItem() { }

        protected LibraryItem(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Название: {Title}";
        }

        public static LibraryItem CreateRandomItem()
        {
            Random rand = new Random();
            if (rand.Next(2) == 0)
            {
                return new Book(rand.Next(1000), $"Книга{rand.Next(100)}", $"Автор{rand.Next(50)}");
            }
            else
            {
                return new Journal(rand.Next(1000), $"Журнал{rand.Next(100)}", rand.Next(1, 12));
            }
        }

        public static implicit operator LibraryItem(lab12dot7.Journal v)
        {
            throw new NotImplementedException();
        }
    }

    public class Book : LibraryItem
    {
        public string Author { get; set; }

        public Book() { }

        public Book(int id, string title, string author) : base(id, title)
        {
            Author = author;
        }

        public override string ToString()
        {
            return base.ToString() + $", Автор: {Author}";
        }
    }

    public class Journal : LibraryItem
    {
        public int IssueNumber { get; set; }

        public Journal() { }

        public Journal(int id, string title, int issueNumber) : base(id, title)
        {
            IssueNumber = issueNumber;
        }

        public override string ToString()
        {
            return base.ToString() + $", Номер выпуска: {IssueNumber}";
        }
    }
}