using UnityEngine;

namespace BaseScripts
{
    public class IteracteObjectActive : MonoBehaviour
    {
        [SerializeField] private GameObject tablePrefab;
        [SerializeField] private GameObject moneyPrefab;
        [SerializeField] private GameObject libraryPrefab;

        [SerializeField] private int activationCost = 10;

        private Wallet wallet;
        private ReceptionManager receptionManager;
        private ReadersSpawner readersSpawner;

        private void Start()
        {
            wallet = FindObjectOfType<Wallet>();
            receptionManager = FindObjectOfType<ReceptionManager>();
            readersSpawner =  FindObjectOfType<ReadersSpawner>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) return;

            if (wallet != null && wallet.Money >= activationCost)
            {
                tablePrefab.SetActive(true);
                moneyPrefab.SetActive(true);
                libraryPrefab.SetActive(true);

                receptionManager.tableManagers.Add(tablePrefab.GetComponent<TableManager>());
                receptionManager.libraryManagers.Add(libraryPrefab.GetComponent<LibraryManager>());
                readersSpawner.
                
                //wallet.SpendMoney(activationCost);
                gameObject.SetActive(false);
            }
        }
    }
}