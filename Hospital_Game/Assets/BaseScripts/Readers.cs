using UnityEngine;

namespace BaseScripts
{
    public class Readers : MonoBehaviour
    {
        [SerializeField] private BooksEnum desiredBook;

        public BooksEnum DesiredBook => desiredBook;

        public void ReadBook() => 
            Debug.Log($"Читатель сел читать книгу: {desiredBook}");

        public void Wait() => 
            Debug.Log($"Читатель ждёт книгу: {desiredBook}");
    }
}   