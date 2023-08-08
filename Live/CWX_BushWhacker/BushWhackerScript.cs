using EFT.Interactive;
using System.Linq;
using UnityEngine;

namespace CWX_BushWhacker
{
    public class BushWhackerScript : MonoBehaviour
    {
        public BushWhackerScript() 
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
