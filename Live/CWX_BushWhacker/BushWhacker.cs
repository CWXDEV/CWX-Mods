using BepInEx;
using EFT.Interactive;
using System.Linq;
using UnityEngine;

namespace CWX_BushWhacker
{
    [BepInPlugin("com.cwx.bushwhacker", "cwx-bushwhacker", "1.3.2")]
    public class BushWhacker : BaseUnityPlugin
    {
        public void Start()
        {
            new BushWhackerPatch().Enable();
        }

        public void DisableBushes()
        {
            var bushes = FindObjectsOfType<ObstacleCollider>().ToList();

            var swamps = FindObjectsOfType<BoxCollider>().ToList();

            foreach (var swamp in swamps)
            {
                if (swamp.name == "Swamp_collider")
                {
                    DestroyImmediate(swamp);
                }
            }

            foreach (var bushesItem in bushes)
            {
                var filbert = bushesItem?.transform?.parent?.gameObject?.name?.Contains("filbert");
                var fibert = bushesItem?.transform?.parent?.gameObject?.name?.Contains("fibert");

                if (filbert == true || fibert == true)
                {
                    DestroyImmediate(bushesItem);
                }
            }
        }
    }
}
