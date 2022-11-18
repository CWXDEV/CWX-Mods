using Aki.Reflection.Patching;
using EFT;
using System.Reflection;

namespace CWX_BushWhacker
{
    public class BushWhackerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        public static void PatchPostFix()
        {
            BushWhacker bushWhacker = new BushWhacker();
            bushWhacker.DisableBushes();
        }
    }
}
