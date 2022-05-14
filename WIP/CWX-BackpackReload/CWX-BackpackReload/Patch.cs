using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using EFT.InventoryLogic;
using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CWX_BackpackReload
{
    public class Patch : ModulePatch
    {
        private static Type _targetType;

        public Patch()
        {
            _targetType = PatchConstants.EftTypes.Single(IsTargetType);
        }

        private bool IsTargetType(Type type)
        {
            return type.GetMethod("UpdateTotalWeight") != null;
        }

        protected override MethodBase GetTargetMethod()
        {
            return typeof(InventoryClass).GetMethod("UpdateTotalWeight");
        }

        [PatchPrefix]
        private static void PatchPrefix()
        {
            Debug.Log("HELLO!");

            EquipmentSlot[] value = Traverse.Create(_targetType).Field<EquipmentSlot[]>("FastAccessSlots").Value;
            foreach (var slot in value)
            {
                Debug.Log(slot);
            }

            value.AddItem(EquipmentSlot.Backpack);
        }
    }
}
