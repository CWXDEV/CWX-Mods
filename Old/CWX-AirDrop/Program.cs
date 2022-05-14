using BepInEx;
using Comfort.Common;
using EFT;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using System;
using AirdropLogic2Class = GClass778;  // wont be needed once we move to latest modules

namespace AirDrop
{
    [BepInPlugin("com.aki.secret", "aki-secret", "1.0.0")]
    public class AirDrop : BaseUnityPlugin
    {
        public static GameWorld gameWorld;
        public static SynchronizableObject planes; // first plane in list which is always called: IL76MD-90
        public static SynchronizableObject boxes; // first box in the list which is always called: scontainer_airdrop_box_04
        public static bool planesEnabled = false; // used to check if the plane is currently visible on the map and Init'd
        public static bool boxesEnabled = false; // used to check if the box is currently visible on the map and Init'd
        public static int amountDropped; // used to make sure the EnableObjects.Start(); on line60 does not keep repeating
        public static int dropChance = 20; // used to check if the airdrop can spawn from chance
        public static List<AirdropPoint> airdropPoints; // list of airdrop points on the map loaded
        public static AirdropPoint randomAirdropPoint; // one random point picked to drop box too
        public static int boxObjId = 10; // random number selected thats not the same as the plane can be

        public static Vector3 boxPosition; // randomAirdropPoints Position Coords (used as a targetPosition)
        public static Vector3 planeStartPosition; // planes starting position which can be 4 different ones definded in EnableObjects.PlaneStartGen()
        public static Vector3 planeStartRotation; // planes starting rotation which can be 4 different ones from above (might not be needed as we rotate to target)
        public static int planeObjId; // range from 1 - 4 using RandomChanceGen, used planeObjId as a spawnpoint number as well as what EFT needs to init
        public static float planeEndPointPositive = 3000f;
        public static float planeEndPointNegative = -3000f;
        public static float defaultDropHeight = 400f;

        public static float timer; // starting timer
        public static float timeToDrop; // time when the plane should start moving in seconds

        public static bool doNotRun = false; // used to determine if map has planes and drops, line 130

        // TODO:
        // once server is converted to TS attach to server endpoint for chances and timeToDrop
        // ask for advice on refactor, make things easier to read/better

        public void Start()
        {
            new Patch().Enable();
        }

        public void FixedUpdate() // FixedUpdate executes 50 times per second. https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
        {
            if (gameWorld == null)
            {
                return;
            }

            timer += 0.02f;

            if (timer >= timeToDrop && !planesEnabled && amountDropped != 1 && !doNotRun)
            {
                EnableObjects.Start();
            }

            if (timer >= timeToDrop && planesEnabled && !doNotRun)
            {
                planes.transform.Translate(Vector3.forward, Space.Self); // transform foward on local rotation using Space.Self, vector3.forward moves forward 1f per run

                switch (AirDrop.planeObjId)
                {
                case 1:
                    if (planes.transform.position.z >= planeEndPointPositive && planesEnabled) // spawn 1 moves along Z axis and goes in the positive direction, 
                        {
                        DisableObjects.DisablePlanes(planes);
                    }

                    if (planes.transform.position.z >= randomAirdropPoint.transform.position.z && !boxesEnabled)
                    {
                        EnableObjects.InitDrop(boxes);
                    }
                    break;
                case 2:
                    if (planes.transform.position.x >= planeEndPointPositive && planesEnabled) // spawn 2 moves along x axis and goes in the positive direction
                    {
                        DisableObjects.DisablePlanes(planes);
                    }

                    if (planes.transform.position.x >= randomAirdropPoint.transform.position.x && !boxesEnabled)
                    {
                        EnableObjects.InitDrop(boxes);
                    }
                    break;
                case 3:
                    if (planes.transform.position.z <= planeEndPointNegative && planesEnabled) // spawn 3 moves along z axis and goes in the negative direction
                    {
                        DisableObjects.DisablePlanes(planes);
                    }

                    if (planes.transform.position.z <= randomAirdropPoint.transform.position.z && !boxesEnabled)
                    {
                        EnableObjects.InitDrop(boxes);
                    }
                    break;
                case 4:
                    if (planes.transform.position.x <= planeEndPointNegative && planesEnabled) // spawn 2 moves along x axis and goes in the negative direction
                        {
                        DisableObjects.DisablePlanes(planes);
                    }

                    if (planes.transform.position.x <= randomAirdropPoint.transform.position.x && !boxesEnabled)
                    {
                        EnableObjects.InitDrop(boxes);
                    }
                    break;
                }
            }
        }

