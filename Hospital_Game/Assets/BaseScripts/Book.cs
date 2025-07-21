using UnityEngine;

namespace BaseScripts
{
    public class Book : MonoBehaviour
    {
        public GameObject prefabReference;
        public GameObject bookForLibrary;
        
        [SerializeField] private BooksEnum book;
        
        private LibraryManager library;
        
        private void Awake()
        {
            if (book == BooksEnum.blue)
            {
                prefabReference = Resources.Load<GameObject>($"BookBlueLibrery");
                bookForLibrary =  Resources.Load<GameObject>($"BookBlue");
            }
            
            if (book == BooksEnum.red)
            {
                prefabReference = Resources.Load<GameObject>($"BookRedLibrery");
                bookForLibrary =  Resources.Load<GameObject>($"BookRed");
            }
            
            if (book == BooksEnum.green)
            {
                prefabReference = Resources.Load<GameObject>($"BookGreenLibrery");
                bookForLibrary =  Resources.Load<GameObject>($"BookGreen");
            }
            
            
            
        }

        private void Start() => 
            library = GetComponentInParent<LibraryManager>();

        public BooksEnum BookType => book;
        
        public void SetLibrary(LibraryManager lib) => 
            library = lib;

        public void RemoveFromLibrary() => 
            library?.RemoveBook(this);
    }
}
