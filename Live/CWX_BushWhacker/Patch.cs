using Aki.Reflection.Patching;
using EFT;
using System.Reflection;

namespace BushWhacker
{
    public class Patch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        public static void PatchPostFix()
        {
            BushWhacker.DisableBushes();
        }
    }
}
