using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework;
using VehicleFramework.VehicleParts;
using VehicleFramework.VehicleTypes;
using System.IO;
using System.Reflection;
using UnityEngine.U2D;
using System.Collections;
using UWE;
using static Nautilus.Assets.PrefabTemplates.FabricatorTemplate;
using UnityEngine.Assertions;
using VehicleFramework.Assets;
using AircraftLib;
using VehicleFramework.Engines;
using AircraftLib.Engines;
using AircraftLib.VehicleTypes;
using AircraftLib.Managers;

namespace SMA17Numi
{
    public class Numi : PlaneVehicle
    {
        public static IEnumerator Register()
        {
            GetAssets();
            Submersible Numi = assets.model.EnsureComponent<Numi>();
            Numi.name = "Numi";
            yield return CoroutineHost.StartCoroutine(VehicleRegistrar.RegisterVehicle(Numi));
            yield break;
        }
        public static VehicleFramework.Assets.VehicleAssets assets;
        public static void GetAssets()
        {
            assets = AssetBundleInterface.GetVehicleAssetsFromBundle("assets/numi", "numi_vehicle", "SpriteAtlas", "NumiPing", "NumiCrafter");
        }
        public override Atlas.Sprite CraftingSprite => VehicleFramework.Assets.SpriteHelper.GetSprite("assets/NumiCrafter.png");
        public override Atlas.Sprite PingSprite => VehicleFramework.Assets.SpriteHelper.GetSprite("assets/NumiPing.png");
        public override VehiclePilotSeat PilotSeat
        {
            get
            {
                VehicleFramework.VehicleParts.VehiclePilotSeat vps = new VehicleFramework.VehicleParts.VehiclePilotSeat();
                Transform mainSeat = transform.Find("Pilotseat");
                vps.Seat = mainSeat.gameObject;
                vps.SitLocation = mainSeat.Find("SitLocation").gameObject;
                vps.LeftHandLocation = mainSeat;
                vps.RightHandLocation = mainSeat;
                return vps;
            }
        }

        public override List<VehicleHatchStruct> Hatches
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleHatchStruct>();

                foreach (Transform tr in CollisionModel.transform)
                {
                    VehicleFramework.VehicleParts.VehicleHatchStruct thisHatch = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                    thisHatch.Hatch = tr.gameObject;
                    thisHatch.ExitLocation = transform.Find("Hatch/ExitPosition");
                    thisHatch.SurfaceExitLocation = thisHatch.ExitLocation;
                    list.Add(thisHatch);
                }

