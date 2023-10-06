using System.Reflection;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using EFT.UI;

namespace CWX_LockPicking
{
    public class LockPickerUIPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(ItemUiContext).GetMethod("GetItemContextInteractions", PatchConstants.PublicFlags);
        }

        [PatchPostfix]
        private static void PostFixPatch(ref ItemInfoInteractions __result, ItemUiContext __instance,
            ItemContext itemContext)
        {
            
        }
    }
}