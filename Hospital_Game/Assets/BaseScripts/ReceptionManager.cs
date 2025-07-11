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
        [SerializeField] private List<Transform> tablePoints;

        [SerializeField] private List<LibraryManager> libraryManagers;
        [SerializeField] private List<TableManager> tableManagers;
        [SerializeField] private List<Readers> readers;

        [SerializeField] private Readers currentReader;
        [SerializeField] public bool playerGiveBook;

        [SerializeField] private Wallet wallet;
        
        private TableManager currentTable;
        

        private void Start()
        {
           
        }

        [ContextMenu("SetNewReader")]
        private void SetNewReader()
        {
            if (readers.Count == 0)
            {
                currentReader = null;
                Debug.Log("Очередь закончилась.");
                return;
            }

            currentReader = readers[0];
            readers.RemoveAt(0);

            var movement = currentReader.GetComponent<ReadersMovement>();
            movement.SetSpeed(3.5f);
            movement.MoveTo(receptionPoint.position, TryReadProcess);
        }

        private void TryReadProcess()
        {
            foreach (var table in tableManagers)
            {
                if (table.TrySits(out Transform seat, out int seatIndex))
                {
                    currentTable = table;
                    
                    StartCoroutine(WaitForBookPermission(seat, currentReader, currentTable));
                    return;
                }
            }

            currentReader.Wait();
        }
        
        private IEnumerator WaitForBookPermission(Transform seat, Readers reader, TableManager table)
        {
            reader.Wait();

            while (!playerGiveBook)
                yield return null;

            SetNewReader();
            foreach (var library in libraryManagers)
            {
                wallet.AddMoney(25);
                if (library.TryGetBook(reader.DesiredBook))
                {
                    var move = reader.GetComponent<ReadersMovement>();

                    move.MoveTo(libraryPoint.position, () =>
                    {
                        move.MoveTo(seat.position, () =>
                        {
                            reader.ReadBook();
                            StartCoroutine(ReaderFinishRoutine(reader, table));
                        });

                        playerGiveBook = false;
                    });

                    playerGiveBook = false;
                    yield break;
                }
            }

            reader.Wait();
        }

        
        private IEnumerator ReaderFinishRoutine(Readers reader, TableManager table)
        {
            yield return new WaitForSeconds(3f);

            table.ReleaseSeats();
            Destroy(reader.gameObject);
        }
        
    }
}
