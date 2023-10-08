using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using EFT.UI;
using JetBrains.Annotations;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace TraderScrolling
{
    public class PlayerCardPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            Logger.LogError("Patching Show");
            return typeof(DisplayMoneyPanelTMPText).GetMethod("Show", PatchConstants.PublicFlags);
        }

        [PatchPostfix]
        public static void PatchPostFix()
        {
            var go = GameObject.Find("Menu UI");
            var check = go.GetComponentInChildren<PlayerCardScript>();


            if (check != null)
            {
                return;
            }

            go.AddComponent<PlayerCardScript>();
        }
    }
}
