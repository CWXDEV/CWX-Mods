using Aki.Reflection.Patching;
using EFT;
using System.Reflection;
using CWX_HalloweenEvent.Changes;

namespace CWX_HalloweenEvent.Patches
{
    public class RaidStartPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        public static void PatchPostFix()
        {
            EventWeather eventWeather = new EventWeather();
            eventWeather.Enable();
        }
    }
}
