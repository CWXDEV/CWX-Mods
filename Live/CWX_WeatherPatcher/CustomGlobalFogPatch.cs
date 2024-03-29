﻿using Aki.Reflection.Patching;
using System.Reflection;

namespace CWX_WeatherPatcher
{
    class CustomGlobalFogPatch : ModulePatch  // MAKES CHANGES TO CUSTOMGLOBALFOG
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(CustomGlobalFog).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PostFixPatch(ref CustomGlobalFog __instance)
        {
            __instance.FuncStart = 0f;
        }
    }
}
