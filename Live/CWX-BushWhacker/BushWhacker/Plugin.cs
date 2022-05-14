using BepInEx;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using System.Linq;
using UnityEngine;

namespace BushWhacker
{
    [BepInPlugin("com.cwx.bushwhacker", "cwx-bushwhacker", "1.0.0")]
    public class BushWhacker : BaseUnityPlugin
    {
        public void Start()
        {
            new Patch().Enable();
        }

        public static void DisableBushes()
        {
            var gameWorld = Singleton<GameWorld>.Instance;
            var bushes = GameObject.FindObjectsOfType<ObstacleCollider>().ToList();
            foreach (var bushesItem in bushes)
            {
                if (bushesItem.transform.parent.gameObject.name.Contains("filbert") || bushesItem.transform.parent.gameObject.name.Contains("fibert"))
                {
                    Object.DestroyImmediate(bushesItem);
                }
            }
        }
    }
}
