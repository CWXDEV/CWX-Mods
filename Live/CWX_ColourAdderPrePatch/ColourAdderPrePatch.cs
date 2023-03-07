using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JsonType;
using Mono.Cecil;

namespace CWX_ColourAdder
{
    public static class ColourAdderPrePatch
    {
        public static IEnumerable<string> TargetDLLs { get; } = new[] { "Assembly-CSharp.dll" };
        private static long CWXpink = 0x00000022;
        private static long CWXpurple = 0x00000023;
        
        public static void Patch(ref AssemblyDefinition assembly)
        {
            var colourEnum = assembly.MainModule.GetType("JsonType.TaxonomyColor");

            var cwxPink = new FieldDefinition("CWXpink",
                    FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.HasDefault,
                    colourEnum)
                { Constant = CWXpink };

            var cwxPurple = new FieldDefinition("CWXpurple",
                    FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.HasDefault,
                    colourEnum)
                { Constant = CWXpurple };

            colourEnum.Fields.Add(cwxPink);
            colourEnum.Fields.Add(cwxPurple);
        }
    }
}

