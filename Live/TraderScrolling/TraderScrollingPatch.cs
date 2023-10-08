namespace TraderScrolling
{
    public class TraderScrollingPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(TraderScreensGroup).GetMethod("method_4", PatchConstants.PrivateFlags);
        }

        [PatchPostfix]
        public static void PatchPostFix()
        {
            var go = GameObject.Find("Menu UI");
            var check = go.GetComponentInChildren<TraderScrollingScript>();
            if (check != null)
            {
                return;
            }

            go.AddComponent<TraderScrollingScript>();
        }
    }
}
