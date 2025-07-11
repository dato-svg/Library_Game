using System.Collections.Generic;
using UnityEngine;

namespace BaseScripts
{
    public class LibraryManager : MonoBehaviour
    {
        [SerializeField] private List<Book> books;

        private void Start() => 
            books = new List<Book>(GetComponentsInChildren<Book>());


        private void RemoveBook(Book book)
        {
            Destroy(book.gameObject);
            books.Remove(book);
        }

        private void AddBook(Book book) => 
            books.Add(book);
        
        
        public bool TryGetBook(BooksEnum requestedType)
        {
            foreach (var book in books)
            {
                if (book.BookType == requestedType)
                {
                    RemoveBook(book);
                    return true;
                }
            }

            return false;
        }
    }
}