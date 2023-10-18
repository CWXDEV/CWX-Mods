using System;
using System.Linq;
using Aki.Reflection.Patching;
using EFT.InventoryLogic;
using EFT.UI.DragAndDrop;
using System.Reflection;
using Aki.Reflection.Utils;
using EFT.UI;
using UnityEngine;
using Newtonsoft.Json;

namespace Test_layout
{
    public class Test_layoutPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            var typesToCheck = PatchConstants.EftTypes;
            var afterChecks = typesToCheck.First(IsTargetType);
            
            Debug.LogError($"AfterChecks: {afterChecks.Name}");
            
            var methodToCheck = afterChecks.GetMethod("OnModChanged", PatchConstants.PublicFlags);
            
            Debug.LogError($"methodName: {methodToCheck.Name}");
            
            return methodToCheck;
        }

        private bool IsTargetType(Type type)
        {
            Debug.LogError($"type name: {type.Name}");
            var fieldsToCheck = type.GetFields();

            if (fieldsToCheck.Length == 4 && fieldsToCheck.Any(x => x.Name == "item_0") &&
                fieldsToCheck.Any(x => x.Name == "slot_0") && fieldsToCheck.Any(x => x.Name == "callback_0"))
            {
                Debug.LogError("type found mother fucker");
                return true;
            }

            return false;
        }

        [PatchPostfix]
        private static void PatchPostfix(ref ContainedGridsView __result, Item item, ContainedGridsView containedGridsTemplate)
        {
            Debug.LogError(item.TemplateId);

            if (item.TemplateId != "5648a69d4bdc2ded0b8b457b") return;
            
            Debug.LogError("Test!");

            foreach (var gridView in __result.GridViews)
            {
                Debug.LogError(JsonConvert.SerializeObject(gridView));
            }

            var test = new GridView();
            test.enabled = true;
            test.Grid = new GClass2317("1", 1, 2, false, false, Array.Empty<ItemFilter>(),
                new LootItemClass("test", new GClass2348()));
            test.IsMagnified = false;
            test.name = "GridView 1";
            test.tag = "Untagged";

            __result.GridViews = new[] { test };
        }
    }
}