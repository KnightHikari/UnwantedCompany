using HarmonyLib;

namespace UnwantedCompany
{
	[HarmonyPatch(typeof(GameNetworkManager), "Singleton_OnClientDisconnectCallback")]
	public static class Singleton_OnClientDisconnectCallbackPatch
	{
		public static bool Prefix(ulong clientId, GameNetworkManager __instance)
		{
			if (Settings.Instance.AntiDisconnect)
			{
				return false;
			}
			return true;
		}
	}
}