﻿using BepInEx;
using UnityEngine;


namespace CWX_AlarmChanger
{
    [BepInPlugin("com.cwx.alarmchanger", "cwx-alarmchanger", "1.0.0")]
    public class AlarmChanger : BaseUnityPlugin
    {
        public static GameObject CWX_GameObject;
        public static AlarmChangerScript CWX_Component;

        public void Start()
        {
            CWX_GameObject = new GameObject("CWX_GameObject");
            CWX_Component = CWX_GameObject.AddComponent<AlarmChangerScript>();
            DontDestroyOnLoad(CWX_GameObject);

            new AlarmChangerPatch().Enable();
        }
    }
}
