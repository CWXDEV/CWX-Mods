using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using System.Reflection;

namespace CWX_AlarmChanger
{
    public class AlarmChangerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        public static void PatchPostFix()
        {
            var gameWorld = Singleton<GameWorld>.Instance;

            if (gameWorld != null && gameWorld.MainPlayer.Location.ToLower() == "rezervbase")
            {
                AlarmChanger.CWX_Component.SetSounds();
            }
        }
    }
}
