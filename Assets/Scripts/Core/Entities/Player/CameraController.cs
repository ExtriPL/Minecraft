using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minecraft.Core.Entities
{
    public class CameraController : NetworkBehaviour
    {
        [SerializeField]
        private Transform playerCamera;

        public override void OnStartClient()
        {
            if (!hasAuthority)
            {
                Destroy(playerCamera.gameObject);
                return;
            }

            playerCamera.gameObject.SetActive(true);
        }
    }
}
