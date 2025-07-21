using System;
using UnityEngine;

namespace BaseScripts
{
    public class TableManager : MonoBehaviour
    {
        [SerializeField] private Transform seat1;
        [SerializeField] private Transform seat2;

        public Transform bookPoint1; 
        public Transform bookPoint2;

        private bool seat1Taken = false;
        private bool seat2Taken = false;

        public bool book1Spawned = false;
        public bool book2Spawned = false;
        
        
        public bool HasFreeSeat => 
            (!seat1Taken && !book1Spawned) || (!seat2Taken && !book2Spawned);

        public bool CanMobSit()
        {
            return HasFreeSeat;
        }

        public bool TrySits(out Transform seatPoint, out int seatIndex)
        {
            if (!seat1Taken && !book1Spawned)
            {
                seat1Taken = true;
                seatPoint = seat1;
                seatIndex = 1;
                return true;
            }

            if (!seat2Taken && !book2Spawned)
            {
                seat2Taken = true;
                seatPoint = seat2;
                seatIndex = 2;
                return true;
            }

            seatPoint = null;
            seatIndex = -1;
            return false;
        }

        public void SpawnBookOnTable(Book book)
        {
            if (!book1Spawned)
            { 
                Book bookOnTable = Instantiate(book, bookPoint1.position, Quaternion.identity, bookPoint1);
                bookOnTable.transform.localScale = new Vector3(1.37f,15.79f,1.26706803f);
                bookOnTable.transform.localPosition = Vector3.zero;
                
                bookOnTable.transform.GetChild(0).transform.localScale = Vector3.one;
                bookOnTable.transform.GetChild(0).transform.localPosition = Vector3.zero;
                BoxCollider box = bookOnTable.GetComponent<BoxCollider>();

                if (box != null)
                {
                    box.center = new Vector3(-0.1639268f, 0, 0.07416546f);
                    box.size = new Vector3(1.474331f, 0.05999999f, 0.6609165f);
                }
                
                else
                    Debug.LogWarning("BoxCollider не найден на объекте " + bookOnTable.name);
                
                book1Spawned = true;
                return;
            }

            if (!book2Spawned)
            {
                Book bookOnTable = Instantiate(book, bookPoint2.position, Quaternion.identity, bookPoint2);
                bookOnTable.transform.localScale = new Vector3(1.37f, 15.79f, 1.26706803f);
                bookOnTable.transform.localPosition = Vector3.zero;
                
                bookOnTable.transform.GetChild(0).transform.localScale = Vector3.one;
                bookOnTable.transform.GetChild(0).transform.localPosition = Vector3.zero;
                BoxCollider box = bookOnTable.GetComponent<BoxCollider>();

                if (box != null)
                {
                    box.center = new Vector3(-0.1639268f, 0, 0.07416546f);
                    box.size = new Vector3(1.474331f, 0.05999999f, 0.6609165f);
                }
                
                else
                    Debug.LogWarning("BoxCollider не найден на объекте " + bookOnTable.name);

                book2Spawned = true;
                return;
            }

            throw new Exception();
        }
        

        public void RemoveBookFromSeat(int seatIndex)
        {
            if (seatIndex == 1)
                book1Spawned = false;
            else if (seatIndex == 2)
                book2Spawned = false;
        }

        public void ReleaseSeats()
        {
            seat1Taken = false;
            seat2Taken = false;
        }
    }
}
