using BepInEx;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CWX_MasterKey
{
    public class MasterKeyScript
    {
        // Black, Blue, Green, Red, Yellow, Violet
        private readonly string[] _keys = { "5c1d0f4986f7744bb01837fa", "5c1d0c5f86f7744bb2683cf0", "5c1d0dc586f7744baf2e7b79", "5c1d0efb86f7744baf2e7b7b", "5c1d0d6d86f7744bb2683e1f", "5c1e495a86f7743109743dfb" };
        private string _keyToUse = "5c1d0d6d86f7744bb2683e1f";

        public void Start()
        {
            if (_keys.Any(x => x == MasterKey.Key.Value))
            {
                _keyToUse = MasterKey.Key.Value;
            }

            if (!CheckPlayer())
            {
                return;
            }

            List<Door> allDoors = Object.FindObjectsOfType<Door>().ToList(); // mechanical doors
            List<KeycardDoor> allKeyCardDoors = Object.FindObjectsOfType<KeycardDoor>().ToList(); // keycard doors
            List<LootableContainer> allKeyContainers = Object.FindObjectsOfType<LootableContainer>().ToList(); // locked loot containers
            List<Trunk> allTrunks = Object.FindObjectsOfType<Trunk>().ToList(); // locked car trunks

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

        public bool CheckPlayer()
        {
            return Singleton<GameWorld>.Instance.MainPlayer.Profile.Inventory.Equipment.GetAllItems()
                .Any(item => item.TemplateId == _keyToUse);
        }
    }
}