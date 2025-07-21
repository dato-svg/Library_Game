using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BaseScripts
{
    public class PlayerReceptionDetected : MonoBehaviour
    {
        [SerializeField] private ReceptionManager receptionManager;
        [SerializeField] private Image sliderImage;
        [SerializeField] private GameObject deactivateImage;

        private Coroutine waitCoroutine;
        private bool playerInsideTrigger = false;

        private void Start()
        {
            deactivateImage.SetActive(false);
        }

        private void Update()
        {
            if (playerInsideTrigger && waitCoroutine == null && receptionManager.isReaderWaiting)
            {
                waitCoroutine = StartCoroutine(WaitAndGiveBook());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() != null)
            {
                playerInsideTrigger = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player>() != null)
            {
                playerInsideTrigger = false;

                if (waitCoroutine != null)
                {
                    StopCoroutine(waitCoroutine);
                    waitCoroutine = null;
                }

                sliderImage.fillAmount = 0f;
                deactivateImage.SetActive(false);
            }
        }

        private IEnumerator WaitAndGiveBook()
        {
            deactivateImage.SetActive(true);
            float duration = 1.5f;
            float timer = 0f;
            sliderImage.fillAmount = 0f;

            while (timer < duration)
            {
                if (!playerInsideTrigger)
                {
                    sliderImage.fillAmount = 0f;
                    deactivateImage.SetActive(false);
                    waitCoroutine = null;
                    yield break;
                }

                timer += Time.deltaTime;
                sliderImage.fillAmount = Mathf.Clamp01(timer / duration);
                yield return null;
            }

            deactivateImage.SetActive(false);
            receptionManager.playerGiveBook = true;
            receptionManager.isReaderWaiting = false;
            waitCoroutine = null;
        }
    }
}
