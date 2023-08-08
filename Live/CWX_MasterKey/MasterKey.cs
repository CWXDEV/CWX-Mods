using BepInEx;
using BepInEx.Configuration;
using System;

namespace CWX_MasterKey
{
    [BepInPlugin("com.CWX.MasterKey", "CWX-MasterKey", "1.4.3")]
    public class MasterKey : BaseUnityPlugin
    {
        private const string KeyConfigName = "Key Config";
        internal static ConfigEntry<string> Key;
        private static string DefaultKey = "5c1d0d6d86f7744bb2683e1f";

        private void Awake()
        {
            InitConfig();

            new MasterKeyPatch().Enable();
        }

        private void InitConfig()
        {
            Key = Config.Bind(
                KeyConfigName,"Key",
                DefaultKey, "This will be used as the key to open doors");
        }
    }
}
