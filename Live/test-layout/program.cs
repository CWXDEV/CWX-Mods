using BepInEx;

namespace Test_layout
{
    [BepInPlugin("com.test_layout", "test_layout", "1.0.0")]
    public class Test_layout : BaseUnityPlugin
    {
        private void Awake()
        {
            new Test_layoutPatch().Enable();
        }
    }
}
