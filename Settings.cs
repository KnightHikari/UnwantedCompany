
namespace UnwantedCompany
{
    public class Settings
    {
        public bool GodMode { get; set; }

        public bool SpamRPCTerminal { get; set; } 

        public bool CallStartGameServerRpc { get; set; }

        public bool CallEndGameServerRpc { get; set; }

        public bool AntiDisconnect { get; set; }

        public bool LateJoin { get; set; }

        public bool PumpkinSpam { get; set; } 

        public bool NoClip { get; set; } 

        public float NoClipSpeed { get; set; } 

        public float SpeedHack { get; set; } 

        public float GrabDistance { get; set; } 

        public float JumpForce { get; set; } 

        public bool ESP { get; set; } 

        public int selectedAudioClip = 0;
        public string[] audioClipOptions = { "Audio Clip 1", "Audio Clip 2", "Audio Clip 3" };

        public GameNetcodeStuff.PlayerControllerB selectedPlayer = null;

        public bool showPlayerMenu = false;

        public bool crosshair = true;

        public bool menuOpen = false;

        private static Settings instance;

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Settings();
                }
                return instance;
            }
        }
    }
}