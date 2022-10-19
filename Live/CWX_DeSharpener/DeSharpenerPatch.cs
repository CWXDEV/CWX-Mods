using System;
using System.Reflection;
using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using System.Linq;

namespace CWX_DeSharpener
{
	public class DeSharpenerPatch : ModulePatch
	{
		private static Type _targetType;

		public DeSharpenerPatch()
        {
			_targetType = PatchConstants.EftTypes.Single(IsTargetType);
        }

		private bool IsTargetType(Type type)
        {
			return type.GetMethod("UpdateAmount") != null && type.GetMethod("ChangeDefaultSharpenValue") != null;
        }

		protected override MethodBase GetTargetMethod()
        {
			return _targetType.GetMethod("UpdateAmount");
        }

		[PatchPrefix]
		private static bool PatchPrefix()
        {
			return false;
        }
    }
}