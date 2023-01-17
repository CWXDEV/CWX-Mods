using System;
using BepInEx;
using Comfort.Common;
using EFT;
using EFT.Console.Core;
using EFT.UI;

namespace CWX_DebuggingTool
{
    [BepInPlugin("com.cwx.debuggingtool", "cwx-debuggingtool", "1.0.0")]
    public class DebuggingTool : BaseUnityPlugin
    {
        private void Awake()
        {
            ConsoleScreen.Processor.RegisterCommandGroup<DebuggingTool>();
        }

        [ConsoleCommand("BotMonitor")]
        public static void BotMonitorConsoleCommand([ConsoleArgument("", "Options: 0 = off, 1 = on")] int value )
        {
            switch (value)
            {
                case 0:
                    DisableBotMonitor();
                    ConsoleScreen.Log("BotMonitor disabled");
                    break;
                case 1:
                    EnableBotMonitor();
                    ConsoleScreen.Log("BotMonitor enabled");
                    break;
                default:
                    // fail to load, or wrong option used
                    ConsoleScreen.LogError("Wrong Option used, please use 0 or 1");
                    break;
            }
        }

        public static void DisableBotMonitor()
        {
            BotmonClass.Instance.Dispose();
        }

        public static void EnableBotMonitor()
        {
            var botmon = BotmonClass.Instance;

            var gameWorld = Singleton<GameWorld>.Instance;

            gameWorld.GetOrAddComponent<BotmonClass>();
        }
    }
}