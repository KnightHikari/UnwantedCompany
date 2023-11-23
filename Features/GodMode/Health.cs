using GameNetcodeStuff;
using UnityEngine;

namespace UnwantedCompany
{
    public class PlayerControllerHandler
    {
        public void SetLocalPlayerHealth(int health)
        {
            var playerControllers = GameObject.FindObjectsOfType<GameNetcodeStuff.PlayerControllerB>();

            foreach (var playerController in playerControllers)
            {
                if (playerController != null && playerController.gameObject.GetComponent<PlayerControllerB>().isPlayerControlled && playerController.IsOwner)
                {
                    playerController.health = health;
                }
            }
        }
    }
}
