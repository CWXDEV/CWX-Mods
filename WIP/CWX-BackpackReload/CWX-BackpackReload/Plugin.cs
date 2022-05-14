using System;
using BepInEx;

namespace CWX_BackpackReload
{
    [BepInPlugin("com.CWX.BackpackReload", "CWX-BackpackReload", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
            Logger.LogInfo("Loading: CWX-BackpackReload - V1.0.0");
            new Patch().Enable();
        }
    }
}
