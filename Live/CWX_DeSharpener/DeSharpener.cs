﻿using BepInEx;

namespace CWX_DeSharpener
{
    [BepInPlugin("com.CWX.DeSharpener", "CWX-DeSharpener", "1.4.7")]
    public class DeSharpener : BaseUnityPlugin
    {
        private void Awake()
        {
            new DeSharpenerPatch().Enable();
        }
    }
}