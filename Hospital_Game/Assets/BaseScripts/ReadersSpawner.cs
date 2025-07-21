using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace BaseScripts
{
    public class ReadersSpawner : MonoBehaviour
    {
        public List<GameObject> readerPrefabs;
        [SerializeField] private ReceptionManager receptionManager;

        public bool canSpawn;

        private int currentIndex = 1;

        [ContextMenu("ActiveCo")]
        public void ActiveCo()
        {
            StartCoroutine(SpawnReaders());
        }

        public IEnumerator SpawnReaders()
        {
            yield return null;

            while (!canSpawn)
                yield return null;

            while (canSpawn)
            {
                GameObject prefab = readerPrefabs[currentIndex];

                Readers reader = Instantiate(prefab, transform.position, quaternion.identity)
                    .GetComponent<Readers>();
                
                Transform emotion = reader.transform.GetChild(0);
                if (emotion != null)
                {
                    emotion.gameObject.SetActive(false);
                }

                receptionManager.readers.Add(reader);
                receptionManager.SetNewReader();
                canSpawn = false;

                currentIndex++;
                if (currentIndex >= readerPrefabs.Count)
                    currentIndex = 0;
            }
        }
    }
}