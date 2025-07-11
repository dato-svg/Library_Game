using UnityEngine;

namespace BaseScripts
{
    public class TableManager : MonoBehaviour
    {
        [SerializeField] private Transform seat1;
        [SerializeField] private Transform seat2;

        private bool seat1Taken = false;
        private bool seat2Taken = false;

        public bool HasFreeSeat => !seat1Taken || !seat2Taken;

        public bool TrySits(out Transform seatPoint, out int seatIndex)
        {
            if (!seat1Taken)
            {
                seat1Taken = true;
                seatPoint = seat1;
                seatIndex = 1;
                return true;
            }

            if (!seat2Taken)
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


        public void ReleaseSeats()
        {
            seat1Taken = false;
            seat2Taken = false;
        }
        
       
    }
}