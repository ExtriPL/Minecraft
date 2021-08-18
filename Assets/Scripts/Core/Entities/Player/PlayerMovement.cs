using Mirror;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Minecraft.Core.Entities.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField]
        private float walkingSpeed = 5, sprintSpeed = 8;

        private PlayerActions playerActions;
        private CharacterController characterController;
        private PlayerAnimationController animationController;

        private float currentSpeed;

        [SyncVar]
        private bool isSprinting;
        public bool IsSprinting => isSprinting;
        [SyncVar]
        private bool isJumping;
        public bool IsJumping => isJumping;
        [SyncVar]
        private bool isCrouching;
        public bool IsCrouching => isCrouching;

        private void OnEnable()
        {
            if(playerActions == null)
                playerActions = new PlayerActions();

            playerActions.Enable();
        }

        public override void OnStartClient()
        {
            if (!hasAuthority)
                return;

            playerActions.Movement.Sprint.started += OnSprintStarted;
            playerActions.Movement.Sprint.canceled += OnSprintEnded;

            playerActions.Movement.Jump.started += OnJumpStarted;
            playerActions.Movement.Jump.canceled += OnJumpEnded;

            playerActions.Movement.Crouch.started += OnCrouchStarted;
            playerActions.Movement.Crouch.canceled += OnCrouchEnded;
        }

        private void OnDisable()
        {
            playerActions.Disable();
        }

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animationController = GetComponent<PlayerAnimationController>();
            currentSpeed = walkingSpeed;
        }

        void Update()
        {
            if (!hasAuthority)
                return;

            Move();
            Jump();
        }

        private void Move()
        {
            Vector2 input = playerActions.Movement.Move.ReadValue<Vector2>();
            Vector3 direction = new Vector3(input.x, 0.0f, input.y);

            Quaternion currentRotation = transform.rotation;
            direction = currentRotation * direction;

            _ = characterController.Move(direction * currentSpeed * Time.deltaTime);

            animationController.SetWalking(direction.sqrMagnitude != 0);
        }

        private void Jump()
        {
            if(IsJumping && characterController.isGrounded)
            {

            }
        }

        private void OnSprintStarted(CallbackContext context)
        {
            currentSpeed = sprintSpeed;
            animationController.SetRunning(true);
            CmdSetSprintState(true);
        }

        private void OnSprintEnded(CallbackContext context)
        {
            currentSpeed = walkingSpeed;
            animationController.SetRunning(false);
            CmdSetSprintState(false);
        }

        private void OnJumpStarted(CallbackContext context)
        {
            CmdSetJumping(true);
        }

        private void OnJumpEnded(CallbackContext context)
        {
            CmdSetJumping(false);
        }

        private void OnCrouchStarted(CallbackContext context)
        {
            isCrouching = true;
            animationController.SetCrouching(true);
            CmdSetCrouching(true);
        }

        private void OnCrouchEnded(CallbackContext context)
        {
            isCrouching = false;
            animationController.SetCrouching(false);
            CmdSetCrouching(false);
        }

        [Command]
        private void CmdSetSprintState(bool state)
        {
            isSprinting = state;
        }

        [Command]
        private void CmdSetJumping(bool state)
        {
            isJumping = state;
        }

        [Command]
        private void CmdSetCrouching(bool state)
        {
            isCrouching = state;
        }
    }
}
