using CWX_NewAiType.utils;
using EFT;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aki.Reflection.Patching;
using CWX_NewAiType.Constants;

namespace CWX_NewAiType.Patches
{
    internal class PlayerStatTrackerOnKillPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            var method = ClassResolver.StatisticsServerClass
                .GetMethod("OnEnemyKill", BindingFlags.Public | BindingFlags.Instance);

            if (method == null)
            {
                throw new NullReferenceException();
            }

            return method;
        }

        private static IEnumerable<CodeInstruction> PatchTranspile(ILGenerator generator, IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            var newInstructionInsertIndex = -1;

            // Generate various labels we'll be using later
            var SptInstructionStartLabel = generator.DefineLabel();
            var killedSptUsecLabel = generator.DefineLabel();
            var SptAddLongJumpLabel = generator.DefineLabel();
            var SptSkipFlagLabel = generator.DefineLabel();

            // This label we'll need to locate when scanning through instructions
            var switchEndLabel = default(Label);
            var killXpClassGetter = default(MethodInfo);

            for (var i = 0; i < codes.Count; i++)
            {
                // We locate the label that points to the end of the switch statement
                if (codes[i].opcode == OpCodes.Ldfld &&
                    (FieldInfo)codes[i].operand == AccessTools.Field(ClassResolver.KillExperienceConfigClass, "VictimLevelExp"))
                {
                    switchEndLabel = (Label)codes[i + 2].operand;
                    continue;
                }

                // We locate the index for where our instructions will be inserted
                if (codes[i].opcode == OpCodes.Ldfld &&
                    (FieldInfo)codes[i].operand == AccessTools.Field(ClassResolver.KillExperienceConfigClass, "VictimBotLevelExp"))
                {
                    newInstructionInsertIndex = i + 2;
                    killXpClassGetter = (MethodInfo)codes[i - 2].operand;
                    continue;
                }

                // We locate the instruction from which we can locate the relevant switch statement
                if (codes[i].opcode == OpCodes.Ldsfld &&
                    (FieldInfo)codes[i].operand == AccessTools.Field(ClassResolver.StatisticsCounterClass, "KilledUsec"))
                {
                    // From here, 4 codes back is where the switch should start
                    if (codes[i - 4].opcode != OpCodes.Switch)
                    {
                        throw new NullReferenceException();
                    }

                    // We grab the switch operand, which is just an array of labels
                    var switchOperand = (Label[])codes[i - 4].operand;
                    // The third label in the switch operand points to the ArgumentOutOfRangeException
                    var outOfRangeLabel = switchOperand[2];

                    // We create a new list of labels, and add all the old values to it
                    var newSwitchOperand = new List<Label>();
                    newSwitchOperand.AddRange(switchOperand);

                    // Since EPlayerSide.Terragroup is 8, we need values 5, 6 and 7 to also exit the switch statement
                    // We locate the index where our ArgumentOutOfRange exception throwing instructions are
                    var targetCodeIndex = codes.FindIndex(c => c.labels != null && c.labels.Contains(outOfRangeLabel));
                    if (targetCodeIndex == -1)
                    {
                        throw new NullReferenceException();
                    }

                    // Generate new labels to fill in the unused switch cases
                    var newOutOfRangeLabels = new List<Label>
                    {
                        generator.DefineLabel(),
                        generator.DefineLabel(),
                        generator.DefineLabel(),
                    };

                    // And add them to the ArgumentOutOfRange exception instruiction label list
                    codes[targetCodeIndex].labels.AddRange(newOutOfRangeLabels);

                    // Finally, we add the new labels to the switch statement
                    newSwitchOperand.AddRange(newOutOfRangeLabels.ToArray());

                    // Now, we can add the 8th label that will point to our new instructions
                    newSwitchOperand.Add(SptInstructionStartLabel);

                    // Same deal, except now we need to pad values 9-15 & add the 16th label for the UN side (int value 16)
                    newOutOfRangeLabels = new List<Label>
                    {
                        generator.DefineLabel(),
                        generator.DefineLabel(),
                        generator.DefineLabel(),
                        generator.DefineLabel(),
                        generator.DefineLabel(),
                        generator.DefineLabel(),
                        generator.DefineLabel()
                    };
                    codes[targetCodeIndex].labels.AddRange(newOutOfRangeLabels);
                    newSwitchOperand.AddRange(newOutOfRangeLabels.ToArray());
                    newSwitchOperand.Add(UN_instructionStartLabel);

                    // Reassign the switch operand with our new label list
                    codes[i - 4].operand = newSwitchOperand.ToArray();
                    continue;
                }
            }

            if (switchEndLabel == default)
            {
                throw new NullReferenceException();
            }

            if (newInstructionInsertIndex == -1)
            {
                throw new NullReferenceException();
            }

            if (killXpClassGetter == null)
            {
                throw new NullReferenceException();
            }

            var newInstructions = new List<CodeInstruction>
            {
                // Since the code is being added at the end of one of the switch cases, we need the previous case to exit the switch
                new CodeInstruction(OpCodes.Br_S, switchEndLabel),
                // The following instructions generate this line:
                // sessionCounters.AddLong(1L, (!role.IsBoss()) ? GClass1443.KilledTerragroup : GClass1443.KilledBoss)
                new CodeInstruction(OpCodes.Ldloc_1) { labels = new List<Label>{ SptInstructionStartLabel } },
                new CodeInstruction(OpCodes.Ldc_I8, (long)1),
                new CodeInstruction(OpCodes.Ldarg_S, 5),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(ClassResolver.BotBossInfoClass, "IsBoss", new [] { typeof(WildSpawnType) })),
                new CodeInstruction(OpCodes.Brfalse_S, killedSptUsecLabel),
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(ClassResolver.StatisticsCounterClass, "KilledBoss")),
                new CodeInstruction(OpCodes.Br_S, SptAddLongJumpLabel),
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(ClassResolver.StatisticsCounterClass, Constants.Constants.CounterTag.KilledSptUsec)) { labels = new List<Label> { killedSptUsecLabel }},
                new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(ClassResolver.StatisticValueParentClass, "AddLong", new []{ typeof(long), ClassResolver.StatisticValueClass })) { labels = new List<Label> { SptAddLongJumpLabel } },
                // The following instructions generate this line:
                // if (flag) { list.Add("Terragroup"); }
                new CodeInstruction(OpCodes.Ldloc_S, 5),
                new CodeInstruction(OpCodes.Brfalse_S, SptSkipFlagLabel),
                new CodeInstruction(OpCodes.Ldloc_S, 7),
                new CodeInstruction(OpCodes.Ldstr, EPlayerSide.Usec),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<string>), "Add", new []{ typeof(string) })),
                // The following instructions generate this line:
                // num2 = killExp
                new CodeInstruction(OpCodes.Ldarg_S, 11) { labels = new List<Label>{ SptSkipFlagLabel } },
                new CodeInstruction(OpCodes.Stloc_S, 4),
                // The following instructions generate this line:
                // if (num2 < 0)
                new CodeInstruction(OpCodes.Ldloc_S, 4),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Bge_S, switchEndLabel),
                // The following instructions generate this line:
                // num2 = base.GClass827_0.Kill.VictimBotLevelExp
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, killXpClassGetter),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(ClassResolver.ExperienceConfigClass, "Kill")),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(ClassResolver.KillExperienceConfigClass, "VictimBotLevelExp")),
                new CodeInstruction(OpCodes.Stloc_S, 4),
            };

            codes.InsertRange(newInstructionInsertIndex, newInstructions);

            return codes;
        }
    }
}
