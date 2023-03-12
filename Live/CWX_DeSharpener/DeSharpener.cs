using BepInEx;

namespace CWX_DeSharpener
{
    [BepInPlugin("com.CWX.DeSharpener", "CWX-DeSharpener", "1.5.1")]
    public class DeSharpener : BaseUnityPlugin
    {
        private void Awake()
        {
            new DeSharpenerPatch().Enable();
        }
    }
}
