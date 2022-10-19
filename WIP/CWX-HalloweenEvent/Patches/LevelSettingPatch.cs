using Aki.Reflection.Patching;
using System.Reflection;

namespace CWX_HalloweenEvent.Patches
{
    public class LevelSettingsPatch : ModulePatch
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
