using System.Collections;
using UnityEngine;

namespace BaseScripts
{
    public class PlayerMoneyDetected : MonoBehaviour
    {
        public Collider collider;
        
        [SerializeField] private int totalMoney;
        
        [SerializeField] private int giveMoney = 20;

        [SerializeField] private Wallet wallet;
        
        [SerializeField] private GameObject moneyPrefab_1;
        [SerializeField] private GameObject moneyPrefab_2;
        [SerializeField] private GameObject moneyPrefab_3;
        [SerializeField] private GameObject moneyPrefab_4;
        
        
        
        private Coroutine coroutine;


        private void Start()
        {
            collider =  GetComponent<Collider>();
            wallet = FindObjectOfType<Wallet>();
            
            collider.enabled = false;
            
            moneyPrefab_1.SetActive(false);
            moneyPrefab_2.SetActive(false);
            moneyPrefab_3.SetActive(false);
            moneyPrefab_4.SetActive(false);
        }


        public void ActiveCoroutine()
        {
            coroutine = StartCoroutine(TakeMoney());
        }

        private void DeactivateCoroutine()
        { 
            StopCoroutine(coroutine);
            coroutine = null;
        }
        
        private IEnumerator TakeMoney()
        {
            yield return null;
            
            totalMoney += giveMoney;

            if (totalMoney >= 10 && totalMoney < 40)
            {
                moneyPrefab_1.SetActive(true);
                moneyPrefab_2.SetActive(false);
                moneyPrefab_3.SetActive(false);
                moneyPrefab_4.SetActive(false);
                
                DeactivateCoroutine();
                yield return null;
            }
            
            if (totalMoney >= 60 && totalMoney < 80)
            {
                moneyPrefab_1.SetActive(true);
                moneyPrefab_2.SetActive(true);
                moneyPrefab_3.SetActive(false);
                moneyPrefab_4.SetActive(false);
                
                DeactivateCoroutine();
                yield return null;
            }
            
            if (totalMoney >= 80 && totalMoney < 100)
            {
                moneyPrefab_1.SetActive(true);
                moneyPrefab_2.SetActive(true);
                moneyPrefab_3.SetActive(true);
                moneyPrefab_4.SetActive(false);
                
                DeactivateCoroutine();
                yield return null;
            }
            
            if (totalMoney >= 130 && totalMoney <= 999)
            {
                moneyPrefab_1.SetActive(true);
                moneyPrefab_2.SetActive(true);
                moneyPrefab_3.SetActive(true);
                moneyPrefab_4.SetActive(true);
                
                DeactivateCoroutine();
                yield return null;
            }
            
        }


        private void OnTriggerEnter(Collider other)
        {
            wallet.AddMoney(totalMoney);
            totalMoney = 0;
            collider.enabled = false;
            moneyPrefab_1.SetActive(false);
            moneyPrefab_2.SetActive(false);
            moneyPrefab_3.SetActive(false);
            moneyPrefab_4.SetActive(false);
        }
    }
}
