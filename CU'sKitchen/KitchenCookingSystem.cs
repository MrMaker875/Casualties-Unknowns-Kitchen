using CUCoreLib.Data;
using CUCoreLib.Helpers;
using CUCoreLib.Registries;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CU_sKitchen
{
    internal static class KitchenCookingSystem
    {
        internal enum CookingStationType
        {
            Skillet,
            Pot,
            PortableOven
        }

        private const string IngredientKey = "kitchen.ingredients";
        private const string FlavorKey = "kitchen.flavors";
        private const string VisualKey = "kitchen.visuals";
        private const string StationKey = "kitchen.stations";
        private const string MethodKey = "kitchen.methods";
        private const string DishKey = "kitchen.dish";

        private const string IngredientTagPrefix = "kitchen-ingredient-";
        private const string FlavorTagPrefix = "kitchen-flavor-";
        private const string VisualTagPrefix = "kitchen-visual-";

        // How this works:

        /* So, each finished item has a series of tags. For instance, some yummy blood noodles would get the following tags:
         * "red", "blood", "umami", "salty", "iron", "yellow", "white", "carbs", "meat"
         * These tags are categorized into 4 groups: Ingredient, Flavor, Visual, and Method. See below.
         * Each group has a score associated with it. So the ingredient tag is prioritized over the method (cookware) tag > flavor > visual
         * 
         * When you cook something, the system will compare the tags of the ingredients you used to the tags of the finished item. 
         * If it matches, add to the total score. 
         * It matches every dish based on this, and will produce the highest score dish 70% of the time, and a random dish from 1-4 score lower 30% of the time.
         * 
         * I love dynamic cooking !!!
         */
        private const int IngredientTagScore = 6;
        private const int FlavorTagScore = 3;
        private const int VisualTagScore = 2;
        private const int MethodTagScore = 4;
        private const int LowerScoreWindow = 3;
        private const float BestDishChance = 0.7f;
        private const float MinimumLiquidCookAmount = 25f;

        // TODO make this dynamic based on existance of onUse delegate
        private static readonly HashSet<string> NonFoodCustomIds = new(StringComparer.OrdinalIgnoreCase)
        {
            "appletree",
            "bamboo",
            "microwave",
            "oven",
            "skillet",
            "cookingpot",
            "portableoven",
            "pepperplant",
            "ricestalk",
            "tomatoplant"
        };

        private static readonly Dictionary<string, CookingTagProfile> LiquidCookingProfiles =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["water"] = new CookingTagProfile(new[] { "water" }, new[] { "clean" }),
                ["milk"] = new CookingTagProfile(new[] { "dairy" }, new[] { "creamy" }, new[] { "white" }),
                ["redblood"] = new CookingTagProfile(new[] { "blood" }, new[] { "umami", "salty", "iron" }, new[] { "red" }), 
                ["blood"] = new CookingTagProfile(new[] { "blood" }, new[] { "umami", "salty", "iron" }, new[] { "red" }),
                ["fat"] = new CookingTagProfile(new[] { "fat" }, new[] { "rich", "savory" }, new[] { "yellow", "white" }),
                ["applejuice"] = new CookingTagProfile(new[] { "fruit" }, new[] { "sweet", "acidic" }, new[] { "yellow", "green" }), 
                ["sap"] = new CookingTagProfile(new[] { "sweetener" }, new[] { "sweet" }, new[] { "yellow", "orange", "brown" }),
            };

        // TODO divide by total tags so weightings are equal within the category
        private static readonly Dictionary<string, DishSeed> DishSeeds =
            new(StringComparer.OrdinalIgnoreCase) // This is for ONLY vanilla items, go edit the customData field for custom items instead :evil:
            // I guess it works for modded items, but please do ^
            {
                ["applepie"] = CreateDish(new[] { CookingStationType.PortableOven }, new[] { "carbs", "fruit" }, new[] { "sweet", "acidic", "buttery" }, new[] { "red", "yellow", "brown" }),
                ["bloodnoodles"] = CreateDish(new[] { CookingStationType.Pot }, new[] { "carbs", "blood", "meat" }, new[] { "umami", "salty", "acidic" }, new[] { "yellow", "red", "white" }),
                ["vegetablebroth"] = CreateDish(new[] { CookingStationType.Pot }, new[] { "produce", "water" }, new[] { "fresh", "savory", "acidic" }, new[] { "red", "yellow", "green" }),
                ["wastelandsalad"] = CreateDish(new[] { CookingStationType.Skillet, CookingStationType.Pot }, new[] { "produce", "tomato" }, new[] { "fresh", "sweet", "acidic" }, new[] { "red", "yellow", "green" }),
                ["meatstock"] = CreateDish(new[] { CookingStationType.Pot }, new[] { "meat", "water" }, new[] { "umami", "salty", "rich" }, new[] { "brown", "red" }),
                ["organbroth"] = CreateDish(new[] { CookingStationType.Pot }, new[] { "meat", "blood", "water" }, new[] { "iron", "umami", "salty" }, new[] { "red", "brown" }),
                ["lumalgaebroth"] = CreateDish(new[] { CookingStationType.Pot }, new[] { "produce", "water" }, new[] { "earthy", "savory", "sweet" }, new[] { "green", "yellow" }),
                ["fleshwrappedcactus"] = CreateDish(new[] { CookingStationType.Skillet }, new[] { "meat", "produce" }, new[] { "umami", "fresh", "acidic" }, new[] { "green", "red", "brown" }),
                ["blobfleshscone"] = CreateDish(new[] { CookingStationType.PortableOven }, new[] { "meat", "carbs", "water" }, new[] { "savory", "earthy", "umami" }, new[] { "brown", "white", "red" }),
                ["cremeofmushroomsoup"] = CreateDish(new[] { CookingStationType.Pot }, new[] { "mushroom", "dairy", "water" }, new[] { "creamy", "earthy", "umami" }, new[] { "white", "brown" }),
                ["sauteedmeat"] = CreateDish(new[] { CookingStationType.Skillet }, new[] { "meat", "fat", "garlic" }, new[] { "umami", "rich", "pungent" }, new[] { "brown", "yellow", "red" }),
                ["shepardspie"] = CreateDish(new[] { CookingStationType.PortableOven }, new[] { "meat", "carbs", "produce" }, new[] { "savory", "sweet", "umami" }, new[] { "brown", "yellow", "orange" }),
                ["stuffedgeigefruitbun"] = CreateDish(new[] { CookingStationType.PortableOven }, new[] { "carbs", "fruit", "sweetener" }, new[] { "sweet", "acidic" }, new[] { "yellow", "orange", "white" }),
                ["frieddough"] = CreateDish(new[] { CookingStationType.Skillet, CookingStationType.PortableOven }, new[] { "carbs", "fat", "water" }, new[] { "rich", "savory" }, new[] { "yellow", "brown" }),
                ["friedmeat"] = CreateDish(new[] { CookingStationType.Skillet }, new[] { "meat", "fat" }, new[] { "umami", "salty", "rich" }, new[] { "brown", "red" }),
                ["rosetea"] = CreateDish(new[] { CookingStationType.Pot }, new[] { "flower", "water" }, new[] { "floral", "sweet" }, new[] { "red", "pink" }),
                ["pilk"] = CreateDish(new[] { CookingStationType.Pot }, new[] { "dairy" }, new[] { "creamy", "sweet" }, new[] { "white", "yellow" }),
                ["candiedgeofruit"] = CreateDish(new[] { CookingStationType.PortableOven }, new[] { "fruit", "sweetener" }, new[] { "sweet" }, new[] { "yellow", "orange" }),
                ["fruitglaze"] = CreateDish(new[] { CookingStationType.Pot, CookingStationType.PortableOven }, new[] { "fruit" }, new[] { "sweet", "acidic" }, new[] { "yellow", "orange" }),
                ["searedmushpear"] = CreateDish(new[] { CookingStationType.Skillet }, new[] { "mushroom" }, new[] { "earthy", "umami" }, new[] { "brown", "white" }),
                ["searedmushtail"] = CreateDish(new[] { CookingStationType.Skillet }, new[] { "mushroom" }, new[] { "earthy", "umami" }, new[] { "brown", "white" }),
                ["pileofjunkfood"] = CreateDish(new[] { CookingStationType.Skillet, CookingStationType.Pot, CookingStationType.PortableOven }, new[] { "carbs", "sweetener", "fat" }, new[] { "sweet", "salty", "rich" }, new[] { "brown", "yellow", "red" }),
                ["bread"] = CreateDish(new[] { CookingStationType.PortableOven }, new[] { "carbs", "water" }, new[] { "yeasty", "savory" }, new[] { "brown", "white" }),
                ["pancake"] = CreateDish(new[] { CookingStationType.Skillet }, new[] { "carbs", "water", "sweetener" }, new[] { "sweet", "rich" }, new[] { "yellow", "brown" }),
                ["nutrientbar"] = CreateDish(new[] { CookingStationType.PortableOven }, new[] { "carbs", "produce", "fat" }, new[] { "sweet", "earthy", "rich" }, new[] { "brown", "yellow", "green" }),
                ["pemmican"] = CreateDish(new[] { CookingStationType.PortableOven }, new[] { "meat", "produce", "fat" }, new[] { "umami", "rich", "savory" }, new[] { "brown", "red" }),
                ["foliagemeal"] = CreateDish(new[] { CookingStationType.Pot }, new[] { "produce" }, new[] { "earthy", "fresh" }, new[] { "green", "brown" }),
                ["burger"] = CreateDish(new[] { CookingStationType.Skillet }, new[] { "meat", "carbs" }, new[] { "umami", "rich", "savory" }, new[] { "brown", "red", "white" }),
                ["soup"] = CreateDish(new[] { CookingStationType.Pot }, new[] { "meat", "water", "fat" }, new[] { "umami", "rich", "savory" }, new[] { "brown", "yellow" }),
            };

        private static readonly Dictionary<string, CookingTagProfile> ItemProfileSeeds =
            new(StringComparer.OrdinalIgnoreCase) // !!! THIS IS TEMPORARY !!!
            // Whilst CUCoreLib doesn't have the newest CustomData system for non-nightly builds, this is a temporary solution to seed the cooking tags for vanilla items.
            // This will be removed once CUCoreLib is updated to support CustomData for non-nightly builds.
            {
                ["acorn"] = new CookingTagProfile(new[] { "nut", "seed" }, new[] { "earthy", "bitter" }, new[] { "brown" }),
                ["almond"] = new CookingTagProfile(new[] { "nut", "seed" }, new[] { "earthy", "sweet" }, new[] { "brown", "white" }),
                ["apple"] = new CookingTagProfile(new[] { "fruit", "orchard" }, new[] { "sweet", "acidic" }, new[] { "red", "white" }),
                ["avocado"] = new CookingTagProfile(new[] { "fruit", "vegetable", "fatty" }, new[] { "creamy", "earthy" }, new[] { "green", "yellow" }),
                ["beansprout"] = new CookingTagProfile(new[] { "vegetable", "sprout" }, new[] { "fresh" }, new[] { "white", "yellow" }),
                ["bellpepper"] = new CookingTagProfile(new[] { "vegetable", "pepperfamily" }, new[] { "sweet", "fresh" }, new[] { "red", "green", "yellow" }),
                ["blueberry"] = new CookingTagProfile(new[] { "fruit", "berry" }, new[] { "sweet", "acidic" }, new[] { "blue", "purple" }),
                ["broccoli"] = new CookingTagProfile(new[] { "vegetable", "brassica" }, new[] { "earthy", "fresh" }, new[] { "green" }),
                ["brusselsprout"] = new CookingTagProfile(new[] { "vegetable", "brassica" }, new[] { "earthy", "bitter" }, new[] { "green" }),
                ["cherry"] = new CookingTagProfile(new[] { "fruit", "berry" }, new[] { "sweet", "acidic" }, new[] { "red" }),
                ["coralcreep"] = new CookingTagProfile(new[] { "vegetable", "coastal" }, new[] { "salty", "umami" }, new[] { "red", "orange" }),
                ["corn"] = new CookingTagProfile(new[] { "vegetable", "grain" }, new[] { "sweet", "starchy" }, new[] { "yellow" }),
                ["dragonfruit"] = new CookingTagProfile(new[] { "fruit", "tropical" }, new[] { "sweet", "acidic" }, new[] { "pink", "green" }),
                ["eggplant"] = new CookingTagProfile(new[] { "vegetable", "nightshade" }, new[] { "earthy" }, new[] { "purple", "white" }),
                ["garlic"] = new CookingTagProfile(new[] { "vegetable", "allium" }, new[] { "pungent", "umami" }, new[] { "white" }),
                ["grapefruit"] = new CookingTagProfile(new[] { "fruit", "citrus" }, new[] { "acidic", "bitter" }, new[] { "yellow", "pink" }),
                ["greenapple"] = new CookingTagProfile(new[] { "fruit", "orchard" }, new[] { "acidic", "sweet" }, new[] { "green", "white" }),
                ["greengrape"] = new CookingTagProfile(new[] { "fruit", "berry" }, new[] { "sweet", "acidic" }, new[] { "green" }),
                ["hazelnut"] = new CookingTagProfile(new[] { "nut", "seed" }, new[] { "earthy", "sweet" }, new[] { "brown" }),
                ["honiedwatermelonslice"] = new CookingTagProfile(new[] { "fruit", "melon" }, new[] { "sweet", "fresh" }, new[] { "red", "green" }),
                ["mango"] = new CookingTagProfile(new[] { "fruit", "tropical" }, new[] { "sweet", "acidic" }, new[] { "yellow", "orange" }),
                ["orange"] = new CookingTagProfile(new[] { "fruit", "citrus" }, new[] { "sweet", "acidic" }, new[] { "orange" }),
                ["passionfruit"] = new CookingTagProfile(new[] { "fruit", "tropical" }, new[] { "sweet", "acidic" }, new[] { "yellow", "purple" }),
                ["peach"] = new CookingTagProfile(new[] { "fruit", "orchard" }, new[] { "sweet" }, new[] { "orange", "yellow" }),
                ["peanut"] = new CookingTagProfile(new[] { "nut", "seed" }, new[] { "earthy", "salty" }, new[] { "brown", "white" }),
                ["pepper"] = new CookingTagProfile(new[] { "fruit", "pepperfamily" }, new[] { "spicy", "acidic" }, new[] { "red" }),
                ["pineapple"] = new CookingTagProfile(new[] { "fruit", "tropical" }, new[] { "sweet", "acidic" }, new[] { "yellow" }),
                ["plum"] = new CookingTagProfile(new[] { "fruit", "stonefruit" }, new[] { "sweet", "acidic" }, new[] { "purple", "red" }),
                ["pumpkin"] = new CookingTagProfile(new[] { "vegetable", "gourd" }, new[] { "sweet", "earthy" }, new[] { "orange", "yellow" }),
                ["purplegrape"] = new CookingTagProfile(new[] { "fruit", "berry" }, new[] { "sweet", "acidic" }, new[] { "purple" }),
                ["radish"] = new CookingTagProfile(new[] { "vegetable", "root" }, new[] { "peppery", "fresh" }, new[] { "red", "white" }),
                ["rambutan"] = new CookingTagProfile(new[] { "fruit", "tropical" }, new[] { "sweet", "acidic" }, new[] { "red", "white" }),
                ["rice"] = new CookingTagProfile(new[] { "vegetable", "grain", "cereal" }, new[] { "starchy" }, new[] { "white" }),
                ["salmonberry"] = new CookingTagProfile(new[] { "fruit", "berry" }, new[] { "sweet", "acidic" }, new[] { "red", "pink" }),
                ["squash"] = new CookingTagProfile(new[] { "vegetable", "gourd" }, new[] { "sweet", "earthy" }, new[] { "yellow", "orange" }),
                ["strawberry"] = new CookingTagProfile(new[] { "fruit", "berry" }, new[] { "sweet", "acidic" }, new[] { "red" }),
                ["tomato"] = new CookingTagProfile(new[] { "fruit", "nightshade", "tomato" }, new[] { "acidic", "sweet" }, new[] { "red" }),
                ["turnip"] = new CookingTagProfile(new[] { "vegetable", "root" }, new[] { "earthy", "sweet" }, new[] { "white", "purple" }),
                ["watermelon"] = new CookingTagProfile(new[] { "fruit", "melon" }, new[] { "sweet", "fresh" }, new[] { "red", "green" }),
                ["watermelonslice"] = new CookingTagProfile(new[] { "fruit", "melon" }, new[] { "sweet", "fresh" }, new[] { "red", "green" }),
                ["wheat"] = new CookingTagProfile(new[] { "vegetable", "grain", "cereal" }, new[] { "starchy" }, new[] { "yellow" }),
                ["bread"] = new CookingTagProfile(new[] { "carbs", "grain" }, new[] { "yeasty", "savory" }, new[] { "brown", "white" }),
                ["cookies"] = new CookingTagProfile(new[] { "carbs", "sweetener" }, new[] { "sweet" }, new[] { "brown" }),
                ["chips"] = new CookingTagProfile(new[] { "carbs", "fat" }, new[] { "salty" }, new[] { "yellow", "brown" }),
                ["candybar"] = new CookingTagProfile(new[] { "sweetener", "fat" }, new[] { "sweet" }, new[] { "brown" }),
                ["geofruit"] = new CookingTagProfile(new[] { "fruit" }, new[] { "sweet", "acidic" }, new[] { "yellow", "orange" }),
                ["popfruit"] = new CookingTagProfile(new[] { "fruit" }, new[] { "sweet", "acidic" }, new[] { "purple", "yellow" }),
                ["rosepod"] = new CookingTagProfile(new[] { "flower" }, new[] { "floral" }, new[] { "red", "pink" }),
                ["browncap"] = new CookingTagProfile(new[] { "mushroom" }, new[] { "earthy", "umami" }, new[] { "brown", "white" }),
                ["mushpear"] = new CookingTagProfile(new[] { "mushroom" }, new[] { "earthy", "umami" }, new[] { "brown", "white" }),
                ["mushtail"] = new CookingTagProfile(new[] { "mushroom" }, new[] { "earthy", "umami" }, new[] { "brown", "white" }),
                ["cactusflesh"] = new CookingTagProfile(new[] { "vegetable" }, new[] { "fresh", "acidic" }, new[] { "green", "white" }),
                ["blobflesh"] = new CookingTagProfile(new[] { "meat", "fat" }, new[] { "umami", "salty" }, new[] { "red", "pink" }),
                ["internalorgans"] = new CookingTagProfile(new[] { "meat", "blood" }, new[] { "iron", "umami" }, new[] { "red", "brown" }),
                ["glowplantfruit"] = new CookingTagProfile(new[] { "vegetable" }, new[] { "earthy", "sweet" }, new[] { "green", "yellow" }),
                ["banana"] = new CookingTagProfile(new[] { "fruit", "tropical" }, new[] { "sweet" }, new[] { "yellow" }),
                ["stonefruitopen"] = new CookingTagProfile(new[] { "fruit", "stonefruit" }, new[] { "sweet", "acidic" }, new[] { "orange", "yellow" }),
                ["ryebulb"] = new CookingTagProfile(new[] { "vegetable", "root", "grain" }, new[] { "earthy" }, new[] { "yellow", "white" }),
                ["animalflesh"] = new CookingTagProfile(new[] { "meat" }, new[] { "umami", "salty" }, new[] { "red", "brown" }),
                ["pancake"] = new CookingTagProfile(new[] { "carbs", "sweetener" }, new[] { "sweet", "rich" }, new[] { "yellow", "brown" }),
                ["nutrientbar"] = new CookingTagProfile(new[] { "carbs", "produce", "fat" }, new[] { "sweet", "earthy", "rich" }, new[] { "brown", "yellow", "green" }),
                ["pemmican"] = new CookingTagProfile(new[] { "meat", "produce", "fat" }, new[] { "umami", "rich", "savory" }, new[] { "brown", "red" }),
                ["foliagemeal"] = new CookingTagProfile(new[] { "produce" }, new[] { "earthy", "fresh" }, new[] { "green", "brown" }),
                ["burger"] = new CookingTagProfile(new[] { "meat", "carbs" }, new[] { "umami", "rich", "savory" }, new[] { "brown", "red", "white" }),
                ["soup"] = new CookingTagProfile(new[] { "meat", "water", "fat" }, new[] { "umami", "rich", "savory" }, new[] { "brown", "yellow" }),
            };

        private static readonly string[] VanillaProfileIds =
        {
            "bread",
            "cookies",
            "chips",
            "candybar",
            "geofruit",
            "popfruit",
            "rosepod",
            "browncap",
            "mushpear",
            "mushtail",
            "cactusflesh",
            "glowplantfruit",
            "banana",
            "stonefruitopen",
            "ryebulb",
            "animalflesh",
            "pancake",
            "nutrientbar",
            "pemmican",
            "foliagemeal",
            "burger",
            "soup"
        };

        private static bool _vanillaTagsApplied;

        internal static void Initialize()
        {
            _vanillaTagsApplied = false;
            SeedCustomItemCookingMetadata();
            EnsureVanillaItemTagsApplied();
        }

        internal static void RegisterCookwareItems()
        {
            ItemRegistry.Register("skillet", new CustomItemInfo
            {
                fullName = "Skillet",
                description = "A shallow pan, for frying and sautéing ingredients. <br><color=orange>Drag 3 or more ingredients and use to cook food.</color>",
                category = "container",
                usable = true,
                usableOnLimb = false,
                destroyAtZeroCondition = false,
                combineable = true,
                weight = 1.8f,
                scaleWeightWithCondition = true,
                value = 26,
                rec = new Recognition(6),
                capacity = 150f,
                autoFill = false,
                Container = new ContainerProperties
                {
                    Capacity = 4.5f,
                    MaxWeightPerItem = 1.6f,
                    EncumbranceReduction = 0.85f,
                    ItemsVisible = true
                },
                useAction = (body, item) => CookWithContainer(body, item, CookingStationType.Skillet),
                qualities = new List<CraftingQuality>
                {
                    new CraftingQuality("heatsource", 18f)
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Skillet.png"));

            ItemRegistry.Register("cookingpot", new CustomItemInfo
            {
                fullName = "Cooking Pot",
                description = "A deep pot for soups, noodles, and anything else that benefits from simmering together. <br><color=orange>Drag 3 or more ingredients alongside 50ml water and use to cook food.</color>",
                category = "container",
                usable = true,
                usableOnLimb = false,
                destroyAtZeroCondition = false,
                combineable = true,
                weight = 2.4f,
                scaleWeightWithCondition = true,
                value = 34,
                rec = new Recognition(7),
                capacity = 700f,
                autoFill = false,
                Container = new ContainerProperties
                {
                    Capacity = 7f,
                    MaxWeightPerItem = 2.4f,
                    EncumbranceReduction = 0.8f,
                    ItemsVisible = true
                },
                useAction = (body, item) => CookWithContainer(body, item, CookingStationType.Pot),
                qualities = new List<CraftingQuality>
                {
                    new CraftingQuality("heatsource", 20f)
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Pot.png"));

            ItemRegistry.Register("portableoven", new CustomItemInfo
            {
                fullName = "Portable Oven",
                description = "A compact field oven that bakes whatever its contents most strongly suggest. <br><color=orange>Drag 3 or more ingredients and use to cook food.</color>",
                category = "container",
                usable = true,
                usableOnLimb = false,
                destroyAtZeroCondition = false,
                combineable = true,
                weight = 4.5f,
                scaleWeightWithCondition = true,
                value = 45,
                rec = new Recognition(9),
                capacity = 250f,
                autoFill = false,
                Container = new ContainerProperties
                {
                    Capacity = 6f,
                    MaxWeightPerItem = 2.8f,
                    EncumbranceReduction = 0.75f,
                    ItemsVisible = false
                },
                useAction = (body, item) => CookWithContainer(body, item, CookingStationType.PortableOven),
                qualities = new List<CraftingQuality>
                {
                    new CraftingQuality("heatsource", 30f)
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.PortableOven.png"));
        }

        private static DishSeed CreateDish(
            CookingStationType[] stations,
            string[] ingredientTags = null,
            string[] flavorTags = null,
            string[] visualTags = null,
            int amount = 1,
            float resultCondition = 1f)
        {
            return new DishSeed
            {
                Stations = stations ?? Array.Empty<CookingStationType>(),
                Profile = new CookingTagProfile(ingredientTags, flavorTags, visualTags),
                MethodTags = (stations ?? Array.Empty<CookingStationType>()).Select(ToMethodTag).Distinct(StringComparer.OrdinalIgnoreCase).ToArray(),
                Amount = amount,
                ResultCondition = resultCondition
            };
        }

        private static void SeedCustomItemCookingMetadata()
        {
            foreach (KeyValuePair<string, CookingTagProfile> entry in ItemProfileSeeds)
            {
                string id = entry.Key;
                if (!ItemRegistry.TryGetCustomInfo(id, out CustomItemInfo info) || info == null)
                    continue;

                if (NonFoodCustomIds.Contains(id))
                    continue;

                CookingTagProfile profile = entry.Value;
                DishSeeds.TryGetValue(id, out DishSeed dishSeed);

                if (profile == null || !profile.HasAnyTags)
                    continue;

                info.CustomData ??= new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                info.CustomData[IngredientKey] = string.Join(",", profile.IngredientTags);
                if (profile.FlavorTags.Count > 0) info.CustomData[FlavorKey] = string.Join(",", profile.FlavorTags);
                if (profile.VisualTags.Count > 0) info.CustomData[VisualKey] = string.Join(",", profile.VisualTags);

                if (dishSeed != null && dishSeed.Stations.Length > 0)
                {
                    info.CustomData[DishKey] = true;
                    info.CustomData[StationKey] = string.Join(",", dishSeed.Stations.Select(ToStationToken));
                    info.CustomData[MethodKey] = string.Join(",", dishSeed.MethodTags);
                    info.tags = AppendPlainTags(info.tags, dishSeed.MethodTags);
                }

                ItemRegistry.Register(id, info);
            }
        }

        // Awful code, guh
        private static void EnsureVanillaItemTagsApplied()
        {
            if (_vanillaTagsApplied || Item.GlobalItems == null)
                return;

            foreach (string id in VanillaProfileIds)
            {
                if (!Item.GlobalItems.TryGetValue(id, out ItemInfo info) || info == null)
                    continue;

                if (!ItemProfileSeeds.TryGetValue(id, out CookingTagProfile profile))
                    continue;
                DishSeeds.TryGetValue(id, out DishSeed dishSeed);
                if (profile == null || !profile.HasAnyTags)
                    continue;

                List<string> tags = new();
                if (!string.IsNullOrWhiteSpace(info.tags))
                    tags.AddRange(info.tags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(tag => tag.Trim()));

                AppendTagTokens(tags, IngredientTagPrefix, profile.IngredientTags);
                AppendTagTokens(tags, FlavorTagPrefix, profile.FlavorTags);
                AppendTagTokens(tags, VisualTagPrefix, profile.VisualTags);
                if (dishSeed != null) AppendPlainTags(tags, dishSeed.MethodTags);

                info.tags = string.Join(",", tags.Distinct(StringComparer.OrdinalIgnoreCase));
                info.SetTags();
            }

            _vanillaTagsApplied = true;
        }

        private static void AppendTagTokens(List<string> tags, string prefix, IEnumerable<string> values)
        {
            if (tags == null || values == null) return;

            foreach (string value in values)
            {
                if (string.IsNullOrWhiteSpace(value)) continue;
                string token = prefix + value.Trim().ToLowerInvariant();
                if (!tags.Contains(token, StringComparer.OrdinalIgnoreCase))
                    tags.Add(token);
            }
        }

        private static string AppendPlainTags(string rawTags, IEnumerable<string> plainTags)
        {
            List<string> tags = new();
            if (!string.IsNullOrWhiteSpace(rawTags))
                tags.AddRange(rawTags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(tag => tag.Trim()));

            AppendPlainTags(tags, plainTags);
            return string.Join(",", tags.Distinct(StringComparer.OrdinalIgnoreCase));
        }

        private static void AppendPlainTags(List<string> tags, IEnumerable<string> plainTags)
        {
            if (tags == null || plainTags == null) return;

            foreach (string plainTag in plainTags)
            {
                if (string.IsNullOrWhiteSpace(plainTag)) continue;
                if (!tags.Contains(plainTag, StringComparer.OrdinalIgnoreCase))
                    tags.Add(plainTag);
            }
        }

        private static string ToStationToken(CookingStationType station)
        {
            return station switch
            {
                CookingStationType.Skillet => "skillet",
                CookingStationType.Pot => "pot",
                CookingStationType.PortableOven => "portableoven",
                _ => string.Empty
            };
        }

        private static string ToMethodTag(CookingStationType station)
        {
            return station switch
            {
                CookingStationType.Skillet => "fry",
                CookingStationType.Pot => "soup",
                CookingStationType.PortableOven => "bake",
                _ => string.Empty
            };
        }

        private static CookingStationType? ParseStationToken(string token)
        {
            return token?.Trim().ToLowerInvariant() switch
            {
                "skillet" => CookingStationType.Skillet,
                "pot" => CookingStationType.Pot,
                "portableoven" => CookingStationType.PortableOven,
                _ => null
            };
        }

        private static bool TryGetCookingProfileForItem(Item item, out CookingTagProfile profile)
        {
            profile = null;
            if (item == null) return false;

            if (ItemRegistry.TryGetCustomInfo(item, out CustomItemInfo customInfo) &&
                TryReadProfileFromCustomData(customInfo.CustomData, out profile))
                return true;

            EnsureVanillaItemTagsApplied();
            if (TryReadProfileFromTags(item.Stats?.tags, out profile))
                return true;
            return false;
        }

        private static bool TryGetCookingProfileForLiquid(string liquidId, out CookingTagProfile profile)
        {
            profile = null;
            if (string.IsNullOrWhiteSpace(liquidId)) return false;

            if (LiquidCookingProfiles.TryGetValue(liquidId, out profile) && profile.HasAnyTags)
                return true;
            return false;
        }

        private static bool TryReadProfileFromCustomData(IReadOnlyDictionary<string, object> customData, out CookingTagProfile profile)
        {
            profile = null;
            if (customData == null) return false;

            string ingredientString = ReadCustomDataString(customData, IngredientKey);
            string flavorString = ReadCustomDataString(customData, FlavorKey);
            string visualString = ReadCustomDataString(customData, VisualKey);

            profile = new CookingTagProfile(
                SplitMetadataValues(ingredientString),
                SplitMetadataValues(flavorString),
                SplitMetadataValues(visualString));

            return profile.HasAnyTags;
        }

        private static string ReadCustomDataString(IReadOnlyDictionary<string, object> customData, string key)
        {
            if (customData == null || string.IsNullOrWhiteSpace(key) || !customData.TryGetValue(key, out object raw) || raw == null)
                return null;

            return raw.ToString();
        }

        private static bool TryReadProfileFromTags(string rawTags, out CookingTagProfile profile)
        {
            profile = null;
            if (string.IsNullOrWhiteSpace(rawTags)) return false;

            List<string> ingredients = new();
            List<string> flavors = new();
            List<string> visuals = new();

            foreach (string rawTag in rawTags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string tag = rawTag.Trim();
                if (tag.StartsWith(IngredientTagPrefix, StringComparison.OrdinalIgnoreCase))
                    ingredients.Add(tag.Substring(IngredientTagPrefix.Length));
                else if (tag.StartsWith(FlavorTagPrefix, StringComparison.OrdinalIgnoreCase))
                    flavors.Add(tag.Substring(FlavorTagPrefix.Length));
                else if (tag.StartsWith(VisualTagPrefix, StringComparison.OrdinalIgnoreCase))
                    visuals.Add(tag.Substring(VisualTagPrefix.Length));
            }

            profile = new CookingTagProfile(ingredients, flavors, visuals);
            return profile.HasAnyTags;
        }

        private static IReadOnlyList<CookingDishDefinition> GetRegisteredDishDefinitions(CookingStationType station)
        {
            List<CookingDishDefinition> result = new();

            foreach (string id in ItemRegistry.GetRegisteredItemIds())
            {
                if (!ItemRegistry.TryGetCustomInfo(id, out CustomItemInfo info) || info?.CustomData == null)
                    continue;

                if (!TryReadDishDefinition(id, info.CustomData, out CookingDishDefinition definition))
                    continue;

                if (definition.Supports(station))
                    result.Add(definition);
            }

            if (Item.GlobalItems != null)
            {
                foreach (KeyValuePair<string, DishSeed> entry in DishSeeds)
                {
                    string id = entry.Key;
                    DishSeed seed = entry.Value;

                    if (ItemRegistry.TryGetCustomInfo(id, out _)) continue;
                    if (!Item.GlobalItems.ContainsKey(id)) continue;
                    if (!seed.Stations.Contains(station)) continue;

                    result.Add(new CookingDishDefinition
                    {
                        ResultId = id,
                        Stations = seed.Stations,
                        MethodTags = seed.MethodTags,
                        Profile = seed.Profile,
                        Amount = seed.Amount,
                        ResultCondition = seed.ResultCondition
                    });
                }
            }

            return result;
        }

        private static bool TryReadDishDefinition(string id, IReadOnlyDictionary<string, object> customData, out CookingDishDefinition definition)
        {
            definition = null;
            if (customData == null) return false;

            object dishValue;
            if (!customData.TryGetValue(DishKey, out dishValue) || dishValue == null)
                return false;

            if (!bool.TryParse(dishValue.ToString(), out bool isDish) || !isDish)
                return false;

            if (!TryReadProfileFromCustomData(customData, out CookingTagProfile profile))
                return false;

            string stationString = ReadCustomDataString(customData, StationKey);
            CookingStationType[] stations = SplitMetadataValues(stationString)
                .Select(ParseStationToken)
                .Where(value => value.HasValue)
                .Select(value => value.Value)
                .Distinct()
                .ToArray();

            if (stations.Length == 0)
                return false;

            definition = new CookingDishDefinition
            {
                ResultId = id,
                Stations = stations,
                MethodTags = SplitMetadataValues(ReadCustomDataString(customData, MethodKey)).ToArray(),
                Profile = profile
            };

            if (DishSeeds.TryGetValue(id, out DishSeed seed))
            {
                definition.Amount = seed.Amount;
                definition.ResultCondition = seed.ResultCondition;
                if (definition.MethodTags == null || definition.MethodTags.Length == 0)
                    definition.MethodTags = seed.MethodTags;
            }

            return true;
        }

        private static IEnumerable<string> SplitMetadataValues(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return Array.Empty<string>();

            return raw.Split(new[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(value => value.Trim())
                .Where(value => !string.IsNullOrWhiteSpace(value));
        }

        internal static void CookWithContainer(Body body, Item cookerItem, CookingStationType station)
        {
            if (body == null || cookerItem == null)
                return;

            Container container = cookerItem.GetComponent<Container>();
            WaterContainerItem waterContainer = cookerItem.GetComponent<WaterContainerItem>();
            if (container == null)
            {
                PlayerCamera.main.PlayUISound(PlayerCamera.UISoundType.Deny);
                return;
            }

            List<CookingInput> inputs = new();
            List<Item> solidIngredients = new();
            List<string> unsupportedSources = new();

            foreach (Transform child in container.transform)
            {
                if (!child.TryGetComponent<Item>(out Item containedItem)) continue;

                solidIngredients.Add(containedItem);
                if (TryGetCookingProfileForItem(containedItem, out CookingTagProfile profile))
                {
                    inputs.Add(new CookingInput
                    {
                        Profile = profile,
                        SourceId = containedItem.id,
                        IsLiquid = false
                    });
                }
                else
                {
                    unsupportedSources.Add(containedItem.id);
                }
            }


            if (waterContainer?.stack != null)
            {
                foreach (LiquidStack stack in waterContainer.stack)
                {
                    if (stack == null || stack.amount < MinimumLiquidCookAmount) continue;

                    if (TryGetCookingProfileForLiquid(stack.liquidId, out CookingTagProfile profile))
                    {
                        inputs.Add(new CookingInput
                        {
                            Profile = profile,
                            SourceId = stack.liquidId,
                            IsLiquid = true
                        });
                    }
                    else
                    {
                        unsupportedSources.Add(stack.liquidId);
                    }
                }
            }

            if (inputs.Count == 0)
            {
                PlayerCamera.main.DoAlert("You can't cook with nothing!");
                PlayerCamera.main.PlayUISound(PlayerCamera.UISoundType.Deny);
                return;
            }

            if (inputs.Count <= 2)
            {
                PlayerCamera.main.DoAlert("You need more ingredients for a good meal!");
                PlayerCamera.main.PlayUISound(PlayerCamera.UISoundType.Deny);
                return;
            }

            if(station == CookingStationType.Pot)
            {
                if (waterContainer == null || waterContainer.stack == null || waterContainer.stack.Sum(stack => stack?.amount ?? 0f) < MinimumLiquidCookAmount)
                {
                    PlayerCamera.main.DoAlert("You can't cook a soup without liquid!"); // I gotta add more sass ;)
                    PlayerCamera.main.PlayUISound(PlayerCamera.UISoundType.Deny);
                    return;
                }
            }

            if (unsupportedSources.Count > 0)
            {
                string unsupported = string.Join(", ", unsupportedSources.Distinct(StringComparer.OrdinalIgnoreCase));
                PlayerCamera.main.DoAlert("As much as cooking with a " + unsupported + " sounds fun, it's not possible...");
                PlayerCamera.main.PlayUISound(PlayerCamera.UISoundType.Deny);
                return;
            }

            List<CookingDishScore> scoredDishes = GetRegisteredDishDefinitions(station)
                .Select(dish => new CookingDishScore
                {
                    Dish = dish,
                    Score = ScoreDish(dish, inputs, station)
                })
                .Where(result => result.Score > 0)
                .OrderByDescending(result => result.Score)
                .ToList();

            if (scoredDishes.Count == 0)
            {
                PlayerCamera.main.DoAlert("The cosmic forces prevent this combination from creating a meal.. (how did you manage this?)");
                PlayerCamera.main.PlayUISound(PlayerCamera.UISoundType.Deny);
                return;
            }

            CookingDishDefinition selectedDish = SelectDish(scoredDishes);

            foreach (Item ingredient in solidIngredients)
                UnityEngine.Object.Destroy(ingredient.gameObject);

            waterContainer?.DrainAll();

            SpawnCookedResult(selectedDish, cookerItem, container);
            if (PlayerCamera.main.currentContainer == container)
                PlayerCamera.main.RepopulateContainer();

            Sound.Play("combine", cookerItem.transform.position);
            PlayerCamera.main.PlayUISound(PlayerCamera.UISoundType.Click);
        }

        private static int ScoreDish(CookingDishDefinition dish, IEnumerable<CookingInput> inputs, CookingStationType station)
        {
            int total = 0;
            foreach (CookingInput input in inputs)
            {
                if (input?.Profile == null) continue;

                total += CountMatches(input.Profile.IngredientTags, dish.Profile.IngredientTags) * IngredientTagScore;
                total += CountMatches(input.Profile.FlavorTags, dish.Profile.FlavorTags) * FlavorTagScore;
                total += CountMatches(input.Profile.VisualTags, dish.Profile.VisualTags) * VisualTagScore;
            }

            if (dish.MethodTags != null && dish.MethodTags.Contains(ToMethodTag(station), StringComparer.OrdinalIgnoreCase))
                total += MethodTagScore;

            return total;
        }

        private static int CountMatches(HashSet<string> sourceTags, HashSet<string> targetTags)
        {
            if (sourceTags == null || targetTags == null || sourceTags.Count == 0 || targetTags.Count == 0)
                return 0;

            int matches = 0;
            foreach (string tag in sourceTags)
            {
                if (targetTags.Contains(tag))
                    matches++;
            }

            return matches;
        }

        private static CookingDishDefinition SelectDish(List<CookingDishScore> scoredDishes)
        {
            int bestScore = scoredDishes[0].Score;
            List<CookingDishScore> bestMatches = scoredDishes
                .Where(result => result.Score == bestScore)
                .ToList();

            List<CookingDishScore> lowerMatches = scoredDishes
                .Where(result => result.Score < bestScore && result.Score >= bestScore - LowerScoreWindow)
                .ToList();

            if (lowerMatches.Count == 0 || UnityEngine.Random.value <= BestDishChance)
                return bestMatches[UnityEngine.Random.Range(0, bestMatches.Count)].Dish;

            return lowerMatches[UnityEngine.Random.Range(0, lowerMatches.Count)].Dish;
        }

        private static void SpawnCookedResult(CookingDishDefinition selectedDish, Item cookerItem, Container container)
        {
            for (int i = 0; i < selectedDish.Amount; i++)
            {
                Item resultItem = Utils.Create(selectedDish.ResultId, cookerItem.transform.position, 0f).GetComponent<Item>();
                resultItem.condition = selectedDish.ResultCondition;

                if (container != null && container.CanHoldItem(resultItem))
                    container.LoadItem(resultItem);
                else
                    PlayerCamera.main.body.AutoPickUpItem(resultItem);
            }
        }

        private sealed class CookingTagProfile
        {
            public HashSet<string> IngredientTags { get; }
            public HashSet<string> FlavorTags { get; }
            public HashSet<string> VisualTags { get; }

            public bool HasAnyTags =>
                IngredientTags.Count > 0 || FlavorTags.Count > 0 || VisualTags.Count > 0;

            public CookingTagProfile(
                IEnumerable<string> ingredientTags = null,
                IEnumerable<string> flavorTags = null,
                IEnumerable<string> visualTags = null)
            {
                IngredientTags = CreateTagSet(ingredientTags);
                FlavorTags = CreateTagSet(flavorTags);
                VisualTags = CreateTagSet(visualTags);
            }

            private static HashSet<string> CreateTagSet(IEnumerable<string> tags)
            {
                HashSet<string> result = new(StringComparer.OrdinalIgnoreCase);
                if (tags == null) return result;

                foreach (string tag in tags)
                {
                    if (!string.IsNullOrWhiteSpace(tag))
                        result.Add(tag.Trim());
                }

                return result;
            }
        }

        private sealed class CookingDishDefinition
        {
            public string ResultId { get; set; } = string.Empty;
            public CookingStationType[] Stations { get; set; } = Array.Empty<CookingStationType>();
            public string[] MethodTags { get; set; } = Array.Empty<string>();
            public CookingTagProfile Profile { get; set; } = new();
            public int Amount { get; set; } = 1;
            public float ResultCondition { get; set; } = 1f;

            public bool Supports(CookingStationType station)
            {
                return Stations.Contains(station);
            }
        }

        private sealed class CookingInput
        {
            public CookingTagProfile Profile { get; set; } = new();
            public string SourceId { get; set; } = string.Empty;
            public bool IsLiquid { get; set; }
        }

        private sealed class CookingDishScore
        {
            public CookingDishDefinition Dish { get; set; } = null!;
            public int Score { get; set; }
        }

        private sealed class DishSeed
        {
            public CookingStationType[] Stations { get; set; } = Array.Empty<CookingStationType>();
            public string[] MethodTags { get; set; } = Array.Empty<string>();
            public CookingTagProfile Profile { get; set; } = new();
            public int Amount { get; set; } = 1;
            public float ResultCondition { get; set; } = 1f;
        }
    }
}
