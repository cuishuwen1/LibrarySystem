using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    // Task1
    public enum ItemType
    {
        Novels,
        Magazine,
        TextBook
    }

    public abstract class LibraryItem
    {
        private readonly int id;
        private readonly string title;
        public ItemType ItemType { get; }

        public int Id => id;
        public string Title => title;

        public LibraryItem(int id, string title, ItemType itemType)
        {
            this.id = id;
            this.title = title;
            this.ItemType = itemType;
        }

        public abstract string GetDetails();
    }

    // Task2
    public class Novel : LibraryItem
    {
        private readonly string author;
        public string Author => author;

        public Novel(int id, string title, string author)
            : base(id, title, ItemType.Novels)
        {
            this.author = author;
        }

        public override string GetDetails()
        {
            return $"Novel: {Title}, Author: {Author}";
        }
    }

    // Task3
    public class Magazine : LibraryItem
    {
        private readonly int issueNumber;
        public int IssueNumber => issueNumber;

        public Magazine(int id, string title, int issueNumber)
            : base(id, title, ItemType.Magazine)
        {
            this.issueNumber = issueNumber;
        }

        public override string GetDetails()
        {
            return $"Magazine: {Title}, Issue Number: {IssueNumber}";
        }
    }

    // Task4
    public class TextBook : LibraryItem
    {
        private readonly string publisher;
        public string Publisher => publisher;

        public TextBook(int id, string title, string publisher)
            : base(id, title, ItemType.TextBook)
        {
            this.publisher = publisher;
        }

        public override string GetDetails()
        {
            return $"TextBook: {Title}, Publisher: {Publisher}";
        }
    }

    // Task5
    public class Member
    {
        private readonly string name;
        public string Name => name;

        private List<LibraryItem> borrowedItems = new List<LibraryItem>();

        public Member(string name)
        {
            this.name = name;
        }

        public string BorrowItem(LibraryItem item)
        {
            if (borrowedItems.Count >= 3)
            {
                return "You cannot borrow more than 3 items.";
            }
            borrowedItems.Add(item);
            return $"{item.Title} has been borrowed by {Name}.";
        }

        public string ReturnItems(LibraryItem item)
        {
            if (borrowedItems.Contains(item))
            {
                borrowedItems.Remove(item);
                return $"{item.Title} has been returned by {Name}.";
            }
            else
            {
                return $"{item.Title} is not in {Name}'s borrowed items.";
            }
        }

        public List<LibraryItem> GetBorrowedItems()
        {
            return borrowedItems;
        }
    }

    // Task6
    public class LibraryManager
    {
        private List<LibraryItem> catalog = new List<LibraryItem>();
        private List<Member> members = new List<Member>();

        public void AddItem(LibraryItem item)
        {
            catalog.Add(item);
        }

        public void RegisterMember(Member member)
        {
            members.Add(member);
        }

        public void ShowCatalog()
        {
            Console.WriteLine("Library Catalog:");
            foreach (var item in catalog)
            {
                Console.WriteLine(item.GetDetails());
            }
        }

        public LibraryItem? FindItemById(int id) => catalog.Find(i => i.Id == id);

        public Member? FindMemberByName(string name) => members.Find(m => m.Name == name);
    }

    // Task7
    class Program
    {
        static void Main(string[] args)
        {
            LibraryManager library = new LibraryManager();

            
            library.AddItem(new Novel(1, "三体", "刘慈欣"));
            library.AddItem(new Magazine(2, "科学美国人", 202309));
            library.AddItem(new TextBook(3, "高等数学", "高教出版社"));

            
            Member alice = new Member("Alice");
            Member bob = new Member("Bob");

            
            library.RegisterMember(alice);
            library.RegisterMember(bob);

            
            library.ShowCatalog();

            
            for (int i = 1; i <= 3; i++)
            {
                var item = library.FindItemById(i);
                if (item != null)
                {
                    Console.WriteLine(alice.BorrowItem(item));
                }
            }

            
            var newNovel = new Novel(4, "活着", "余华");
            Console.WriteLine(alice.BorrowItem(newNovel));
        }
    }
}