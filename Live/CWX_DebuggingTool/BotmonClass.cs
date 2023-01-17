using EFT.UI;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Comfort.Common;
using EFT;

namespace CWX_DebuggingTool
{
    public sealed class BotmonClass : MonoBehaviour, IDisposable
    {
        private static BotmonClass _instance = null;
        private GUIContent _guiContent = null;
        private GUIStyle _textStyle;
        private StringBuilder _stringBuilder = new StringBuilder(200);
        private Player _player;
        private Dictionary<string, List<Player>> _zoneAndPlayers = new Dictionary<string, List<Player>>();
        private List<BotZone> _zones = null;
        private GameWorld _gameWorld = null;
        private IBotGame _botGame;
        private Rect _rect;
        private String _content = "";
        private Vector2 _guiSize;
        private float _distance;
        private float _timer;

        private BotmonClass()
        {

        }

        public static BotmonClass Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BotmonClass();
                }

                return _instance;
            }
        }

        public void Dispose()
        {
            var gameWorld = Singleton<GameWorld>.Instance;

            var gameobj = gameWorld.GetComponent<BotmonClass>();
            Destroy(gameobj);
            _instance = null;
            GC.SuppressFinalize(this);
        }

        ~BotmonClass()
        {
            ConsoleScreen.Log("BotMonitor Disabled on game end");
        }

        public void Awake()
        {
            // Set Basics
            _gameWorld = Singleton<GameWorld>.Instance;

            _botGame = Singleton<IBotGame>.Instance;

            _zones = LocationScene.GetAllObjects<BotZone>().ToList();

            foreach (var botZone in _zones)
            {
                _zoneAndPlayers.Add(botZone.name, new List<Player>());
            }

            // get player
            _player = _gameWorld.AllPlayers.Find(x => x.IsYourPlayer);

            if (_gameWorld.AllPlayers.Count > 1)
            {
                foreach (var player in _gameWorld.AllPlayers)
                {
                    if (!player.IsYourPlayer)
                    {
                        var theirZone = player.AIData.BotOwner.BotsGroup.BotZone.NameZone;

                        _zoneAndPlayers[theirZone].Add(player);
                    }
                }
            }

            _rect = new Rect(0, 60, 0, 0);

            _botGame.BotsController.BotSpawner.OnBotCreated += owner =>
            {
                Player player = owner.GetPlayer;
                var theirZone = player.AIData.BotOwner.BotsGroup.BotZone.NameZone;

                _zoneAndPlayers[theirZone].Add(player);
            };
        }

        public void OnGUI()
        {
            // set basics on GUI
            if (_textStyle == null)
            {
                _textStyle = new GUIStyle(GUI.skin.box);
                _textStyle.alignment = TextAnchor.MiddleLeft;
                _textStyle.fontSize = 20;
                _textStyle.margin = new RectOffset(3, 3, 3, 3);
            }

            // new GUI Content
            if (_guiContent == null)
            {
                _guiContent = new GUIContent();
            }
            
            _content = string.Empty;

            if (_zoneAndPlayers != null)
            {
                _content += $"Total = {_gameWorld.AllPlayers.Count - 1}\n";

                foreach (var zone in _zoneAndPlayers)
                {
                    _content += $"{zone.Key} = {_zoneAndPlayers[zone.Key].FindAll(x => x.HealthController.IsAlive).Count}\n";

                    foreach (var player in _zoneAndPlayers[zone.Key])
                    {
                        if (!player.HealthController.IsAlive)
                        {
                            continue;
                        }

                        _distance = Vector3.Distance(player.Position, _player.Position);
                        _content += $"> [{_distance:n2}m] [{player.Profile.Info.Settings.Role}] [{player.Profile.Side}] [{player.Profile.Info.Settings.BotDifficulty}] {player.Profile.Nickname}\n";
                    }
                }
            }

            _guiContent.text = _content;

            _guiSize = _textStyle.CalcSize(_guiContent);

            _rect.x = Screen.width - _guiSize.x - 5f;
            _rect.width = _guiSize.x;
            _rect.height = _guiSize.y;

            GUI.Box(_rect, _guiContent, _textStyle);
        }
    }
}