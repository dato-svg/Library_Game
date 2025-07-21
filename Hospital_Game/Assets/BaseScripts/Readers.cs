using UnityEngine;

namespace BaseScripts
{
    public class Readers : MonoBehaviour
    {
        [SerializeField] private BooksEnum desiredBook;
        
        public Book currentBook;
        public Book currentBookSpawn;

        
        
        public BooksEnum DesiredBook => desiredBook;

        public void ReadBook() => 
            Debug.Log($"Читатель сел читать книгу: {desiredBook}");

        public void Wait()
        {
            Debug.Log($"Читатель ждёт книгу И ЛИВАЕТ АХХАХАХАХАХАХАХА ЛОХИ ПОКА: {desiredBook}");
            
        }
    }
}   