                return list;
            }
        }

        public override GameObject VehicleModel
        {
            get
            {
                return assets.model;
            }
        }

        public override GameObject CollisionModel
        {
            get
            {
                return transform.Find("Collider").gameObject;
            }
        }

        public override GameObject StorageRootObject
        {
            get
            {
                return transform.Find("StorageRoot").gameObject;
            }
        }

        public override GameObject ModulesRootObject
        {
            get
            {
                return transform.Find("ModulesRoot").gameObject;
            }
        }
        public override List<VehicleUpgrades> Upgrades
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleUpgrades>();
                VehicleFramework.VehicleParts.VehicleUpgrades vu = new VehicleFramework.VehicleParts.VehicleUpgrades();
                vu.Interface = transform.Find("Upgrades").gameObject;
                vu.Flap = vu.Interface;
                list.Add(vu);
                return list;
            }
        }

        public override List<VehicleBattery> Batteries
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleBattery>();

                VehicleFramework.VehicleParts.VehicleBattery vb1 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb1.BatterySlot = transform.Find("Batteries/Battery1").gameObject;
                list.Add(vb1);

                VehicleFramework.VehicleParts.VehicleBattery vb2 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb2.BatterySlot = transform.Find("Batteries/Battery2").gameObject;
                list.Add(vb2);

                return list;
            }
        }

        public override List<VehicleFloodLight> HeadLights
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleFloodLight>();

                VehicleFramework.VehicleParts.VehicleFloodLight Front = new VehicleFramework.VehicleParts.VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/headlights/Front").gameObject,
                    Angle = 70,
                    Color = Color.white,
                    Intensity = 1.3f,
                    Range = 90f
                };
                list.Add(Front);
                return list;
            }
        }


        public override List<GameObject> WaterClipProxies
        {
            get
            {
                var list = new List<GameObject>();
                foreach (Transform child in transform.Find("WaterClipProxies"))
                {
                    list.Add(child.gameObject);
                }
                return list;
            }
        }

        public override List<GameObject> CanopyWindows
        {
            get
            {
                var list = new List<GameObject>();
                //list.Add(transform.Find("Model/Canopy").gameObject);
                return list;
            }
        }

        public override BoxCollider BoundingBoxCollider
        {
            get
            {
                return transform.Find("BoundingBoxCollider").gameObject.GetComponent<BoxCollider>();
            }
        }

        public override Dictionary<TechType, int> Recipe
        {
            get
            {
                Dictionary<TechType, int> recipe = new Dictionary<TechType, int>();
                recipe.Add(TechType.PlasteelIngot, 2);
                recipe.Add(TechType.PowerCell, 2);
                recipe.Add(TechType.EnameledGlass, 1);
                recipe.Add(TechType.AdvancedWiringKit, 2);
                return recipe;
            }
        }
        public override string vehicleDefaultName
        {
            get
            {
                Language main = Language.main;
                bool flag = !(main != null);
                string result;
                if (flag)
                {
                    result = "SMA-17 Numi";
                }
                else
                {
                    result = main.Get("SMA-17 Numi");
                }
                return result;
            }
        }

        public override Sprite EncyclopediaImage => VehicleFramework.Assets.SpriteHelper.GetSpriteRaw("assets/NumiDatabank.png");
     

        public override string Description => "Super Manuverability Aircraft 17";

        public override string EncyclopediaEntry => "Warning: Following vehicle is not designed nor produced by Alterra.\nIts effectiveness and specifications might not be at the Alterra standards and may pose danger to its user.\nThis technology cannot be fully read due to the difference in Alterra and Verdent corporation’s blueprint systems, but we will try to interpret this blueprint's features as closely as possible and for enhanced user experience it will use Alterra hud.\nAll its weapon systems will be completely removed, due to the structural integration of its gun the aircraft will retain its gun but will be completely disabled and locked.\nFollowing user to-read information is directly provided by Verdent Corporation.\n\nSuperManeuverability Aircraft-17 is a military aircraft produced by Verdent corporation.\nIt is the first aircraft to use air-breathing electric jet engines. These engines use the air passing through and rapidly ionize it, providing a large amount of thrust.\n\nIt possesses an 8-barrel 25mm gun [Weapon was disabled]\nTypically possesses 8 hardpoints [Hardpoints were completely removed]\n\nIt uses traditional gauges and instruments in the cockpit, providing accurate data about the surroundings [those systems were disabled due to integration to the Alterra hud]\n\nIt is powered by two power cells due to the substantial efficiency of the engines."
;

        public override int BaseCrushDepth => 30;

        public override int CrushDepthUpgrade1 => 20;

        public override int CrushDepthUpgrade2 => 50;

        public override int CrushDepthUpgrade3 => 50;

        public override int MaxHealth => 1600;

        public override int Mass => 5000;
        public override int NumModules => 6;

        public override void Start()
        {
            base.Start();
            SetupMagnetBoots();
        }
        public void MyAttach()
        {
            collisionModel.SetActive(true);
            useRigidbody.detectCollisions = true;
            transform.Find("LandingGear/GearCollider/1").GetComponent<Collider>().enabled = false;
            transform.Find("BoundingBoxCollider").GetComponent<Collider>().enabled = false;
        }
        public void MyDetach()
        {
            collisionModel.SetActive(true);
            useRigidbody.detectCollisions = true;
            transform.Find("LandingGear/GearCollider/1").GetComponent<Collider>().enabled = true;
            transform.Find("BoundingBoxCollider").GetComponent<Collider>().enabled = false;
        }
        public void SetupMagnetBoots()
        {
            var magBoots = gameObject.EnsureComponent<VehicleFramework.VehicleComponents.MagnetBoots>();
            magBoots.MagnetDistance = 5f;
            magBoots.recharges = true;
            magBoots.rechargeRate = 0.1f;
            magBoots.Attach = MyAttach;
            magBoots.Detach = MyDetach;
        }
        public void Attach()
        {
            gameObject.EnsureComponent<VehicleFramework.VehicleComponents.VFArmsManager>().ShowArms(false);
        }
        public void Detach()
        {
            gameObject.EnsureComponent<VehicleFramework.VehicleComponents.VFArmsManager>().ShowArms(true);
        }

        public override ModVehicleEngine Engine
        {
            get
            {
                return Radical.EnsureComponent<PlaneRCAgileEngine>(base.gameObject);
            }
        }
        protected float _maxSpeed = 200f;
        public override float maxSpeed
        {
            get
            {
                return _maxSpeed;
            }
            set
            {
                _maxSpeed = value;
            }
        }

        protected float _takeoffSpeed = 40f;
        public override float takeoffSpeed
        {
            get
            {
                return _takeoffSpeed;
            }
            set
            {
                _takeoffSpeed = value;
            }
        }
        public override void Update()
        {
            base.Update();
            FlightManager.CheckLandingGear(this);
        }
    }
}