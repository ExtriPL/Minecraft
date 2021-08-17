using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minecraft.Core.Entities.Player
{
    public class CameraController : NetworkBehaviour
    {
        [SerializeField]
        private Transform playerCamera;
        [SerializeField]
        private float horizontalSensitivity = 9, verticalSensitivity = 5;
        [SerializeField]
        private float verticalRange = 45;

        private PlayerActions playerActions;

        public override void OnStartClient()
        {
            if (!hasAuthority)
            {
                Destroy(playerCamera.gameObject);
                Destroy(this);
                return;
            }

            playerCamera.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            if(playerActions == null)
                playerActions = new PlayerActions();

            playerActions.Enable();
        }

        private void OnDisable()
        {
            playerActions.Disable();
        }

        private void Update()
        {
            if (!hasAuthority)
                return;

            MoveCameraHorizontal();
            MoveCameraVertical();
        }

        private void MoveCameraHorizontal()
        {
            float horizontal = playerActions.Look.Horizontal.ReadValue<float>();
            transform.Rotate(0f, horizontal * horizontalSensitivity * Time.deltaTime, 0f);
        }

        private void MoveCameraVertical()
        {
            float vertical = playerActions.Look.Vertical.ReadValue<float>();

            Vector3 currentRotation = playerCamera.transform.rotation.eulerAngles;
            float rotationX = currentRotation.x;

            rotationX -= vertical * verticalSensitivity * Time.deltaTime;

            if (rotationX > verticalRange && rotationX < 180)
                rotationX = verticalRange;
            else if (rotationX < (360 - verticalRange) && rotationX > 180)
                rotationX = 360 - verticalRange;

            currentRotation.x = rotationX;

            playerCamera.transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
