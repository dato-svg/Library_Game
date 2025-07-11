using BaseScripts.Movement;
using UnityEngine;

namespace BaseScripts
{
    public class Bootstrapper : MonoBehaviour
    {
        private Game game;

        private void Awake()
        {
            game = new Game();
        }
    }
}
