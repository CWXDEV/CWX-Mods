using CWX_NewAiType.utils;
using CWX_NewAiType.Utils;
using EFT;
using EFT.Counters;
using HarmonyLib;
using System;
using System.Collections.Generic;

namespace CWX_NewAiType.Patches
{
    public static class StaticPatches
    {
        public static void PatchBotGlobalSettings()
        {
            Traverse.Create(ClassResolver.AISettingsLoaderClass)
                .Field<Dictionary<WildSpawnType, List<BotDifficulty>>>("ExcludedDifficulties")
                .Value
                .Add(EnumResolver.sptUsec, new List<BotDifficulty>
                {
                    BotDifficulty.easy,
                    BotDifficulty.normal,
                    BotDifficulty.hard,
                    BotDifficulty.impossible
                });
        }

        public static void InitKillCounters()
        {
            var statisticValueConstructor = ClassResolver.StatisticValueClass.GetConstructor(new[] { typeof(CounterValueType), typeof(IEnumerable<string>) });
            if (statisticValueConstructor == null)
            {
                throw new ArgumentNullException();
            }

            Traverse
                .Create(ClassResolver.StatisticsCounterClass)
                .Field("KilledSptUsec")
                .SetValue(statisticValueConstructor.Invoke(new object[] { CounterValueType.Long, new[] { "KilledSptUsec" } }));

            var sessionCountersField = Traverse.Create(ClassResolver.StatisticsCounterClass).Field("SessionToOverallCounters").GetValue();

            Traverse.Create(sessionCountersField)
                .Method("Add", new[] { ClassResolver.StatisticValueClass })
                .GetValue(Traverse.Create(ClassResolver.StatisticsCounterClass).Field("KilledSptUsec").GetValue());
        }
    }
}
