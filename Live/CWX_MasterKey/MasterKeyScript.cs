using Aki.Common.Http;
using Aki.Common.Utils;
using BepInEx;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using UnityEngine;

namespace CWX_MasterKey
{
    public class MasterKeyScript
    {
        private object _lockObject = new object();

        // Black, Blue, Green, Red, Yellow, Violet
        private readonly string[] _keys = { "5c1d0f4986f7744bb01837fa", "5c1d0c5f86f7744bb2683cf0", "5c1d0dc586f7744baf2e7b79", "5c1d0efb86f7744baf2e7b7b", "5c1d0d6d86f7744bb2683e1f", "5c1e495a86f7743109743dfb" };
        private string _keyToUse = "5c1d0d6d86f7744bb2683e1f";

        public void Start()
        {
            MasterKeyConfigClass config = new MasterKeyConfigClass();
            bool lockWasTaken = false;

            try
            {
                Monitor.Enter(_lockObject, ref lockWasTaken);

                config = GetConfig();
            }
            catch (WebException)
            {
                Debug.LogError("[CWX_MasterKey] Issue happened whilst getting config from server");
            }
            finally
            {
                if (lockWasTaken)
                {
                    Monitor.Exit(_lockObject);
                }
            }

            if (_keys.Any(x => x == config.keyId))
            {
                _keyToUse = config.keyId;
            }

            if (!CheckPlayer())
            {
                return;
            }

            List<Door> allDoors = GameObject.FindObjectsOfType<Door>().ToList(); // mechanical doors
            List<KeycardDoor> allKeyCardDoors = GameObject.FindObjectsOfType<KeycardDoor>().ToList(); // keycard doors
            List<LootableContainer> allKeyContainers = GameObject.FindObjectsOfType<LootableContainer>().ToList(); // locked loot containers
            List<Trunk> allTrunks = GameObject.FindObjectsOfType<Trunk>().ToList(); // locked car trunks

            foreach (Door door in allDoors)  // mechanical doors
            {
                if (!door.KeyId.IsNullOrWhiteSpace() || !door.KeyId.IsNullOrEmpty())
                {
                    door.KeyId = _keyToUse;
                }
            }
            
            foreach (KeycardDoor door in allKeyCardDoors) // keycard doors
            {
                if (!door.KeyId.IsNullOrWhiteSpace() || !door.KeyId.IsNullOrEmpty())
                {
                    door.KeyId = _keyToUse;
                }
            }
            
            foreach (LootableContainer door in allKeyContainers) // locked loot containers
            {
                if (!door.KeyId.IsNullOrWhiteSpace() || !door.KeyId.IsNullOrEmpty())
                {
                    door.KeyId = _keyToUse;
                }
            }
            
            foreach (Trunk door in allTrunks) // locked car trunks
            {
                if (!door.KeyId.IsNullOrWhiteSpace() || !door.KeyId.IsNullOrEmpty())
                {
                    door.KeyId = _keyToUse;
                }
            }
        }

        public MasterKeyConfigClass GetConfig()
        {
            var json = RequestHandler.GetJson($"/cwx/masterkey");
            var jsonClass = Json.Deserialize<MasterKeyConfigClass>(json);

            return jsonClass;
        }

        public bool CheckPlayer()
        {
            GameWorld gameWorld = Singleton<GameWorld>.Instance;

            Player player= gameWorld.RegisteredPlayers.Find(p => p.IsYourPlayer);

            var items = player.Profile.Inventory.Equipment.GetAllItems();

            return items.Any(item => item.TemplateId == _keyToUse);
        }
    }
}