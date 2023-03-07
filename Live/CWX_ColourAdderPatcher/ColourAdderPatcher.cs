using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using BepInEx;
using EFT;
using Comfort.Common;
using JsonType;
using UnityEngine;

namespace CWX_ColourAdderPatcher
{
    [BepInPlugin("com.cwx.ColourAdder", "cwx-ColourAdder", "1.0.0")]
    public class ColourAdderPatcher : BaseUnityPlugin
    {
        public void Start()
        {
            var type = PatchConstants.EftTypes.Single(x => x.Name == "GClass1179")
                .GetField("dictionary_0");
            // get field with name of dictionary_0 add custom
            // dictionary.Add(TaxonomyColor.yellow, new Color32(104, 102, 40, byte.MaxValue));
        }
    }
}

