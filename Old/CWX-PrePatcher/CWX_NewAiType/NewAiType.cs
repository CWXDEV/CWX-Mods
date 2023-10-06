using BepInEx;
using CWX_NewAiType.Patches;

namespace CWX_NewAiType
{
    [BepInPlugin("com.CWX.NewAiType", "CWX-NewAiType", "1.0.0")]
    public class NewAiType : BaseUnityPlugin
    {
        public NewAiType()
        {
            StaticPatches.PatchBotGlobalSettings();
            StaticPatches.InitKillCounters();
        }
    }
}