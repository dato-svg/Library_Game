using UnityEngine;

namespace BaseScripts
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int Vert = Animator.StringToHash("Vert");
        private static readonly int State = Animator.StringToHash("State");

       

        public void StartMove()
        {
            animator.SetFloat(Vert,1);
            animator.SetFloat(State,0);
        }

        public void StopMove()
        {
            animator.SetFloat(Vert,0);
            animator.SetFloat(State,2);
        }
    }
}
