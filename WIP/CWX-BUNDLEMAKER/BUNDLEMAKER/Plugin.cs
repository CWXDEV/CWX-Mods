using BepInEx;
using Aki.Reflection.Patching;
using System.Reflection;
using EFT;
using UnityEngine;

namespace BUNDLEMAKER
{
    [BepInPlugin("com.cwx.bundlemaker", "CWX-BUNDLEMAKER", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
            new Patch().Enable();
            Logger.LogInfo("Plugin CWX-BUNDLEMAKER is loaded!");
        }

        private void Update()
        {
        }

        public static void LoadAssets()
        {
            AssetBundle target = AssetBundle.LoadFromFile("C:/AKI PROJECT/AKI 17686/EscapeFromTarkov_Data/StreamingAssets/Windows/assets/custom/helicrash");
            GameObject targetGameObject = (GameObject)target.LoadAsset("Hind");
            Instantiate(targetGameObject);
            targetGameObject.transform.position = new Vector3(239.8987f, -2.06f, 89.7596f);
            targetGameObject.transform.rotation = Quaternion.Euler(286.1528f, 44.3206f, 8.1589f);
        }
    }

    public class Patch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("OnGameStarted", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostFix()
        {
            Plugin.LoadAssets();
        }
    }
}
