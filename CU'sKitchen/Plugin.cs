using BepInEx;
using BepInEx.Logging;
using CUCoreLib.ContentReload;
using CUCoreLib.Data;
// Used for CUCoreLib features. Remove if you don't use any of said features :)
using CUCoreLib.Helpers;
using CUCoreLib.Registries;
using CUCoreLib.Saving;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public partial class Plugin : BaseUnityPlugin
    {
        public const string ModGUID = "com.MrMaker.CUsKitchen";
        public const string ModName = "Casualties Unknown's Kitchen";
        public const string ModVersion = "1.2.0";

        internal static new ManualLogSource Logger;
        private readonly Harmony _harmony = new(ModGUID);
        public static Plugin Instance { get; private set; } = null!;

        public void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            _harmony.PatchAll();
            Logger.LogInfo($"Plugin {ModName} is loaded!");

            ContentReloadManager.EnableHotReload(ModGUID);
            CustomItems();
            KitchenCookingSystem.RegisterCookwareItems();
            CustomLiquids();
            CustomBuildings();
            CustomRecipes();
            KitchenCookingSystem.Initialize();
        }

        //ItemInfo closestItem = new RecipeItem(0.5f) { quality = new CraftingQuality("") }

        public void CustomBuildings()
        {
            BuildingEntityRegistry.Register("appletree", new CustomBuildingEntityDefinition
            {
                Name = "Apple Tree",
                Description = "A tall tree that seems to bear some sort of red fruit",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.AppleTree.png"),
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
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.PepperPlant.png"),
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
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.TomatoPlant.png"),
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
            //    Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.Oven.png"),
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
                    id = "breadcrumbs",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.2f) { specific = true, specificId = "bread" },
                    new RecipeItem(0f) { quality = new CraftingQuality("cutting", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 5,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "candiedgeofruit",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "geofruit" },
                    new RecipeItem(100f) { specific = true, specificId = "sap", isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 5,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "fruitglaze",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "geofruit" },
                    new RecipeItem(100f) { specific = true, specificId = "applejuice", isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 6,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "frieddough",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0f) { quality = new CraftingQuality("flour", 1f) },
                    new RecipeItem(0f) { quality = new CraftingQuality("water", 100f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("fat", 25f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 6,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "friedmeat",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.2f) { quality = new CraftingQuality("meat") },
                    new RecipeItem(0f) { quality = new CraftingQuality("fat", 25f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 6,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "searedmushpear",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "mushpear" },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 6,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "searedmushtail",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "mushtail" },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 6,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "rosetea",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "rosepod" },
                    new RecipeItem(0f) { quality = new CraftingQuality("water", 150f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.25f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 6,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "pilk",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(250f) { specific = true, specificId = "milk", isLiquid = true, destroyItem = false },
                    new RecipeItem(125f) { specific = true, specificId = "sodacan", isLiquid = true, destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 7,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "vegetablebroth",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "tomato" },
                    new RecipeItem(0.5f) { specific = true, specificId = "corn" },
                    new RecipeItem(0f) { quality = new CraftingQuality("water", 200f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 7,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "wastelandsalad",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "popfruit" },
                    new RecipeItem(0.5f) { specific = true, specificId = "stonefruitopen" },
                    new RecipeItem(0.5f) { specific = true, specificId = "helluce" },
                    new RecipeItem(0f) { quality = new CraftingQuality("cutting", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 7,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "meatstock",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.2f) { quality = new CraftingQuality("meat") },
                    new RecipeItem(0f) { quality = new CraftingQuality("water", 200f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 7,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "organbroth",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "internalorgans" },
                    new RecipeItem(0f) { quality = new CraftingQuality("water", 200f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 7,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "lumalgaebroth",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "glowplantfruit" },
                    new RecipeItem(0f) { quality = new CraftingQuality("lumalgae", 200f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 7,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "fleshwrappedcactus",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "cactusflesh" },
                    new RecipeItem(0.2f) { quality = new CraftingQuality("meat") },
                    new RecipeItem(0f) { quality = new CraftingQuality("cutting", 0.5f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.25f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 8,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "blobfleshscone",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "blobflesh" },
                    new RecipeItem(0f) { quality = new CraftingQuality("flour", 1f) },
                    new RecipeItem(0f) { quality = new CraftingQuality("water", 100f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 8,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "bloodnoodles",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0f) { quality = new CraftingQuality("flour", 1f) },
                    new RecipeItem(0f) { quality = new CraftingQuality("blood", 50f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0.2f) { quality = new CraftingQuality("meat") },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("cutting", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 8,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "cremeofmushroomsoup",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.5f) { specific = true, specificId = "browncap" },
                    new RecipeItem(0.5f) { specific = true, specificId = "mushpear" },
                    new RecipeItem(150f) { specific = true, specificId = "milk", isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("water", 100f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 8,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "sauteedmeat",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.2f) { quality = new CraftingQuality("meat") },
                    new RecipeItem(0.5f) { specific = true, specificId = "garlic" },
                    new RecipeItem(0f) { quality = new CraftingQuality("fat", 25f), isLiquid = true, destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 8,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "pileofjunkfood",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.2f) { specific = true, specificId = "cookies" },
                    new RecipeItem(0.2f) { specific = true, specificId = "chips" },
                    new RecipeItem(0.2f) { specific = true, specificId = "candybar" }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 9,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "shepardspie",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.2f) { quality = new CraftingQuality("meat") },
                    new RecipeItem(0.5f) { specific = true, specificId = "turnip" },
                    new RecipeItem(0.5f) { specific = true, specificId = "squash" },
                    new RecipeItem(0f) { quality = new CraftingQuality("flour", 1f) },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.75f), destroyItem = false }
                },
            });
            RecipeRegistry.Register(new Recipe
            {
                INT = 9,
                category = Recipes.RecipeCategory.Food,
                result = new RecipeResult
                {
                    id = "stuffedgeigefruitbun",
                    amount = 1,
                    isLiquid = false,
                    resultCondition = 1f
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.2f) { specific = true, specificId = "bread" },
                    new RecipeItem(0.5f) { specific = true, specificId = "popfruit" },
                    new RecipeItem(0.5f) { specific = true, specificId = "fruitglaze" },
                    new RecipeItem(0f) { quality = new CraftingQuality("heatsource", 0.5f), destroyItem = false }
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

            // Custom Cookery 

            RecipeRegistry.Register(new Recipe
            {
                INT = 9,
                category = Recipes.RecipeCategory.Utilities,
                result = new RecipeResult
                {
                    id = "skillet",
                    amount = 1,
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.9f) { specific = true, specificId = "scrappanel" },
                    new RecipeItem(0.5f) { specific = true, specificId = "scrappanel" },
                    new RecipeItem(0.9f) { specific = true, specificId = "stick" },
                    new RecipeItem(0.9f) { specific = true, specificId = "nails" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 5f), destroyItem = false },
                    new RecipeItem(20f) { specific = true, specificId = "biochem", isLiquid = true ,destroyItem = false},
                }
            });

            RecipeRegistry.Register(new Recipe
            {
                INT = 10,
                category = Recipes.RecipeCategory.Utilities,
                result = new RecipeResult
                {
                    id = "portableoven",
                    amount = 1,
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0f) { specific = true, specificId = "lightbulb" },
                    new RecipeItem(0f) { specific = true, specificId = "processedcopper" },
                    new RecipeItem(0f) { specific = true, specificId = "flexiglass" },
                    new RecipeItem(0f) { specific = true, specificId = "circuitboard" },
                    new RecipeItem(0.5f) { specific = true, specificId = "scrappanel" },
                    new RecipeItem(0.5f) { specific = true, specificId = "scrappanel" },
                    new RecipeItem(0f) { quality = new CraftingQuality("hammering", 5f), destroyItem = false },
                    new RecipeItem(0f) { quality = new CraftingQuality("cutting", 5f), destroyItem = false },
                    new RecipeItem(20f) { specific = true, specificId = "biochem", isLiquid = true ,destroyItem = false},
                }
            });


            RecipeRegistry.Register(new Recipe
            {
                INT = 9,
                category = Recipes.RecipeCategory.Utilities,
                result = new RecipeResult
                {
                    id = "pot",
                    amount = 1,
                },
                items = new List<RecipeItem>
                {
                    new RecipeItem(0.9f) { specific = true, specificId = "scraptube" },
                    new RecipeItem(0.5f) { specific = true, specificId = "woodpanel" },
                    new RecipeItem(0.9f) { specific = true, specificId = "plasticchunk" },
                    new RecipeItem() { specific = true, specificId = "rope" },
                    new RecipeItem(0.5f) { specific = true, specificId = "rawcopper" },
                    new RecipeItem(0f) { quality = new CraftingQuality("cutting", 5f), destroyItem = false },
                    new RecipeItem(20f) { specific = true, specificId = "biochem", isLiquid = true ,destroyItem = false},
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
