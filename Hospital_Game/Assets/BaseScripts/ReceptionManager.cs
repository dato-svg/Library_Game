using System.Collections;
using System.Collections.Generic;
using BaseScripts.NavMovement;
using UnityEngine;

namespace BaseScripts
{
    public class ReceptionManager : MonoBehaviour
    {
        [SerializeField] private Transform receptionPoint;

        public List<LibraryManager> libraryManagers;
        public List<TableManager> tableManagers;

        public List<Readers> readers;

        [SerializeField] public bool playerGiveBook;

        [SerializeField] private Wallet wallet;

        [SerializeField] private List<PlayerMoneyDetected> tableMoneyDetectors;

        [SerializeField] private PlayerMoneyDetected receptionMoneyDetected;

        [SerializeField] private ReadersSpawner readersSpawner;

        public bool isReaderWaiting;

        private void Start()
        {
            SetNewReader();
        }

        [ContextMenu("SetNewReader")]
        public void SetNewReader()
        {
            playerGiveBook = false;
            isReaderWaiting = false;

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
                isReaderWaiting = true;
                StartCoroutine(TryReadProcess(reader));
            });
        }

        private IEnumerator TryReadProcess(Readers reader)
        {
            while (!playerGiveBook)
                yield return null;

            TableManager targetTable = null;
            Transform targetSeat = null;

            foreach (var table in tableManagers)
            {
                if (!table.book1Spawned && !table.book2Spawned && table.TrySits(out Transform seat, out int seatIndex))
                {
                    targetTable = table;
                    targetSeat = seat;
                    break;
                }
            }

            if (targetTable == null)
            {
                Debug.LogWarning("Нет свободных столов. Читатель уходит.");

                var movement = reader.GetComponent<ReadersMovement>();
                if (movement != null)
                {
                    Vector3 exitPoint = new Vector3(-15.79f, 0, 17.98f);

                    yield return ShowReaderReaction(reader);

                    movement.MoveTo(exitPoint, () =>
                    {
                        Destroy(movement.gameObject);
                        readersSpawner.canSpawn = true;
                        readersSpawner.ActiveCo();
                        Debug.Log("Читатель ушёл, так как не было свободных столов.");
                    });
                }

                yield break;
            }

            Book foundBook = null;
            LibraryManager sourceLibrary = null;

            foreach (var library in libraryManagers)
            {
                if (library.TryGetBook(reader.DesiredBook, out Book book))
                {
                    foundBook = book;
                    sourceLibrary = library;
                    break;
                }
            }

            if (foundBook == null)
            {
                Debug.LogWarning($"Книга {reader.DesiredBook} не найдена ни в одной библиотеке.");
                reader.Wait();

                yield return ShowReaderReaction(reader);

                yield break;
            }

            reader.currentBook = foundBook;

            receptionMoneyDetected.collider.enabled = true;
            receptionMoneyDetected.ActiveCoroutine();

            readersSpawner.canSpawn = true;
            readersSpawner.ActiveCo();

            SetNewReader();

            var move = reader.GetComponent<ReadersMovement>();

            move.MoveTo(sourceLibrary.transform.position, () =>
            {
                reader.currentBook.RemoveFromLibrary();

                move.MoveTo(targetSeat.position, () =>
                {
                    Vector3 lookTarget = targetSeat.forward + targetSeat.position;
                    reader.transform.LookAt(lookTarget);

                    reader.ReadBook();
                    StartCoroutine(ReaderFinishRoutine(reader, targetTable));
                });
            });
        }

        private IEnumerator ReaderFinishRoutine(Readers reader, TableManager table)
        {
            yield return new WaitForSeconds(3f);

            int tableIndex = tableManagers.IndexOf(table);
            if (tableIndex >= 0 && tableIndex < tableMoneyDetectors.Count)
            {
                var detector = tableMoneyDetectors[tableIndex];
                detector.collider.enabled = true;
                detector.ActiveCoroutine();
            }
            else
            {
                Debug.LogWarning("Не найдена зона оплаты для данного стола.");
            }

            table.ReleaseSeats();

            if (reader.currentBookSpawn != null)
            {
                table.SpawnBookOnTable(reader.currentBookSpawn);
                reader.currentBook = null;
            }

            var move = reader.GetComponent<ReadersMovement>();
            if (move != null)
            {
                Vector3 exitPoint = new Vector3(-10.96f, 1.53f, 19.04f);
                move.MoveTo(exitPoint, () =>
                {
                    Destroy(move.gameObject);
                    Debug.Log("Читатель ушёл.");
                });
            }
        }

        private IEnumerator ShowReaderReaction(Readers reader)
        {
            Transform child = reader.transform.GetChild(0);

            if (child != null)
            {
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(1f);
                child.gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
