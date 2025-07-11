using System.Collections;
using UnityEngine;

namespace BaseScripts
{
    public class PlayerReceptionDetected : MonoBehaviour
    {
        [SerializeField] private ReceptionManager receptionManager;

        private Coroutine waitCoroutine;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() != null)
            {
                if (waitCoroutine == null)
                    waitCoroutine = StartCoroutine(WaitAndGiveBook());
                
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player>() != null)
            {
                if (waitCoroutine != null)
                {
                    StopCoroutine(waitCoroutine);
                    waitCoroutine = null;
                }
            }
        }
        
        private IEnumerator WaitAndGiveBook()
        {
            yield return new WaitForSeconds(3f);
            receptionManager.playerGiveBook = true;
            waitCoroutine = null;
        }
    }
}