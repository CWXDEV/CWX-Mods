using System;
using System.Linq;
using Aki.Reflection.Patching;
using System.Reflection;
using Aki.Reflection.Utils;
using EFT;
using HarmonyLib;

namespace Chocster_mod
{
    public class ChocPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return PatchConstants.EftTypes.First(IsTargetType).GetMethod("OnModChanged", PatchConstants.PublicFlags);
        }

        private bool IsTargetType(Type type)
        {
            if (type.Name == "Class943")
            {
                return true;
            }
            
            return false;
        }

        [PatchPrefix]
        private static void PatchPrefix(ref object __instance)
        {
            Logger.LogError($"Get weaponPrefab");
            var testTraverse = (Player.FirearmController) Traverse.Create(__instance).Field("firearmController_0").GetValue();
            var testtest = (WeaponPrefab)Traverse.Create(testTraverse).Field("weaponPrefab_0").GetValue();
            Logger.LogError($"Got weaponPrefab");
            Logger.LogError($"Get weaponPrefab methods");
            var methodsOnType = testtest.GetType().GetMethods().ToList();
            Logger.LogError($"Log weaponPrefab methods");
            foreach (var method in methodsOnType)
            {
                Logger.LogError(method.Name);
            }
            Logger.LogError($"End");
            testtest.ResetStatesToDefault();
        }
    }
}

