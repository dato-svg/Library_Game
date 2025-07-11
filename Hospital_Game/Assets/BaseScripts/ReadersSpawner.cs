using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BaseScripts
{
    public class ReadersSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> readerPrefabs;
        [SerializeField] private ReceptionManager receptionManager;
        
        [SerializeField] private bool canSpawn;

        private int index;

        [ContextMenu("ActiveCo")]
        private void ActiveCo()
        {
            StartCoroutine(SpawnReaders());
        }

        public IEnumerator SpawnReaders()
        {
            yield return null;
            while (!canSpawn)
            {
                yield return null;
            }

            while (canSpawn)
            {
                index = Random.Range(0, readerPrefabs.Count);
                
                Readers reader = Instantiate(readerPrefabs[index],
                    transform.position, 
                    quaternion.identity)
                    .gameObject.GetComponent<Readers>();
                
                receptionManager.readers.Add(reader);
                canSpawn = false;
            }
        }
    }
}
