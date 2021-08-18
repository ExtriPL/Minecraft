using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft.Core.Entities.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator playerAnimator, cameraAnimator;

        public void SetRunning(bool state)
        {
            if (playerAnimator != null)
                playerAnimator.SetBool("isRunning", state);

            if (cameraAnimator != null)
                cameraAnimator.SetBool("isRunning", state);
        }

        public void SetCrouching(bool state)
        {
            if (playerAnimator != null)
                playerAnimator.SetBool("isCrouching", state);

            if (cameraAnimator != null)
                cameraAnimator.SetBool("isCrouching", state);
        }

        public void SetWalking(bool state)
        {
            if(playerAnimator != null)
                playerAnimator.SetBool("isWalking", state);

            if(cameraAnimator != null)
                cameraAnimator.SetBool("isWalking", state);
        }
    }
}
