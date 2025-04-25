using BepInEx;
using HarmonyLib;
using Nautilus.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleFramework.VehicleTypes;
using System.Collections;
using VehicleFramework;
using SMA17Numi;

namespace SMA17Numi
{
    public static class Logger
    {
        public static void Log(string message)
        {
            UnityEngine.Debug.Log("[Numi]:" + message);
        }
        public static void Output(string msg)
        {
            BasicText message = new BasicText(500, 0);
            message.ShowMessage(msg, 5);
        }
    }
    [BepInPlugin("com.royalty.subnautica.Numi.mod", "Numi", "1.0.5")]
    [BepInDependency("com.mikjaw.subnautica.vehicleframework.mod")]
    [BepInDependency("com.snmodding.nautilus")]
    [BepInDependency("com.Bobasaur.AircraftLib")]

    public class MainPatcher : BaseUnityPlugin
    {
        public void Start()
        {
            var harmony = new Harmony("com.royalty.subnautica.Numi.mod");
            harmony.PatchAll();
            UWE.CoroutineHost.StartCoroutine(Numi.Register());
        }


    }
}
