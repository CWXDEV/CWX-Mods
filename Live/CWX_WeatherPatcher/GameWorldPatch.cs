using System.Reflection;
using Aki.Reflection.Patching;
using EFT;
using UnityEngine;

namespace CWX_WeatherPatcher
{
    public class GameWorldPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostFix()
        {
            Debug.LogError("logging for gameworld patch");
            WeatherPatcher.Fix();
        }
    }
}