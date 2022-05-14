using Aki.Common.Utils;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace VoiceAdd
{
    public class VoicePatch : ModulePatch
    {
        private static Type _targetType;
        public static string CurrentDirectory { get; set; }

        public VoicePatch()
        {
            _targetType = PatchConstants.EftTypes.Single(IsTargetType);
        }

        private bool IsTargetType(Type type)
        {
            return type.GetMethod("TakePhrasePath") != null;
        }

        protected override MethodBase GetTargetMethod()
        {
            return _targetType.GetMethod("TakePhrasePath");
        }

        [PatchPrefix]
        private static void PatchPrefix()
        {
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            var modDirectory = currentDirectory + "/user/mods/CWX_VoiceAdder/";
            string json = new StreamReader(modDirectory + "voices.json").ReadToEnd();

            Dictionary<string, string> voices = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            Dictionary<string, string> value = Traverse.Create(_targetType).Field<Dictionary<string, string>>("dictionary_0").Value;

            foreach (var voicename in voices.Keys)
            {
                voices.TryGetValue(voicename, out var voicebundle);
                if (!value.ContainsKey(voicename))
                    value.Add(voicename, voicebundle);
            }
        }
    }
}
