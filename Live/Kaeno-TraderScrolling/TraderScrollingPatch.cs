using System.Reflection;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using EFT.UI;
using UnityEngine;

namespace TraderScrolling
{
    public class TraderScrollingPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(TraderScreensGroup).GetMethod("method_4", PatchConstants.PrivateFlags);
        }

        [PatchPostfix]
        public static void PatchPostFix()
        {
            var gameObject = GameObject.Find("Menu UI");
            var check = gameObject.GetComponentInChildren<TraderScrollingScript>();
            
            if (check != null)
            {
                return;
            }

            gameObject.AddComponent<TraderScrollingScript>();
        }
    }
}
