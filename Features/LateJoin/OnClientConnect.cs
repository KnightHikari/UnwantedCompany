using System.Reflection;
using System.Collections.Generic;
using Unity.Netcode;
using HarmonyLib;
using System;

namespace UnwantedCompany
{
    [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.OnClientConnect))]
    [HarmonyWrapSafe]
    internal class OnClientConnect_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(ulong clientId)
        {
            StartOfRound sor = StartOfRound.Instance;

            if (Settings.Instance.LateJoin && sor.IsServer && !sor.inShipPhase) {

                Dictionary<uint, string> rpcHandlers = new Dictionary<uint, string>
                {
                    { 1659269112U, "__rpc_handler_1659269112" },
                    { 1193916134U, "__rpc_handler_1193916134" },
                    { 192551691U, "__rpc_handler_192551691" },
                    { 710372063U, "__rpc_handler_710372063" }, 
                    { 2729232387U, "__rpc_handler_2729232387" },
                    { 46494176U, "__rpc_handler_46494176" },
                    { 3840785488U, "__rpc_handler_3840785488" },
                    { 1061166170U, "__rpc_handler_1061166170" }, 
                    { 1586488299U, "__rpc_handler_1586488299" },
                    { 1145714957U, "__rpc_handler_1145714957" }, 
                    { 112447504U, "__rpc_handler_112447504" }, 
                    { 445397880U, "__rpc_handler_445397880" },
                    { 3840203489U, "__rpc_handler_3840203489" }
                };

                if (NetworkManager.__rpc_func_table == null)
                {
                    Console.WriteLine("__rpc_func_table is null");
                    return;
                }

                foreach (var pair in rpcHandlers)
                {
                    MethodInfo rpcHandler = typeof(RoundManager).GetMethod(pair.Value, BindingFlags.NonPublic | BindingFlags.Static);

                    if (rpcHandler == null)
                    {
                        Console.WriteLine($"Method {pair.Value} not found");
                        continue;
                    }

                    try
                    {
                        Delegate d = Delegate.CreateDelegate(typeof(NetworkManager.RpcReceiveHandler), rpcHandler);
                        if (!NetworkManager.__rpc_func_table.ContainsKey(pair.Key))
                        {
                            NetworkManager.__rpc_func_table.Add(pair.Key, (NetworkManager.RpcReceiveHandler)d);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error creating delegate for method {pair.Value}: {e}");
                    }
                }

            }
        }
    }
}
