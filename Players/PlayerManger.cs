using GameNetcodeStuff;
using System.Collections.Generic;
using UnityEngine;

namespace UnwantedCompany.Players
{
    public class PlayerManager
    {
        private List<GameNetcodeStuff.PlayerControllerB> _players;

        public PlayerManager()
        {
            _players = new List<GameNetcodeStuff.PlayerControllerB>();
        }

        public void UpdatePlayers()
        {
            _players.Clear();

            if (!LocalPlayer.IsValid()) {
                var playerControllers = GameObject.FindObjectsOfType<GameNetcodeStuff.PlayerControllerB>();
                foreach (var playerController in playerControllers) {
                    if (playerController != null 
                        && playerController.gameObject.GetComponent<PlayerControllerB>().isPlayerControlled && playerController.IsOwner)
                    {
                        LocalPlayer.localPlayer = new LocalPlayer(playerController);
                        break;
                    }
                }
            }

            _players.AddRange(GameObject.FindObjectsOfType<GameNetcodeStuff.PlayerControllerB>());
            _players.RemoveAll(player => !IsValidPlayer(player));
        }

        public IEnumerable<GameNetcodeStuff.PlayerControllerB> GetPlayers()
        {
            return _players;
        }

        bool IsValidPlayer(GameNetcodeStuff.PlayerControllerB player)
        {
            if (player == null || player == LocalPlayer.localPlayer.PlayerController)
            {
                return false;
            }

            if (player.gameObject == null || player.gameObject.GetComponent<GameNetcodeStuff.PlayerControllerB>() == null)
            {
                return false;
            }

            if(player.playerSteamId == 0)
            {
                return false;
            }

            return true;
        }
    }
}