        public static int RandomChanceGen(int minValue, int maxValue)
        {
            System.Random chance = new System.Random();
            return chance.Next(minValue, maxValue);
        }

        public static void InitObjects()
        {
            // this method sets to default, checks if indoors map, gets new objects
            boxesEnabled = false;
            planesEnabled = false;
            doNotRun = false;
            timer = 0f;
            amountDropped = 0;

            gameWorld = Singleton<GameWorld>.Instance;
            var player = gameWorld.RegisteredPlayers[0];
            if (player != null)
            {
                if (player.Location.Contains("factory") || player.Location.Contains("laboratory") || player.Location.Contains("hideout"))
                {
                    doNotRun = true; // will stay true till next map load
                    return;
                }
            }
            AirDrop.timeToDrop = RandomChanceGen(60, 900);
            planes = LocationScene.GetAll<SynchronizableObject>().First(x => x.name.Contains("IL76MD-90"));
            boxes = LocationScene.GetAll<SynchronizableObject>().First(x => x.name.Contains("scontainer_airdrop_box_04"));
            airdropPoints = LocationScene.GetAll<AirdropPoint>().ToList();
            randomAirdropPoint = airdropPoints.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
        }
    }

    public static class EnableObjects
    {
        public static void Start()
        {
            if (AirDrop.boxes != null)
            {
                AirDrop.boxPosition = AirDrop.randomAirdropPoint.transform.position;
                AirDrop.boxPosition.y = AirDrop.defaultDropHeight;
            }

            if (AirDrop.planes != null)
            {
                PlaneStartGen();
            }
        }

        public static void PlaneStartGen()
        {
            AirDrop.planeObjId = AirDrop.RandomChanceGen(1, 4);

            switch (AirDrop.planeObjId)
            {
            case 1:
                AirDrop.planeStartPosition = new Vector3(0, AirDrop.defaultDropHeight, -3000);
                AirDrop.planeStartRotation = new Vector3(0, 0, 0);
                break;
            case 2:
                AirDrop.planeStartPosition = new Vector3(-3000, AirDrop.defaultDropHeight, 0);
                AirDrop.planeStartRotation = new Vector3(0, 90, 0);
                break;
            case 3:
                AirDrop.planeStartPosition = new Vector3(0, AirDrop.defaultDropHeight, 3000);
                AirDrop.planeStartRotation = new Vector3(0, 180, 0);
                break;
            case 4:
                AirDrop.planeStartPosition = new Vector3(3000, AirDrop.defaultDropHeight, 0);
                AirDrop.planeStartRotation = new Vector3(0, 270, 0);
                break;
            }

            InitPlane(AirDrop.planes);
        }

        public static void InitPlane(SynchronizableObject plane)
        {
            AirDrop.planesEnabled = true;
            plane.TakeFromPool();
            plane.Init(AirDrop.planeObjId, AirDrop.planeStartPosition, AirDrop.planeStartRotation);
            plane.transform.LookAt(AirDrop.boxPosition); // turns the plane to look at the box drop point
            plane.ManualUpdate(0);

            var sound = plane.GetComponentInChildren<AudioSource>();
            sound.volume = 1f;
            sound.dopplerLevel = 1;
            sound.Play();
        }

        public static void InitDrop(SynchronizableObject box)
        {
            // this is basically a copy of the debugAirdrop script BSG has in the syncProcess
            object[] passToList = new object[1];
            passToList[0] = SynchronizableObjectType.AirDrop;
            var syncProcess = AirDrop.gameWorld.SynchronizableObjectLogicProcessor;
            syncProcess.GetType().GetMethod("method_9", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(syncProcess, passToList);
            // if the drop is not in list_0, the box will not move, the above adds the box to that list

            AirDrop.boxesEnabled = true;
            box.SetLogic(new AirdropLogic2Class());
            box.ReturnToPool();
            box.TakeFromPool();
            box.Init(AirDrop.boxObjId, AirDrop.boxPosition, Vector3.zero);
        }
    }

    public static class DisableObjects
    {
        public static void DisablePlanes(SynchronizableObject plane)
        {
            AirDrop.planesEnabled = false;
            AirDrop.amountDropped = 1;
            plane.ReturnToPool();
        }
    }
}
