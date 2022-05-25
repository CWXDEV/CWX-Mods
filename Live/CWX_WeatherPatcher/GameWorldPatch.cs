using System.Reflection;
using Aki.Reflection.Patching;
using CWX_WeatherPatcher;
using EFT;

namespace CWX_WeatherPatch
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
            Plugin.Fix();
        }
    }
}