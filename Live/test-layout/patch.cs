using Aki.Reflection.Patching;
using EFT.InventoryLogic;
using EFT.UI.DragAndDrop;
using System.Reflection;
using UnityEngine;

namespace test_layout
{
    public class test_layoutPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            Debug.LogError("fucker");
            var methods = getMethods();

            foreach (var method in methods)
            {
                Debug.LogError(method.Name);
            }

            return typeof(ContainedGridsView).GetMethod("CreateGrids", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public MethodInfo[] getMethods()
        {
            return typeof(ContainedGridsView).GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
        }

        [PatchPrefix]
        private static bool PatchPrefix(ref ContainedGridsView __result, Item item, ContainedGridsView containedGridsTemplate)
        {
            Debug.LogError(item.TemplateId);

            if (item.TemplateId == "5648a69d4bdc2ded0b8b457b")
            {
                foreach (var grid in containedGridsTemplate.GridViews)
                {
                    Debug.LogError(grid.transform.ToString());
                }

                __result = Object.Instantiate(containedGridsTemplate);
                return false;
            }

            return true;
        }
    }
}