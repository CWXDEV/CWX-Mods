using BepInEx;

namespace VoiceAdd
{
    [BepInPlugin("com.CWX.VoiceAdder", "CWX-VoiceAdder", "2.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
            Logger.LogInfo("Loading: CWX-VoiceAdder - V2.0.0");
            new VoicePatch().Enable();
        }
    }
}
