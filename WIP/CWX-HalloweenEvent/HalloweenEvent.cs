using BepInEx;
using CWX_HalloweenEvent.Patches;
using EFT.Weather;
using System;

namespace CWX_HalloweenEvent
{
    [BepInPlugin("com.cwx.HalloweenEvent", "cwx-HalloweenEvent", "1.0.0")]
    public class HalloweenEvent : BaseUnityPlugin
    {
        public void Start()
        {
            new LevelSettingsPatch().Enable();
            new RaidStartPatch().Enable();
        }

        /*
         *
         *  1. Orange Moon and Evening time.
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
    }
}
