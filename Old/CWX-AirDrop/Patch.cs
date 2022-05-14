using Aki.Reflection.Patching;
using EFT;
using System.Reflection;
using System;

namespace AirDrop
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
            int chance = AirDrop.RandomChanceGen(1, 99);
            if (chance <= AirDrop.dropChance)
            {
                AirDrop.InitObjects();
            }
        }
    }
}
