using BepInEx;
using System;

namespace TraderScrolling
{
    [BepInPlugin("com.kaeno.TraderScrolling", "Kaeno-TraderScrolling", "1.0.0")]
    public class TraderScrolling : BaseUnityPlugin
    {
        public void awake()
        {
            new TraderScrollingPatch().Enable();
        }
    }
}
