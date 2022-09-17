using Aki.Reflection.Patching;
using System.Reflection;
using EFT;

namespace CWX_WeatherPatcher
{
    class GClassPatch : ModulePatch  // MAKES CHANGES TO WeatherClass SMETHOD_0
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(WeatherClass).GetMethod("smethod_0", BindingFlags.NonPublic | BindingFlags.Static);
        }

        [PatchPostfix]
        private static void PatchPostfix(ref WeatherClass __result)
        {
            __result.Cloudness = -1;
            __result.WindDirection = 8;
            __result.Wind = 0;
            __result.Rain = 0;
            __result.RainRandomness = 0;
            __result.ScaterringFogDensity = 0;
            __result.GlobalFogDensity = 0;
            __result.GlobalFogHeight = 0;
        }
    }
}
