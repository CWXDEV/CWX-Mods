using BepInEx;
using BepInEx.Configuration;

namespace DeSharpener
{
    [BepInPlugin("com.CWX.DeSharpener", "CWX-DeSharpener", "1.4.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new SharpenPatch().Enable();
        }
    }
}
