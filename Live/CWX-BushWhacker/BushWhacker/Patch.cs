using Aki.Reflection.Patching;
using EFT;
using System.Reflection;
using UnityEngine;

namespace BushWhacker
{
    public class Patch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            var result = typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);

            return result;
        }
        [PatchPostfix]
        public static void PatchPostFix()
        {
            Debug.LogError("test patch");
            BushWhacker.DisableBushes();
        }
    }
}
