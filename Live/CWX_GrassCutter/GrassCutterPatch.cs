using Aki.Reflection.Patching;
using EFT;
using System.Reflection;

namespace CWX_GrassCutter
{
    public class GrassCutterPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostFix()
        {
            GrassCutterScript grassCutter = new GrassCutterScript();

            grassCutter.Start();
        }
    }
}
