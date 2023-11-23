using Unity.Netcode;
using HarmonyLib;

namespace UnwantedCompany
{
    [HarmonyPatch(typeof(GameNetworkManager), "ConnectionApproval")]
    [HarmonyWrapSafe]
    internal static class ConnectionApproval_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            if (!Settings.Instance.LateJoin) return;

            if (request.Equals(default) || response.Equals(default)) {
                return;
            }

            if (request.ClientNetworkId == NetworkManager.Singleton.LocalClientId) return;

            if (response.Reason.Contains("Game has already started") && GameNetworkManager.Instance.gameHasStarted) {
                response.Reason = string.Empty;
                response.CreatePlayerObject = false;
                response.Approved = true;
                response.Pending = false;
            }
        }
    }

    [HarmonyPatch(typeof(GameNetworkManager), nameof(GameNetworkManager.LeaveLobbyAtGameStart))]
    [HarmonyWrapSafe]
    internal static class LeaveLobbyAtGameStart_Patch
    {
        [HarmonyPrefix]
        private static bool Prefix() => false;
    }

}