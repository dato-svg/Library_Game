using UnityEngine;

namespace BaseScripts
{
    public class LookAtCamera : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
