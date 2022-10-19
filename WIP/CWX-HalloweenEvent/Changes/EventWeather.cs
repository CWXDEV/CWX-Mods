using Comfort.Common;
using EFT;
using EFT.Weather;
using GPUInstancer;
using System;
using UnityEngine;

namespace CWX_HalloweenEvent.Changes
{
    public class EventWeather
    {
        public WeatherController WController;
        public Transform SkyDomeGameObject;
        public TOD_Sky SkySettings;
        public TOD_Time TimeSettings;
        public GameWorld GWorld;

        // The separation isn't needed, but is easier to read.
        public void Enable()
        {
            // Initialise all Vars.
            InitVars();

            // Change all the vars.
            ChangeVars();
        }

        private void InitVars()
        {
            // Finds the GameWorld Instance
            GWorld = Singleton<GameWorld>.Instance;

            // Finds the WeatherControllers Instance.
            WController = WeatherController.Instance;

            // Finds the SkyDome GameObject.
            SkyDomeGameObject = WController.transform.Find("Sky Dome");

            // Finds the TOD_Time attached to SkyDome.
            TimeSettings = SkyDomeGameObject.GetComponent<TOD_Time>();

            // Finds the TOD_Sky Instance;
            SkySettings = TOD_Sky.Instance;
        }

        private void ChangeVars()
        {
            // Enables WeatherDebug and removes clouds and makes lightning 100% possible
            WController.WeatherDebug.Enabled = true;
            WController.WeatherDebug.CloudDensity = -1f;
            WController.WeatherDebug.LightningThunderProbability = 100f;

            // Disables the higher clouds.
            SkyDomeGameObject.transform.Find("Clouds").gameObject.SetActive(false);

            // Sets the SkyColor to Red
            for (int i = 0; i < SkySettings.Night.SkyColor.colorKeys.Length ; i++)
            {
                SkySettings.Night.SkyColor.colorKeys[i].color.r = 1f;
                SkySettings.Night.SkyColor.colorKeys[i].color.g = 0f;
                SkySettings.Night.SkyColor.colorKeys[i].color.b = 0f;
                SkySettings.Night.SkyColor.colorKeys[i].color.a = 1f;
            }

            // Makes the stars a little brighter
            SkySettings.Stars.Brightness = 10f;

            // Makes the moon 200% bigger
            SkySettings.Moon.MeshSize = 3f;

            // Disables the lower clouds.
            WController.CloudsController.enabled = false;

            WController.RainController.enabled = false;

            // Sets the time to midnight whatever was picked on the raid select screen.
            TimeSettings.GameDateTime.Reset(new DateTime(2022, 10, 14, 3, 0, 0));
        }
    }
}
