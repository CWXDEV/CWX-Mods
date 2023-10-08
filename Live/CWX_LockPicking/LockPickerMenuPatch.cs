using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using EFT;
using UnityEngine;
using System.Threading.Tasks;
using EFT.Interactive;

namespace CWX_LockPicking
{
    public class LockPickerMenuPatch : ModulePatch
    {
        // TODO: Make Version Agnostic
        private static Type playerActionClass; // GClass1726 - smethod_9 on this class
        private static Type menuClass; // GClass2804
        private static Type menuItemClass; // GClass2803
        
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GClass1726).GetMethod("smethod_9", BindingFlags.Static | BindingFlags.NonPublic);
        }

        [PatchPostfix]
        public static void PatchPostFix(ref GClass2804 __result, GamePlayerOwner owner, Door door)
        {
            // always do this to get rid of the useless interactions
            __result.Actions.RemoveAll(
                x => x.Name == "Bang & clear" || x.Name == "Flash & clear" || x.Name == "Move in");
            
            // make sure its a locked and a door that can be unlocked
            if (door.DoorState != EDoorState.Locked || door.KeyId == null || door.KeyId == "") return;
            
            // add action to unlock door - currently no animation
            __result.Actions.Add(new GClass2803
            {
                Name = "LockPicking",
                Action = () => { door.DoorState = EDoorState.Shut; },
                Disabled = false
            });
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    public class TestLock : MonoBehaviour
    {
        private string _BundlePath = "BepInEx/plugins/CWX/Test/lockpick.bundle";
        public AssetBundle assetBundle;
        public async Task<GameObject> LoadLock()
        {
            var bundleLoadRequest = AssetBundle.LoadFromFileAsync(_BundlePath);

            while (!bundleLoadRequest.isDone)
                await Task.Yield();

            assetBundle = bundleLoadRequest.assetBundle;

            var assetLoadRequest = assetBundle.LoadAllAssetsAsync<GameObject>();

            while (!assetLoadRequest.isDone)
                await Task.Yield();

            var requestedGo = assetLoadRequest.allAssets[0] as GameObject;

            return requestedGo;
        }

        public async void Init()
        {
            Instantiate(await LoadLock(), new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }
}