using BepInEx;

namespace CWX_DeSharpener
{
    [BepInPlugin("com.CWX.DeSharpener", "CWX-DeSharpener", "1.6.0")]
    public class DeSharpener : BaseUnityPlugin
    {
        private void Awake()
        {
            new DeSharpenerPatch().Enable();
        }
    }
}
