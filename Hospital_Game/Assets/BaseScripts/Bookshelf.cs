using System.Collections.Generic;
using UnityEngine;

namespace BaseScripts
{
    public class Bookshelf : MonoBehaviour
    {
        [SerializeField] private Transform playerBookHolder;
        [SerializeField] private string bookTag = "BookOnTable";

        private LibraryManager libraryManager;
        private BookCollector bookCollector;

        private void Awake()
        {
            libraryManager = GetComponent<LibraryManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            if (bookCollector == null)
                bookCollector = other.GetComponentInChildren<BookCollector>();

            var booksToTransfer = new List<Transform>();

            for (int i = 0; i < playerBookHolder.childCount; i++)
            {
                Transform bookTransform = playerBookHolder.GetChild(i);
                if (bookTransform.CompareTag(bookTag))
                {
                    booksToTransfer.Add(bookTransform);
                }
            }

            foreach (Transform bookTransform in booksToTransfer)
            {
                Book book = bookTransform.GetComponent<Book>();
                if (book == null || book.bookForLibrary == null) continue;
                
                if (book.BookType != libraryManager.libraryBookType) continue;

                Transform freeSlot = GetFreeShelfSlot();
                if (freeSlot == null) continue;

                GameObject newBook = Instantiate(book.bookForLibrary, freeSlot.position, Quaternion.identity, freeSlot);
                newBook.GetComponent<BoxCollider>().enabled = false;

                Book newBookComponent = newBook.GetComponent<Book>();
                if (newBookComponent != null)
                    newBookComponent.SetLibrary(libraryManager);
                
                bookCollector?.RemoveBook(bookTransform.gameObject);
            }

            libraryManager.AddAllBooks();
        }

        private Transform GetFreeShelfSlot()
        {
            foreach (var slot in libraryManager.shelfSpawnsPoint)
            {
                if (slot.childCount == 0)
                    return slot;
            }

            return null;
        }
    }
}
