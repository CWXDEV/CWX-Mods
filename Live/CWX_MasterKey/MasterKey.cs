using BepInEx;
using EFT.Interactive;
using System.Linq;
using UnityEngine;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using Aki.Common.Http;
using Aki.Common.Utils;

namespace CWX_MasterKey
{
    public static class MasterKey
    {
        static object lockObject = new object();

        // Black, Blue, Green, Red, Yellow, Violet
        static string[] keys = new string[] { "5c1d0f4986f7744bb01837fa", "5c1d0c5f86f7744bb2683cf0", "5c1d0dc586f7744baf2e7b79", "5c1d0efb86f7744baf2e7b7b", "5c1d0d6d86f7744bb2683e1f", "5c1e495a86f7743109743dfb" };

        public static void Start()
        {
            ConfigClass config = new ConfigClass();
            bool lockWasTaken = false;

            string keyToUse = "5c1d0d6d86f7744bb2683e1f";

            try
            {
                Monitor.Enter(lockObject, ref lockWasTaken);

                config = GetConfig();
            }
            catch (WebException)
            {
                Debug.LogError("[CWX_Masterkey] Issue happened whilst getting config from server");
            }
            finally
            {
                if (lockWasTaken)
                {
                    Monitor.Exit(lockObject);
                }
            }

            if (keys.Any(x => x == config.keyId))
            {
                keyToUse = config.keyId;
            }

            List<Door> allDoors = GameObject.FindObjectsOfType<Door>().ToList(); // mechanical doors
            List<KeycardDoor> allKeyCardDoors = GameObject.FindObjectsOfType<KeycardDoor>().ToList(); // keycard doors
            List<LootableContainer> allKeyContainers = GameObject.FindObjectsOfType<LootableContainer>().ToList(); // locked loot containers
            List<Trunk> allTrunks = GameObject.FindObjectsOfType<Trunk>().ToList(); // locked car trunks

            foreach (Door door in allDoors)  // mechanical doors
            {
                if (!door.KeyId.IsNullOrWhiteSpace() || !door.KeyId.IsNullOrEmpty())
                {
                    door.KeyId = keyToUse;
                }
            }
            
            foreach (KeycardDoor door in allKeyCardDoors) // keycard doors
            {
                if (!door.KeyId.IsNullOrWhiteSpace() || !door.KeyId.IsNullOrEmpty())
                {
                    door.KeyId = keyToUse;
                }
            }
            
            foreach (LootableContainer door in allKeyContainers) // locked loot containers
            {
                if (!door.KeyId.IsNullOrWhiteSpace() || !door.KeyId.IsNullOrEmpty())
                {
                    door.KeyId = keyToUse;
                }
            }
            
            foreach (Trunk door in allTrunks) // locked car trunks
            {
                if (!door.KeyId.IsNullOrWhiteSpace() || !door.KeyId.IsNullOrEmpty())
                {
                    door.KeyId = keyToUse;
                }
            }
        }

        public static ConfigClass GetConfig()
        {
            var json = RequestHandler.GetJson($"/cwx/masterkey");
            var jsonClass = Json.Deserialize<ConfigClass>(json);

            return jsonClass;
        }
    }
}