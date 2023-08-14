using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using EFT.UI;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

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
            Debug.LogError("test 1");
            var traderCards = GameObject.Find("TraderCards");

            Debug.LogError(traderCards);
            var traderCardsRect = traderCards.RectTransform();
            Debug.LogError(traderCardsRect);

            traderCardsRect.anchorMax = new Vector2(1f, 1f);
            traderCardsRect.anchorMin = new Vector2(0.385f, 1f);

            var menuUI = GameObject.Find("Menu UI");
            Debug.LogError(menuUI);

            var list = menuUI.GetComponentsInChildren<RectTransform>(true).ToList();
            Debug.LogError(list.Count);

            var container = list.FirstOrDefault(x => x.name == "Container");
            Debug.LogError(container);

            var containerRect = container.RectTransform();
            Debug.LogError(containerRect);

            containerRect.anchorMin = new Vector2(1f, 1f);
            containerRect.anchorMax = new Vector2(0.01f, 0f);

            var scrollrect = traderCards.AddComponent<ScrollRect>();
            Debug.LogError(scrollrect);

            scrollrect.content = traderCardsRect;
            scrollrect.vertical = false;
            scrollrect.movementType = ScrollRect.MovementType.Elastic;
            scrollrect.viewport = containerRect;
        }
    }
}
