using System.Collections.Generic;
using UnityEngine;

namespace BaseScripts
{
    public class BookCollector : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private string bookTag = "BookOnTable";
        [SerializeField] private float yOffset = 0.3f;

        public List<GameObject> collectedBooks = new List<GameObject>();
        public IReadOnlyList<GameObject> CollectedBooks => collectedBooks;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(bookTag)) return;

            if (collectedBooks.Contains(other.gameObject)) return;

            Book bookOnTable = other.GetComponent<Book>();
            if (bookOnTable == null) return;

            TableManager table = other.GetComponentInParent<TableManager>();
            if (table != null)
            {
                if (bookOnTable.transform.parent == table.bookPoint1)
                {
                    table.RemoveBookFromSeat(1);
                }
                else if (bookOnTable.transform.parent == table.bookPoint2)
                {
                    table.RemoveBookFromSeat(2);
                }
            }
            
            other.transform.SetParent(spawnPoint);
            other.transform.localRotation = Quaternion.identity;

            collectedBooks.Add(other.gameObject);

            RepositionBooks();
        }
        
        
        
        public void RemoveBook(GameObject book)
        {
            if (collectedBooks.Contains(book))
            {
                collectedBooks.Remove(book);
                Destroy(book);
                
                RepositionBooks();
            }
        }
        
        private void RepositionBooks()
        {
            for (int i = 0; i < collectedBooks.Count; i++)
            {
                if (collectedBooks[i] != null)
                {
                    collectedBooks[i].transform.localPosition = new Vector3(0, yOffset * i, 0);
                }
            }
        }
        
    }
}
