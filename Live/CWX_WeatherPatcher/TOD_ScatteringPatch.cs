using Aki.Reflection.Patching;
using System.Reflection;

namespace CWX_WeatherPatcher
{
    class TOD_ScatteringPatch : ModulePatch  // MAKES CHANGES TO TOD_SCATTERING FOG
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(TOD_Scattering).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PostFixPatch(ref TOD_Scattering __instance)
        {
            __instance.GlobalDensity = 0f;
            __instance.HeightFalloff = 0f;
        }
    }
}
