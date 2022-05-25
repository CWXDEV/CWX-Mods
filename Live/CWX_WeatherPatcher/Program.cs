using BepInEx;
using CWX_WeatherPatch;
using EFT.Weather;

namespace CWX_WeatherPatcher
{
    [BepInPlugin("com.CWX.WeatherPatcher", "CWX-WeatherPatcher", "3.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogMessage("Loading: CWX-WeatherPatcher - V3.0.0");

            new GClassPatch().Enable();
            new CustomGlobalFogPatch().Enable();
            new LevelSettingsPatch().Enable();
            new TOD_ScatteringPatch().Enable();
            new GameWorldPatch().Enable();
        }

        public static void Fix()
        {
            var instance = WeatherController.Instance;
            if (instance != null)
            {
                instance.RainController.enabled = false;
                instance.LightningController.enabled = false;

                var debug = WeatherController.Instance.WeatherDebug;
                debug.Enabled = true;
                debug.CloudDensity = -1f;
                debug.Fog = 0f;
                debug.Rain = 0f;
                debug.LightningThunderProbability = 0f;
                debug.WindMagnitude = 0f;
            }
        }
    }
}
