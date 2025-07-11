using System.Collections;
using System.Collections.Generic;
using BaseScripts.NavMovement;
using UnityEngine;

namespace BaseScripts
{
    public class ReceptionManager : MonoBehaviour
    {
        [SerializeField] private Transform receptionPoint;
        [SerializeField] private Transform libraryPoint;
        
        [SerializeField] private List<LibraryManager> libraryManagers;
        [SerializeField] private List<TableManager> tableManagers;
        
        public List<Readers> readers;
        
        [SerializeField] public bool playerGiveBook;
        
        [SerializeField] private Wallet wallet;
        
        [SerializeField] private PlayerMoneyDetected  receptionMoneyDetected;
        [SerializeField] private PlayerMoneyDetected  tableMoneyDetected;

        [SerializeField] private ReadersSpawner readersSpawner;

        private void Start()
        {
            SetNewReader();
        }

        [ContextMenu("SetNewReader")]
        private void SetNewReader()
        {
            playerGiveBook = false;

            if (readers.Count == 0)
            {
                Debug.Log("Очередь закончилась.");
                return;
            }

            Readers reader = readers[0];
            readers.RemoveAt(0);

            var movement = reader.GetComponent<ReadersMovement>();
            movement.SetSpeed(3.5f);

            movement.MoveTo(receptionPoint.position, () =>
            {
                StartCoroutine(TryReadProcess(reader));
            });
        }

        private IEnumerator TryReadProcess(Readers reader)
        {
            while (!playerGiveBook)
                yield return null;
            
            foreach (var table in tableManagers)
            {
                if (table.TrySits(out Transform seat, out int seatIndex))
                {
                    StartCoroutine(HandleReaderFlow(reader, table, seat));
                    yield break;
                }
            }
            
            reader.Wait();
        }

        private IEnumerator HandleReaderFlow(Readers reader, TableManager table, Transform seat)
        {
            reader.Wait();

            foreach (var library in libraryManagers)
            {
                if (library.TryGetBook(reader.DesiredBook))
                {
                    receptionMoneyDetected.collider.enabled = true;
                    receptionMoneyDetected.ActiveCoroutine();

                    SetNewReader();
                    
                    var move = reader.GetComponent<ReadersMovement>();
                    
                    move.MoveTo(libraryPoint.position, () =>
                    {
                       
                        //readersSpawner
                            
                            
                        move.MoveTo(seat.position, () =>
                        {
                            reader.ReadBook();
                            StartCoroutine(ReaderFinishRoutine(reader, table));
                        });
                    });

                    yield break;
                }
            }

            reader.Wait();
        }

        private IEnumerator ReaderFinishRoutine(Readers reader, TableManager table)
        {
            yield return new WaitForSeconds(3f);

            tableMoneyDetected.collider.enabled = true;
            tableMoneyDetected.ActiveCoroutine();
            
            table.ReleaseSeats();

            var move = reader.GetComponent<ReadersMovement>();
            if (move != null)
            {
                Vector3 exitPoint = new Vector3(-15.79f, 0, 17.98f);

                move.MoveTo(exitPoint, () =>
                {
                    Debug.Log("He leaves the table");
                    //  reader.Leave();
                });
            }

            yield return null;
        }

    }
}
