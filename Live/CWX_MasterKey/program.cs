using BepInEx;

namespace CWX_MasterKey
{
    [BepInPlugin("com.CWX.MasterKey", "CWX-MasterKey", "1.3.5")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new MasterKeyPatch().Enable();
        }
    }
}
