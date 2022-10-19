using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;

namespace CWX_PrePatcher
{
    public static class PrePatcher
    {
        public static IEnumerable<string> TargetDLLs { get; } = new[] { "Assembly-CSharp.dll" };

        public static void Patch(ref AssemblyDefinition assembly)
        {
            var module = assembly.Modules.FirstOrDefault(x => x.Name == "Assembly-CSharp.dll");

            var types = module.Types;

            var wildSpawnTypes = types.FirstOrDefault(x => x.Name == "WildSpawnType");

            var wilds = wildSpawnTypes.Fields;

            var botDef = new FieldDefinition("sptUsec",
                FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.HasDefault,
                wilds[1].DeclaringType);

            botDef.Constant = 3; // this needs to be set

            wilds.Add(botDef);
        }
    }
}
