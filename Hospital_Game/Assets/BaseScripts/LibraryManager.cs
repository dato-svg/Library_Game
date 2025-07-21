using System.Collections.Generic;
using UnityEngine;

namespace BaseScripts
{
    public class LibraryManager : MonoBehaviour
    {
        [SerializeField] private List<Book> books;
        
        public List<Transform> shelfSpawnsPoint;
        
        public BooksEnum libraryBookType;

        private void Start() => 
            UpdateBooksList();

        public void AddAllBooks() => 
            UpdateBooksList();

        private void UpdateBooksList()
        {
            books = new List<Book>();

            foreach (var book in GetComponentsInChildren<Book>())
            {
                if (book == null) continue;

                book.SetLibrary(this);
                books.Add(book);
            }
        }

        public void RemoveBook(Book book)
        {
            if (books.Contains(book))
            {
                books.Remove(book);
                Destroy(book.gameObject);
            }
        }

        public bool TryGetBook(BooksEnum desiredBook, out Book book)
        {
            foreach (var b in books)
            {
                if (b.BookType == desiredBook)
                {
                    book = b;
                    return true;
                }
            }

            book = null;
            return false;
        }
    }
}