using UnityEngine;

namespace BaseScripts
{
    public class Book : MonoBehaviour
    {
        [SerializeField] private BooksEnum book;

        public BooksEnum BookType => book;
    }
}
