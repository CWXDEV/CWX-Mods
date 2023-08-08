using BepInEx;

namespace CWX_BushWhacker
{
    [BepInPlugin("com.cwx.bushwhacker", "cwx-bushwhacker", "1.4.0")]
    public class BushWhacker : BaseUnityPlugin
    {
        public void Start()
        {
            new BushWhackerPatch().Enable();
        }
    }
}
