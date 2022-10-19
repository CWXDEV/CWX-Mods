using Aki.Reflection.Patching;
using System.Reflection;

namespace CWX_WeatherPatcher
{
    public class LevelSettingsPatch : ModulePatch  // MAKES CHANGES TO LevelSettings
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(LevelSettings).GetMethod("ApplySettings", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PostFixPatch(ref LevelSettings __instance)
        {
            __instance.fog = false;
            __instance.fogEndDistance = 0f;
        }
    }
}
