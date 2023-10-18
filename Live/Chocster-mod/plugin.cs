using BepInEx;
using BepInEx.Logging;

namespace Chocster_mod
{
    [BepInPlugin("com.ChocMod", "ChocMod", "1.0.0")]
    public class ChocPlugin : BaseUnityPlugin
    {
        internal new static ManualLogSource Logger { get; private set; }
        private void Awake()
        {
            Logger = base.Logger;
            
            new ChocPatch().Enable();
        }
    }
}

