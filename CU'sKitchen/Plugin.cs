using BepInEx;
using BepInEx.Logging;
using CasualtiesExtra;
using CasualtiesExtra.UIScripts;
using CUCoreLib.Data;
// Used for CUCoreLib features. Remove if you don't use any of said features :)
using CUCoreLib.Helpers;
using CUCoreLib.Registries;
using CUCoreLib.Saving;
using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
//using MonoMod.RuntimeDetour;
using UnityEngine;
using UnityEngine.Timeline;
using static UnityEngine.EventSystems.EventTrigger;
//using Newtonsoft.Json.Linq;

namespace CU_sKitchen
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [BepInDependency("net.cucorelib", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string ModGUID = "com.MrMaker.CUsKitchen";
        public const string ModName = "Casualties Unknown's Kitchen";
        public const string ModVersion = "1.2.0";

        internal static new ManualLogSource Logger;
        private readonly Harmony _harmony = new(ModGUID);
        public static Plugin Instance { get; private set; } = null!;

        Sprite appleSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Apple.png");
        //Sprite appleSliceSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/AppleSlice.png");
        Sprite appleTreeSprite = AssetLoader.LoadEmbeddedSprite("Sprites.AppleTree.png");
        Sprite applePieSprite = AssetLoader.LoadEmbeddedSprite("Sprites.ApplePie.png");
        //Sprite knifeSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/Knife.png");
        Sprite ovenSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Oven.png");
        Sprite microwaveSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Microwave.png");
        Sprite pepperSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Pepper.png");
        Sprite pepperPlantSprite = AssetLoader.LoadEmbeddedSprite("Sprites.PepperPlant.png");
        Sprite tomatoSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Tomato.png");
        Sprite tomatoPlantSprite = AssetLoader.LoadEmbeddedSprite("Sprites.TomatoPlant.png");

        //Sprite appleSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/Apple.png");
        ////Sprite appleSliceSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/AppleSlice.png");
        //Sprite appleTreeSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/AppleTree.png");
        //Sprite applePieSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/ApplePie.png");
        ////Sprite knifeSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/Knife.png");
        //Sprite ovenSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/Oven.png");
        //Sprite microwaveSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/Microwave.png");
        //Sprite pepperSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/Pepper.png");
        //Sprite pepperPlantSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/PepperPlant.png");
        //Sprite tomatoSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/Tomato.png");
        //Sprite tomatoPlantSprite = AssetLoader.LoadSpriteFromPluginFolder(Instance, "Casualties Unknown's Kitchen/Sprites/TomatoPlant.png");

        public void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            _harmony.PatchAll();
            Logger.LogInfo($"Plugin {ModName} is loaded!");

            CustomItems();
            CustomLiquids();
            CustomBuildings();
            CustomRecipes();

            if (appleSprite == null) appleSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Apple.png");
            //if (appleSliceSprite == null) appleSliceSprite = AssetLoader.LoadEmbeddedSprite("Sprites.AppleSlice.png");
            if (appleTreeSprite == null) appleTreeSprite = AssetLoader.LoadEmbeddedSprite("Sprites.AppleTree.png");
            if (applePieSprite == null) applePieSprite = AssetLoader.LoadEmbeddedSprite("Sprites.ApplePie.png");
            //if (knifeSprite == null) knifeSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Knife.png");
            if (ovenSprite == null) ovenSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Oven.png");
            if (microwaveSprite == null) microwaveSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Microwave.png");
            if (pepperSprite == null) pepperSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Pepper.png");
            if (pepperPlantSprite == null) pepperPlantSprite = AssetLoader.LoadEmbeddedSprite("Sprites.PepperPlant.png");
            if (tomatoSprite == null) tomatoSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Tomato.png");
            if (tomatoPlantSprite == null) tomatoPlantSprite = AssetLoader.LoadEmbeddedSprite("Sprites.TomatoPlant.png");
        }

        //ItemInfo closestItem = new RecipeItem(0.5f) { quality = new CraftingQuality("") }

        public void CustomBuildings()
        {
            BuildingEntityRegistry.Register("appletree", new CustomBuildingEntityDefinition
            {
                Name = "Apple Tree",
                Description = "A tall tree that seems to bear some sort of red fruit",
                Sprite = appleTreeSprite,
                Health = 400f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.07f,
                SpawnMaxPerChunk = 0.014f,
                SurfaceOffset = 3f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("apple"),
                    BuildingEntityRegistry.AddDrop("woodscraps")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("apple", 0.75f, 0.5f, 1f)
                }
            });
            BuildingEntityRegistry.Register("pepperplant", new CustomBuildingEntityDefinition
            {
                Name = "Capsicum Shrub",
                Description = "A small but healthy shrub, what grows on it are thin red fruit. Are they edible?",
                Sprite = pepperPlantSprite,
                Health = 200f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.07f,
                SpawnMaxPerChunk = 0.014f,
                SurfaceOffset = 1.5f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("pepper"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("pepper", 0.5f, 0.4f, 0.1f),
                }
            });
            BuildingEntityRegistry.Register("tomatoplant", new CustomBuildingEntityDefinition
            {
                Name = "Tomato Bush",
                Description = "A round green bush, houses red circular fruits",
                Sprite = tomatoPlantSprite,
                Health = 250f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.07f,
                SpawnMaxPerChunk = 0.014f,
                SurfaceOffset = 2f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
    {
                    BuildingEntityRegistry.AddDrop("tomato"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
    {
                    BuildingEntityRegistry.AddDrop("tomato", 0.5f, 0.4f, 0.1f),
                }
            });
            //BuildingEntityRegistry.Register("oven", new CustomBuildingEntityDefinition
            //{
            //    Name = "Oven",
            //    Description = "A moderately large and sturdy machine that has enclosure inside that acts as a heatsource to cook various foods",
            //    Sprite = ovenSprite,
            //    Health = 800f,
            //    HitSoundReferenceId = "metal",
            //    Placement = BuildingPlacementType.Floor,
            //    SpawnMinPerChunk = 0.02f,
            //    SpawnMaxPerChunk = 0.05f,
            //    SurfaceOffset = 2f,
            //    RequireGround = true,
            //    Metallic = true,
            //    //GenerationStyle = BuildingGenerationStyle.Standard,
            //    Components = new[] { typeof(OvenScript) },
            //    AlwaysDrop = new[]
            //    {
            //        BuildingEntityRegistry.AddDrop("circuitboard"),
            //        BuildingEntityRegistry.AddDrop("scrapmetal")
            //    },
            //});
        }

        public void CustomItems()
        {
            ItemRegistry.Register("apple", new ItemInfo
            {
                fullName = "Apple",
                description = "A red apple, sweet and healthy to eat",
                category = "food",
                weight = 0.5f,
                value = 2,
                usable = true,
                decayMinutes = 30f,
                scaleWeightWithCondition = true,
                tags = "fruit" + "cangetwet" + "sliceable",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(5f, 0.2f);
                    body.Drink(1f);
                    body.happiness += 0.25f;
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality>
                {
                    new CraftingQuality("produce"),
                    new CraftingQuality("sliceable")
                },
            }, appleSprite);
            ItemRegistry.Register("applepie", new ItemInfo
            {
                fullName = "Apple Pie",
                description = "A delicous and filling pie made with apples",
                category = "food",
                weight = 1.5f,
                value = 15,
                usable = true,
                decayMinutes = 90f,
                scaleWeightWithCondition = true,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction= (body, item) =>
                {
                    body.Eat(7f, 0.6f);
                    body.happiness += 0.75f;
                    body.talker.EatGood();
                    item.condition -= 0.125f;
                },
            }, applePieSprite);
            ItemRegistry.Register("oven", new ItemInfo
            {
                fullName = "Oven",
                description = "A large metalic box that has heated inside, used to cook food",
                category = "custom",
                weight = 20f,
                value = 40,
                usable = false,
                rec = new Recognition(10),
                qualities = new List<CraftingQuality>
                {
                    new CraftingQuality("heatsource", 30)
                }
            }, ovenSprite);
            ItemRegistry.Register("microwave", new CustomItemInfo
            {
                fullName = "Microwave",
                description = "A compact metalic box that is capable of heating up and cooking food",
                category = "utility",
                weight = 2f,
                value = 30,
                usable = false,
                tags = "battery",
                rec = new Recognition(9),
                Battery = new BatteryProperties
                {
                    MaxCharge = 100f,
                    StartCharge = 100f,
                    Preset = BatteryItem.BatteryPreset.Large,
                    BatteryType = "largebattery"
                },
                qualities = new List<CraftingQuality>
                {
                    new CraftingQuality("heatsource", 15)
                }
            }, microwaveSprite);
            ItemRegistry.Register("pepper", new ItemInfo
            {
                fullName = "Pepper",
                description = "A red thin fruit, eating might bring discomfort",
                category = "food",
                weight = 0.4f,
                value = 4,
                usable = true,
                decayMinutes = 20f,
                scaleWeightWithCondition = true,
                tags = "fruit" + "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(9f, 0.4f);
                    body.Drink(-4f);
                    body.happiness -= 0.25f;
                    body.limbs[0].pain += 8f;
                    if (body.temperature < 40f)
                    {
                        body.temperature += 1f;
                    }
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality>
                {
                    new CraftingQuality("produce"),
                },
            }, pepperSprite);
            ItemRegistry.Register("tomato", new ItemInfo
            {
                fullName = "Tomato",
                description = "A soft red fruit, a little sickening if eaten too much",
                category = "food",
                weight = 0.4f,
                value = 3,
                usable = true,
                decayMinutes = 40f,
                scaleWeightWithCondition = true,
                tags = "fruit" + "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(4f, 0.2f);
                    body.Drink(3f);
                    body.sicknessAmount += 1f;
                    item.condition -= 0.5f;
                },
                qualities = new List<CraftingQuality>
                {
                    new CraftingQuality("produce"),
                },
            }, tomatoSprite);
            //ItemRegistry.Register("appleslice", new ItemInfo
            //{
            //    fullName = "Apple Slice",
            //    description = "A simple slice of apple, small and light but not very filling",
            //    category = "food",
            //    weight = 0.125f,
            //    value = 1,
            //    usable = true,
            //    decayMinutes = 10,
            //    scaleWeightWithCondition = true,
            //    tags = "fruit" + "cangetwet",
            //    rec = new Recognition(2),
            //    useAction = (body, item) =>
            //    {
            //        body.Eat(1.5f, 0.05f);
            //        body.Drink(0.25f);
            //        body.happiness += 0.1f;
            //        item.condition -= 1f;
            //    },
            //}, appleSliceSprite);
            //ItemRegistry.Register("knife", new ItemInfo
            //{
            //    fullName = "Kitchen Knife",
            //    description = "A sturdy knife, often used to slice food into pieces",
            //    weight = 0.8f,
            //    value = 8,
            //    usable = true,
            //    tags = "tool" + "belttool",
            //    rec = new Recognition(6),
            //    useAction = (body, item) =>
            //    {
            //        Item helditem = PlayerCamera.main.body.GetItem(PlayerCamera.main.body.handSlot);
            //        if (helditem.tag == "sliceable")
            //        {

            //            string itemid;
            //            itemid = helditem.id;
            //            item.condition -= 1f;
            //            body.PickUpItem(Item.GetItem("applepie"), body.DropItem(1), force = false);
            //            //Utils.Create(itemid + "slice", PlayerCamera.main.body.transform.position, 0f).GetComponent<Item>();
            //        }
            //        else return;
            //    },
            //    qualities = new List<CraftingQuality>
            //    {
            //        new CraftingQuality("cutting", 16),
            //    },
            //}, knifeSprite);
        }

        public void CustomLiquids()
        {
            LiquidRegistry.Register("geojam", new CustomLiquidInfo
            {
                name = "Geo-Jam",
                description = "A blue-hued jam made from geofruit. Basic, accessible, but not very filling",
                color = new Color(0.478f, 0.894f, 1f),
                valuePerLiter = 30,
                healthUsable = false,
                onDrink = (ml, body) =>
                {
                    float liters = ml * 0.01f;

                    body.Drink(liters * 10f);
                    body.Eat(liters * 7f, liters * 0.2f);
                    body.temperature -= liters * 0.2f;
                    body.happiness += liters * 0.5f;
                    body.talker.EatGood();
                },
                onHealthUse = (ml, limb) =>
                {
                    float dose = ml * 0.01f;

                    limb.body.happiness -= dose * 12f;
                    limb.body.bloodVolume += dose * 0.75f;
                    limb.body.sicknessAmount += dose * 10f;
                }
            });
            LiquidRegistry.Register("stonefruitjam", new CustomLiquidInfo
            {
                name = "Stonefruit-Jam",
                description = "A neutral-tasting, coarse spread with a green hue. Made with opened stonefruits. Due to it being a dry fruit, water is used to soften up into jam",
                color = new Color(0.427f, 0.902f, 0.549f),
                valuePerLiter = 38,
                healthUsable = false,
                onDrink = (ml, body) =>
                {
                    float liters = ml * 0.01f;

                    body.Drink(liters * 2f);
                    body.Eat(liters * 14f, liters * 0.4f);
                    body.happiness += liters * 0.3f;
                    body.talker.EatGood();
                },
                onHealthUse = (ml, limb) =>
                {
                    float dose = ml * 0.01f;

                    limb.body.happiness -= dose * 15f;
                    limb.body.bloodVolume += dose * 1f;
                    limb.body.sicknessAmount += dose * 15f;
                }
            });
            LiquidRegistry.Register("cactusjam", new CustomLiquidInfo
            {
                name = "Cactus-Jam",
                description = "A sweet jam made from cactus flesh, with all of its prickly spines removed. Watery, but yummy!",
                color = new Color(1f, 1f, 0.7f),
                valuePerLiter = 35,
                healthUsable = false,
                onDrink = (ml, body) =>
                {
                    float liters = ml * 0.01f;

                    body.Drink(liters * 8f);
                    body.Eat(liters * 7f, liters * 0.8f);
                    body.happiness += liters * 1.2f;
                    body.talker.EatGood();
                },
                onHealthUse = (ml, limb) =>
                {
                    float dose = ml * 0.01f;

                    limb.body.happiness -= dose * 10f;
                    limb.body.bloodVolume += dose * 0.6f;
                    limb.body.sicknessAmount += dose * 8f;
                }
            });
            LiquidRegistry.Register("poppingjam", new CustomLiquidInfo
            {
                name = "Popping-Jam",
                description = "A bright green jam that passively crackles. It emits a soft glow once disturbed... Its radiation effects have been refined which make it a medicine for radiation poisoning and it's tasty too!",
                color = new Color(0f, 0.9f, 0f),
                valuePerLiter = 45,
                healthUsable = false,
                onDrink = (ml, body) =>
                {
                    float liters = ml * 0.01f;

                    body.Drink(liters * 6f);
                    body.Eat(liters * 9f, liters * 0.15f);
                    body.happiness += liters * 0.8f;
                    body.talker.EatGood();
                    body.radiationSickness += liters * UnityEngine.Random.Range(-7.5f, -2.5f);
                },
                onHealthUse = (ml, limb) =>
                {
                    float dose = ml * 0.01f;

                    limb.body.happiness -= dose * 13f;
                    limb.body.bloodVolume += dose * 0.9f;
                    limb.body.sicknessAmount += dose * 12f;
                    limb.body.radiationSickness += dose * UnityEngine.Random.Range(-7.5f, -2.5f) * 1.5f;
                }
            });
            LiquidRegistry.Register("numbingjam", new CustomLiquidInfo
            {
                name = "Numbing-Jam",
                description = "A creamy, airy jam made from numberries. While it tastes disgusting, it can be applied directly to the skin to soothe aches. Lesser effect when eaten.",
                color = new Color(0.7f, 0f, 0.8f),
                valuePerLiter = 38,
                healthUsable = true,
                onDrink = (ml, body) =>
                {
                    float liters = ml * 0.01f;

                    body.Drink(liters * 6f);
                    body.Eat(liters * 8f, liters * -3f);
                    body.happiness += liters * -1.2f;
                    body.talker.EatBad();
                    foreach (Limb limbs in body.limbs)
                    {
                        CoUtils.instance.DoTimedOp("numbingjam" + limbs.name, delegate
                        {
                            limbs.pain = Mathf.Lerp(limbs.pain, limbs.pain * 0.1f, 0.3f);
                        }, 25f * liters);
                        //limbs.pain -= liters * 5f;
                    }
                },
                onHealthUse = (ml, limb) =>
                {
                    float dose = ml * 0.01f;
                    CoUtils.instance.DoTimedOp("numbingjam" + limb.name, delegate
                    {
                        limb.pain = Mathf.Lerp(limb.pain, limb.pain * 0.1f, 0.6f);
                    }, 60f * dose);
                    //limb.pain -= dose * 20;
                },
            });
            LiquidRegistry.Register("suspiciousjam", new CustomLiquidInfo
            {
                name = "Suspicious Jam",
                description = "A sticky, brown jam made with miscellaneous mushrooms smashed together. You don't know what it does or what it tastes like, but at least it's edible?",
                color = new Color(0.55f, 0.35f, 0f),
                valuePerLiter = 22,
                healthUsable = false,
                onDrink = (ml, body) =>
                {
                    float liters = ml * 0.01f;

                    body.Eat(liters * UnityEngine.Random.Range(12f, 18f), liters * UnityEngine.Random.Range(-0.8f, 1.2f));
                    body.happiness += liters * 1.2f;
                    float num = UnityEngine.Random.value * 100f;
                    if (num < 20)
                    {
                        body.energy += liters * 10f;
                        body.talker.EatGood();
                    }
                    else if (num < 40)
                    {
                        body.radiationSickness -= liters * 3f;
                        body.talker.EatMediocre();
                    }
                    else if (num < 60)
                    {
                        body.antibioticImmunityTime = 180f;
                        body.talker.EatMediocre();
                    }
                    else if (num < 80)
                    {
                        body.brainHealth += 2.5f;
                        body.talker.EatBad();
                    }
                    else
                    {
                        body.Eat(5f, 0f);
                        body.Drink(15f);
                        body.talker.EatGood();
                    }
                },
                onHealthUse = (ml, limb) =>
                {
                    float dose = ml * 0.01f;

                    limb.body.happiness -= dose * 14f;
                    limb.body.bloodVolume += dose * 0.8f;
                    limb.body.sicknessAmount += dose * 12f;
                }
            });
            LiquidRegistry.Register("bananamash", new CustomLiquidInfo
            {
                name = "Banana Mash",
                description = "Bananas mashed together into what roughly resembles a watery jam. It tastes really nice now without the peel, but is slightly more radioactive.",
                color = new Color(1f, 1f, 0.7f),
                valuePerLiter = 32,
                healthUsable = false,
                onDrink = (ml, body) =>
                {
                    float liters = ml * 0.01f;

                    body.Eat(liters * 25f, liters * 0.3f);
                    body.Drink(liters * 14f);
                    body.happiness += liters * 1.5f;
                    body.radiationSickness += liters * 3.5f;
                    body.talker.EatGood();
                },
                onHealthUse = (ml, limb) =>
                {
                    float dose = ml * 0.01f;

                    limb.body.happiness -= dose * 10f;
                    limb.body.bloodVolume += dose * 0.8f;
                    limb.body.sicknessAmount += dose * 10f;
                    limb.body.radiationSickness += dose * 5f;
                }
            });
        }

        public void CustomRecipes()
        {
            RecipeRegistry.Register(new Recipe
            {
                INT = 6,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "applepie",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "apple" },
                    new RecipeItem(0.5f) { specific = true, specificId = "apple" },
                    new RecipeItem(0.5f) { specific = true, specificId = "apple" },
                    new RecipeItem(0.5f) { quality = new CraftingQuality("flour")},
                    new RecipeItem(0f) { quality = new CraftingQuality("cutting", 1f), destroyItem = false},
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 1f), destroyItem = false}
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 4,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "geojam",
                    amount = 1,
                    isLiquid = true,
                    resultCondition = 300f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "geofruit" },
                    new RecipeItem(0.5f) { specific = true, specificId = "geofruit" },
                    new RecipeItem(0.5f) { specific = true, specificId = "geofruit" },
                    new RecipeItem(0.5f) { specific = true, specificId = "geofruit" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 0.1f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false },
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 8,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "stonefruitjam",
                    amount = 1,
                    isLiquid = true,
                    resultCondition = 300f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "stonefruitopen" },
                    new RecipeItem(0.5f) { specific = true, specificId = "stonefruitopen" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 0.1f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("water", 20f), isLiquid = true, destroyItem = false },
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 7,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "cactusjam",
                    amount = 1,
                    isLiquid = true,
                    resultCondition = 300f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "cactusflesh" },
                    new RecipeItem(0.5f) { specific = true, specificId = "cactusflesh" },
                    new RecipeItem(0.5f) { specific = true, specificId = "cactusflesh" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 0.1f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false },
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 9,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "poppingjam",
                    amount = 1,
                    isLiquid = true,
                    resultCondition = 300f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "popfruit" },
                    new RecipeItem(0.5f) { specific = true, specificId = "popfruit" },
                    new RecipeItem(0.5f) { specific = true, specificId = "popfruit" },
                    new RecipeItem(0.5f) { specific = true, specificId = "popfruit" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 0.1f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false },
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 9,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "numbingjam",
                    amount = 1,
                    isLiquid = true,
                    resultCondition = 300f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "mushpear" },
                    new RecipeItem(0.5f) { specific = true, specificId = "mushpear" },
                    new RecipeItem(0.5f) { specific = true, specificId = "mushpear" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 0.1f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false },
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 10,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "suspiciousjam",
                    amount = 1,
                    isLiquid = true,
                    resultCondition = 300f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "browncap" },
                    new RecipeItem(0.5f) { specific = true, specificId = "browncap" },
                    new RecipeItem(0.5f) { specific = true, specificId = "browncap" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 0.1f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false },
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 11,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "bananamash",
                    amount = 1,
                    isLiquid = true,
                    resultCondition = 400f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "banana" },
                    new RecipeItem(0.5f) { specific = true, specificId = "banana" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 0.1f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false },
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 11,
                category = Recipes.RecipeCategory.Utilities,
                result = new RecipeResult
                {
                    id = "microwave",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 0f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.9f) { specific = true, specificId = "circuitboard" },
                    new RecipeItem(0f) { specific = true, specificId = "scrappanel" },
                    new RecipeItem(0f) { specific = true, specificId = "scrappanel" },
                    new RecipeItem(0f) { specific = true, specificId = "scrappanel" },
                    new RecipeItem(0f) { specific = true, specificId = "scrappanel" },
                    new RecipeItem(0f) { specific = true, specificId = "bundleofwires" },
                    new RecipeItem(0f) { specific = true, specificId = "processedcopper" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 3f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("cutting", 3f), destroyItem = false },
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 8,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "hotsauce",
                    amount = 1,
                    isLiquid = true,
                    resultCondition = 200f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "pepper" },
                    new RecipeItem(0.5f) { specific = true, specificId = "pepper" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 0.1f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 6,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "ketchup",
                    amount = 1,
                    isLiquid = true,
                    resultCondition = 200f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "tomato" },
                    new RecipeItem(0.5f) { specific = true, specificId = "tomato" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 0.1f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 9,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "icecream",
                    amount = 1,
                    isLiquid = true,
                    resultCondition = 500f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.9f) { specific = true, specificId = "icepack", destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("water", 500), destroyItem = false },
                    new RecipeItem(500f) { specific = true, specificId = "milk", isLiquid = true ,destroyItem = false},
                }
            });
            //RecipeRegistry.Register(new Recipe
            //{
            //    INT = 0,
            //    category = Recipes.RecipeCategory.Food,
            //    items = new List<RecipeItem>
            //    {
            //        new RecipeItem(0.5f) { quality = new CraftingQuality("sliceable") },
            //        new RecipeItem(0f) { quality = new CraftingQuality("cutting", 1f) },
            //    },
            //    result = new RecipeResult
            //    {
            //        id = new RecipeItem(0.5f) { quality = new CraftingQuality("sliceable") }.ToString() + "slice",
            //        amount = 4,
            //        isLiquid = false,
            //        resultCondition = 1f
            //    }
            //});
        }
        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(Recipe), "TryMake")]
        //public static void TryMakePatch()
        //{
        //    List<Item> itemsToSlice = GetItemsForRecipe()
        //}

        public sealed class OvenScript : MonoBehaviour
        {
            public CraftingQuality quality = new CraftingQuality("heatsource", 40);
        }
    }
}
