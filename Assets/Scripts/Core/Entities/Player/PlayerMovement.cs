using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Minecraft.Core.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float walkingSpeed = 5, sprintSpeed = 8;

        private PlayerActions playerActions;
        private CharacterController characterController;

        private float currentSpeed;

        private void OnEnable()
        {
            if(playerActions == null)
            {
                playerActions = new PlayerActions();
                playerActions.Movement.Sprint.started += OnSprintStarted;
                playerActions.Movement.Sprint.canceled += OnSprintEnded;
            }

            playerActions.Enable();
        }

        private void OnDisable()
        {
            playerActions.Disable();
        }

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            currentSpeed = walkingSpeed;
        }

        void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 input = playerActions.Movement.Move.ReadValue<Vector2>();
            Vector3 direction = new Vector3(input.x, 0.0f, input.y);

            Quaternion currentRotation = transform.rotation;
            direction = currentRotation * direction;

            _ = characterController.Move(direction * currentSpeed * Time.deltaTime);
        }

        private void OnSprintStarted(CallbackContext context)
        {
            currentSpeed = sprintSpeed;
        }

        private void OnSprintEnded(CallbackContext context)
        {
            currentSpeed = walkingSpeed;
        }
    }
}
