
namespace UnwantedCompany
{
    public class LocalPlayer
    {
        public static LocalPlayer localPlayer;

        public GameNetcodeStuff.PlayerControllerB PlayerController { get; set; }

        public int Health { get; set; }

        public LocalPlayer(GameNetcodeStuff.PlayerControllerB playerController)
        {
            PlayerController = playerController;
        }

        public static bool IsValid()
        {
            return localPlayer != null &&
                   localPlayer.PlayerController != null &&
                   localPlayer.PlayerController.gameObject != null &&
                   localPlayer.PlayerController.gameObject.GetComponent<GameNetcodeStuff.PlayerControllerB>() != null &&
                   localPlayer.PlayerController.gameObject.GetComponent<GameNetcodeStuff.PlayerControllerB>().isPlayerControlled;
        }
    }
}
