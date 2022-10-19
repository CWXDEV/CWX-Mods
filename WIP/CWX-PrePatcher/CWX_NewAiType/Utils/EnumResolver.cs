using EFT;
using System;

namespace CWX_NewAiType.Utils
{
    public static class EnumResolver
    {
        public static WildSpawnType sptUsec => (WildSpawnType)Enum.Parse(typeof(WildSpawnType), "sptUsec", true);

    }
}