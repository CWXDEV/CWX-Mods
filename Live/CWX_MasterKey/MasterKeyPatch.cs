using System.Reflection;
using SPT.Reflection.Patching;
using EFT;

namespace CWX_MasterKey
{
    public class MasterKeyPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostFix()
        {
            MasterKeyScript masterKey = new MasterKeyScript();

            masterKey.Start();
        }
    }
}