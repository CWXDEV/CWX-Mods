using BepInEx;

namespace test_layout
{
    [BepInPlugin("com.test_layout", "test_layout", "1.0.0")]
    public class test_layout : BaseUnityPlugin
    {
        private void Awake()
        {
            new test_layoutPatch().Enable();
        }
    }
}
