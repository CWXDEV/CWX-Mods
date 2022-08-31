using BepInEx;

namespace VoiceAdd
{
    [BepInPlugin("com.CWX.VoiceAdder", "CWX-VoiceAdder", "2.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
            new VoicePatch().Enable();
        }
    }
}
