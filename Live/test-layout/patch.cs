using System;
using Aki.Reflection.Patching;
using EFT.InventoryLogic;
using EFT.UI.DragAndDrop;
using System.Reflection;
using EFT.UI;
using UnityEngine;
using Newtonsoft.Json;

namespace Test_layout
{
    public class Test_layoutPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(ContainedGridsView).GetMethod("CreateGrids", BindingFlags.Public | BindingFlags.Static);
        }

        [PatchPostfix]
        private static bool PatchPostfix(ref ContainedGridsView __result, Item item, ContainedGridsView containedGridsTemplate)
        {
            Debug.LogError(item.TemplateId);

            if (item.TemplateId != "5648a69d4bdc2ded0b8b457b") return true;
            
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
            
            return true;
        }
    }
}