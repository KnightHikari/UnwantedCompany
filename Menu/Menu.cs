using UnityEngine;
using UnwantedCompany.Features.SpawnEnemy;

namespace UnwantedCompany.Menu
{
    public class MenuClass
    {
        private GameObject directionalLightClone = null;

        public void DrawMenu(int windowID)
        {
            if (Settings.Instance.menuOpen)
            {
                GUILayout.Label("'");

                Settings.Instance.GodMode = GUILayout.Toggle(Settings.Instance.GodMode, "God Mode");
                Settings.Instance.AntiDisconnect = GUILayout.Toggle(Settings.Instance.AntiDisconnect, "Anti Disconnect?");
                Settings.Instance.PumpkinSpam = GUILayout.Toggle(Settings.Instance.PumpkinSpam, "Spam Pumpkin Slap");
                Settings.Instance.SpamRPCTerminal = GUILayout.Toggle(Settings.Instance.SpamRPCTerminal, "Spam Terminal Noise");
                Settings.Instance.LateJoin = GUILayout.Toggle(Settings.Instance.LateJoin, "Late Join");

                GUILayout.Label("Select Audio Clip:");
                Settings.Instance.selectedAudioClip = GUILayout.SelectionGrid(Settings.Instance.selectedAudioClip, Settings.Instance.audioClipOptions, Settings.Instance.audioClipOptions.Length);

                if (GUILayout.Button("Start Ship"))
                {
                    Settings.Instance.CallStartGameServerRpc = true;
                }

                if (GUILayout.Button("Make Ship leave"))
                {
                    Settings.Instance.CallEndGameServerRpc = true;
                }

                if (GUILayout.Button("Teleport to Ship"))
                {
                    StartOfRound StartOfRound = GameObject.FindObjectOfType<StartOfRound>();
                    if (StartOfRound != null)
                    {
                        StartOfRound.ForcePlayerIntoShip();
                    }
                }

                if (GUILayout.Button("Unlimited Money"))
                {
                    Terminal terminal = GameObject.FindObjectOfType<Terminal>();
                    if (terminal != null)
                    {
                        terminal.groupCredits = 999999999;
                    }
                }

                if (GUILayout.Button("NightVision"))
                {
                    GameObject environment = GameObject.Find("Environment");
                    if (environment != null)
                    {
                        Transform testRoom = environment.transform.Find("TestRoom");
                        if (testRoom != null)
                        {
                            Transform directionalLightTest = testRoom.transform.Find("Directional Light Test");
                            if (directionalLightTest != null)
                            {
                                if (directionalLightClone == null)
                                {
                                    directionalLightClone = GameObject.Instantiate(directionalLightTest.gameObject);
                                    directionalLightClone.transform.parent = environment.transform;
                                    directionalLightClone.SetActive(true);
                                }
                                else
                                {
                                    directionalLightClone.SetActive(!directionalLightClone.activeSelf);
                                }
                            }
                        }
                    }
                }

                if (GUILayout.Button("Revive all players (Host)"))
                {
                    StartOfRound StartOfRound = GameObject.FindObjectOfType<StartOfRound>();
                    if (StartOfRound != null)
                    {
                        StartOfRound.ReviveDeadPlayers();
                        StartOfRound.PlayerHasRevivedServerRpc();
                        StartOfRound.AllPlayersHaveRevivedClientRpc();
                    }
                }

                if (GUILayout.Button("Revive self"))
                {
                    StartOfRound StartOfRound = GameObject.FindObjectOfType<StartOfRound>();
                    if (StartOfRound != null)
                    {
                        StartOfRound.PlayerHasRevivedServerRpc();
                    }
                }

                if (GUILayout.Button("Waiting for crew sync fix"))
                {
                    RoundManager roundManager = GameObject.FindObjectOfType<RoundManager>();
                    if (roundManager != null)
                    {
                        roundManager.FinishGeneratingNewLevelServerRpc();
                        foreach (var playerController in MainEntry._playerManager.GetPlayers())
                        {
                            roundManager.FinishedGeneratingLevelServerRpc(playerController.playerClientId);
                        }
                    }
                }

                if (GUILayout.Button("Break Game (Ship Only)"))
                {
                    RoundManager roundManager = GameObject.FindObjectOfType<RoundManager>();
                    if (roundManager != null)
                    {
                        roundManager.GenerateNewFloor();
                        roundManager.FinishGeneratingNewLevelServerRpc();
                        foreach (var playerController in MainEntry._playerManager.GetPlayers())
                        {
                            roundManager.FinishedGeneratingLevelServerRpc(playerController.playerClientId);
                        }
                    }
                }

                if (GUILayout.Button("Spawn enemy on localpos (Host)"))
                {
                    RoundManager roundManager = GameObject.FindObjectOfType<RoundManager>();
                    SpawnEnemyClass.SpawnEnemyWithConfigManager("ForestGiant");

                    SpawnableEnemyWithRarity enemy = roundManager.currentLevel.OutsideEnemies[UnityEngine.Random.Range(0, roundManager.currentLevel.OutsideEnemies.Count)];
                    SpawnEnemyClass.SpawnEnemyAtLocalPlayer(enemy, 3);
                }

                Settings.Instance.NoClip = GUILayout.Toggle(Settings.Instance.NoClip, "No Clip");
                GUILayout.Label("Noclip Speed: " + Settings.Instance.NoClipSpeed.ToString());
                Settings.Instance.NoClipSpeed = GUILayout.HorizontalSlider(Settings.Instance.NoClipSpeed, 0.1f, 1.5f);

                GUILayout.Label("Speed Hack: " + Settings.Instance.SpeedHack.ToString());
                Settings.Instance.SpeedHack = GUILayout.HorizontalSlider(Settings.Instance.SpeedHack, 4.6f, 55f);
                if (Settings.Instance.SpeedHack > 4.6f)
                {
                    if (LocalPlayer.IsValid())
                    {
                        LocalPlayer.localPlayer.PlayerController.movementSpeed = Settings.Instance.SpeedHack;
                    }
                }

                GUILayout.Label("Grab Distance: " + Settings.Instance.GrabDistance.ToString());
                Settings.Instance.GrabDistance = GUILayout.HorizontalSlider(Settings.Instance.GrabDistance, 3f, 55f);
                if (Settings.Instance.GrabDistance > 3f) {
                    if (LocalPlayer.IsValid()) {
                        LocalPlayer.localPlayer.PlayerController.grabDistance = Settings.Instance.GrabDistance;
                    }
                }

                GUILayout.Label("Jump Force: " + Settings.Instance.JumpForce.ToString());
                Settings.Instance.JumpForce = GUILayout.HorizontalSlider(Settings.Instance.JumpForce, 13f, 55f);

                if (Settings.Instance.JumpForce > 13f)
                {
                    if (LocalPlayer.IsValid())
                    {
                        LocalPlayer.localPlayer.PlayerController.jumpForce = Settings.Instance.JumpForce;
                    }
                }

                Settings.Instance.ESP = GUILayout.Toggle(Settings.Instance.ESP, "Player ESP");
                {
                    GUILayout.Label("Players:");

                    int playersPerRow = 5;
                    int playerCount = 0;

                    foreach (var playerController in MainEntry._playerManager.GetPlayers())
                    {
                        if (playerCount % playersPerRow == 0)
                        {
                            if (playerCount > 0)
                            {
                                GUILayout.EndHorizontal();
                            }
                            GUILayout.BeginHorizontal();
                        }

                        if (GUILayout.Button(playerController.playerUsername))
                        {
                            Settings.Instance.selectedPlayer = playerController;
                            Settings.Instance.showPlayerMenu = true;
                        }
                        playerCount++;
                    }

                    if (playerCount > 0)
                    {
                        GUILayout.EndHorizontal();
                    }
                }

                GUI.DragWindow();
            }
        }
    }
}
