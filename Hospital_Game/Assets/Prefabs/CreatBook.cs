using UnityEngine;
using UnityEngine.Rendering;

public class CreatBook : MonoBehaviour
{
    public GameObject book;
    public Transform Table;
    void Start()
    {
        GameObject Book = Instantiate(book);
        Book.transform.SetParent(Table);
        Book.transform.localPosition = new Vector3(0,0,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
