using EFT.UI;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Comfort.Common;
using EFT;
using System.Drawing;

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

        private float updateTimer;
        private float updateRate = 5f;
        private float GuiTimer;
        private float GuiRate = 5f;

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

            // get all zones
            _zones = LocationScene.GetAllObjects<BotZone>().ToList();

            foreach (var zone in _zones)
            {
                // add zones and a new list of players
                _zoneAndPlayers.Add(zone.NameZone, new List<Player>());
            }

            // get player
            _player = _gameWorld.AllPlayers.Find(x => x.IsYourPlayer);

        }
        public void Update()
        {
            if (!Application.isFocused) return;

            updateTimer += Time.deltaTime;

            if (updateTimer < updateRate) return;

            updateTimer = 0;

            if (_gameWorld == null) return;

            foreach (var z in _zoneAndPlayers)
            {
                z.Value.Clear();
            }

            if (_gameWorld.AllPlayers.Count > 1)
            {
                foreach (var player in _gameWorld.AllPlayers)
                {
                    if (!player.IsYourPlayer)
                    {
                        var zoneName = player.AIData.BotOwner.BotsGroup.BotZone.NameZone;

                        if (_zoneAndPlayers.ContainsKey(zoneName) && !_zoneAndPlayers[zoneName].Contains(player))
                        {
                            _zoneAndPlayers[zoneName].Add(player);
                        }
                    }
                }
            }

            foreach (var pair in _zoneAndPlayers)
            {
                foreach (var person in pair.Value)
                {
                    if (!person.HealthController.IsAlive)
                    {
                        pair.Value.Remove(person);
                    }
                }
            }
        }

        public void OnGUI()
        {
            GuiTimer += Time.deltaTime;

            if (GuiRate < GuiTimer) return;

            GuiTimer = 0;

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

            _stringBuilder.Clear();

            if (_zoneAndPlayers != null)
            {
                var total = 0;

                foreach (var zone in _zoneAndPlayers)
                {
                    if (_zoneAndPlayers[zone.Key].Count > 0)
                    {
                        _stringBuilder.AppendLine($"{zone.Key} = {_zoneAndPlayers[zone.Key].Count}");
                    }

                    foreach (var player in _zoneAndPlayers[zone.Key])
                    {
                        total++;
                        var distance = Vector3.Distance(player.Position, _player.Position);
                        _stringBuilder.AppendLine($"> [{distance:n2}m] [{player.Profile.Info.Settings.Role}] [{player.Profile.Side}] {player.Profile.Nickname}");
                    }
                }

                _stringBuilder.PrependLine($"Total = {total}");
            }

            _guiContent.text = _stringBuilder.ToString();

            var size = _textStyle.CalcSize(_guiContent);

            GUI.Box(new Rect(Screen.width - size.x - 5f, 60f, size.x, size.y), _guiContent, _textStyle);
        }
    }
}