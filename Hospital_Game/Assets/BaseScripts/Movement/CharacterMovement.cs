using UnityEngine;

namespace BaseScripts.Movement
{
    public class CharacterMovement : MonoBehaviour
    {
        public CharacterController controller;
        public float movementSpeed;
        
        private IInputService inputService;
        private new Camera camera;

        private void Awake()
        {
            inputService = Game.inputService;
        }

        private void Start()
        {
            camera = Camera.main;
        }

        private void Update()
        {
            Vector3 moveDirection = Vector3.zero;

            if (inputService.Axis.sqrMagnitude >= 0.1f)
            { 
                moveDirection = camera.transform.TransformDirection(inputService.Axis);
                moveDirection.y = 0;
                moveDirection.Normalize();
                transform.forward = moveDirection;
            }
            moveDirection += Physics.gravity;
            
            controller.Move(moveDirection * movementSpeed * Time.deltaTime);
        }
    }
}
