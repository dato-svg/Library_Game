using TMPro;
using UnityEngine;

namespace BaseScripts
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private int money;
        [SerializeField] private TextMeshProUGUI moneyText;

        public int Money => money;
        
        private void Start() => 
            ShowMoney();

        public void AddMoney(int amount)
        {
            money += amount;
            Debug.Log("money added :" + money);
            ShowMoney();
        }
        
        public void SpendMoney(int amount)
        {
            money -= amount;
            Debug.Log("money Spend :" + money);
            ShowMoney();
        }

        private void ShowMoney() => 
            moneyText.text = money.ToString();
    }
}
