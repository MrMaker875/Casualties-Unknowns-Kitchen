using CUCoreLib.Data;
using CUCoreLib.Helpers;
using CUCoreLib.Registries;

namespace CU_sKitchen
{
    public partial class Plugin
    {
        private void RegisterProduceBuildingEntities()
        {
            BuildingEntityRegistry.Register("avocadotree", new CustomBuildingEntityDefinition
            {
                Name = "Avocado Bush", // Name, mouse cursor over name
                Description = "A plant with a thin tall stem, green fruit grow from the branches.", // Description, self-explanatory
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.avocado.png"),
                Health = 130f, // Health. One hit = 20-30 damage from barehand expy, better to have this lower for player comfort
                HitSoundReferenceId = "rustle", // Sound to play when hit, shorthands are:
                /* case "metal":
                    normalized = "turret";
                    break;
                case "rubber":
                    normalized = "glowplant";
                    break;
                case "plant":
                    normalized = "glowplant";
                    break;
                case "rustle":
                    normalized = "geotree";
                    break;
                case "crystal":
                    normalized = "BloodCrystal";
                    break;
                case "flesh":
                    normalized = "shadecrawler";
                    break;
                case "pop":
                    normalized = "pop";
                    break;
                case "ice":
                case "glass":
                    normalized = "icestalagmite";
                    break;
                case "stone":
                case "rock":
                    normalized = "stoneplant";
                    break;
                case "chain":
                    normalized = "barbedwirefence";
                    break;*/ // tbh it's probably fine as rustle for all of them
                Placement = BuildingPlacementType.Floor, // Ceiling plants would be cool, for like future tree-based plants? :eyes: 
                SpawnMinPerChunk = 0.05f,
                SpawnMaxPerChunk = 0.15f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(1, 2, 5), // Layers to spawn at
                SurfaceOffset = 2.25f, // Surface offset, how far above the ground the building entity is placed. I tried basing this off the sprite image, but you would need some testing
                RequireGround = true, // Whether or not the building breaks when the ground below it does. If false, it can float in midair, which is a bit weird for a plant
                GenerationStyle = BuildingGenerationStyle.Standard, // Keep standard
                AlwaysDrop = new[] // Always drop these items at 100% condition
                {
                    BuildingEntityRegistry.AddDrop("avocado"),
                    BuildingEntityRegistry.AddDrop("woodscraps")
                },
                ItemsDropOnDestroy = new[] // Drop these items at arg[1] % chance, arg[2] % min condition, arg[3] % max condition
                {
                    BuildingEntityRegistry.AddDrop("avocado", 1f, 0.55f, 0.6f)
                }
            });

            BuildingEntityRegistry.Register("bambooclump", new CustomBuildingEntityDefinition
            {
                Name = "Bamboo Clump",
                Description = "Multiple tall and thin sticks grow from the ground, they don't seem edible.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.bamboo.png"),
                Health = 360f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.1f,
                SpawnMaxPerChunk = 0.15f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(2, 3, 5),
                SurfaceOffset = 2.75f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("bamboo"),
                    BuildingEntityRegistry.AddDrop("woodscraps")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("bamboo", 1f, 0.55f, 0.35f),
                    BuildingEntityRegistry.AddDrop("bamboo", 1f, 0.55f, 0.35f)
                }
            });

            BuildingEntityRegistry.Register("bellpeppershrub", new CustomBuildingEntityDefinition
            {
                Name = "Bell Pepper Shrub",
                Description = "A shrub that grows a cluster of red fruit.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.bellpepper.png"),
                Health = 240f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.1f,
                SpawnMaxPerChunk = 0.2f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(3, 4, 5),
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("bellpepper"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("bellpepper", 1f, 0.55f, 0.55f)
                }
            });

            BuildingEntityRegistry.Register("blueberrybush", new CustomBuildingEntityDefinition
            {
                Name = "Blueberry Bush",
                Description = "An Expansive bush, it has round blue berries.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.blueberry.png"),
                Health = 180f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.05f,
                SpawnMaxPerChunk = 0.12f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(1, 2, 5),
                SurfaceOffset = 1.25f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("blueberry"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("blueberry", 1f, 0.55f, 0.85f)
                }
            });

            BuildingEntityRegistry.Register("broccoliplant", new CustomBuildingEntityDefinition
            {
                Name = "Broccoli Plant",
                Description = "The edible part of this plant is in the middle, it looks like a green flower",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.broccoli.png"),
                Health = 200f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.05f,
                SpawnMaxPerChunk = 0.15f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(1, 2, 5),
                SurfaceOffset = 1.25f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("broccoli"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("broccoli", 1f, 0.55f, 0.45f)
                }
            });

            BuildingEntityRegistry.Register("brusselsproutstalk", new CustomBuildingEntityDefinition
            {
                Name = "Brussels Sprout Stalk",
                Description = "The vegatables on this plant grow on the stem.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.brusselsprouts.png"),
                Health = 240f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.15f,
                SpawnMaxPerChunk = 0.3f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(2, 5),
                SurfaceOffset = 2.5f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("brusselsprout"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("brusselsprout", 1f, 0.55f, 0.55f),
                    BuildingEntityRegistry.AddDrop("brusselsprout", 1f, 0.55f, 0.55f),
                    BuildingEntityRegistry.AddDrop("brusselsprout", 1f, 0.55f, 0.55f),
                    BuildingEntityRegistry.AddDrop("brusselsprout", 1f, 0.55f, 0.55f)
                }
            });

            BuildingEntityRegistry.Register("cornstalk", new CustomBuildingEntityDefinition
            {
                Name = "Corn Stalk",
                Description = "A tall stalk that allows for corn to be grown on.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.corn.png"),
                Health = 260f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.05f,
                SpawnMaxPerChunk = 0.15f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(2, 3, 4, 5),
                SurfaceOffset = 2.25f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("corn"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("corn", 1f, 0.55f, 0.55f)
                }
            });

            BuildingEntityRegistry.Register("dragonfruitcactus", new CustomBuildingEntityDefinition
            {
                Name = "Dragonfruit Cactus",
                Description = "A tall plant where it's fruit grows on a leaf.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.dragonfruit.png"),
                Health = 320f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.05f,
                SpawnMaxPerChunk = 0.12f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(3, 4, 5),
                SurfaceOffset = 2.5f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("dragonfruit"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("dragonfruit", 0.5f, 0.55f, 0.45f)
                }
            });

            BuildingEntityRegistry.Register("garlicpatch", new CustomBuildingEntityDefinition
            {
                Name = "Garlic Patch",
                Description = "A plant that has it's vegetable grow in the ground instead of in the air, smells a bit weird",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.garlic.png"),
                Health = 170f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.1f,
                SpawnMaxPerChunk = 0.2f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(2, 3, 4, 5),
                SurfaceOffset = 0.9f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("garlic"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("garlic", 0.5f, 0.55f, 0.6f),
                    BuildingEntityRegistry.AddDrop("garlic", 0.5f, 0.55f, 0.6f),
                }
            });

            BuildingEntityRegistry.Register("purplegrapevine", new CustomBuildingEntityDefinition
            {
                Name = "Purple Grape Vine",
                Description = "Just like the Green Grape Vine, this plant seems to grow tasty purple grapes.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.grape.png"),
                Health = 220f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.15f,
                SpawnMaxPerChunk = 0.2f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(1, 2, 3, 4, 5),
                SurfaceOffset = 2f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("purplegrape", 1f, 0.55f, 0.7f)
                }
            });

            BuildingEntityRegistry.Register("grapefruittree", new CustomBuildingEntityDefinition
            {
                Name = "Grapefruit Tree",
                Description = "It's fruit reminds you of a Orange, but this isn't exactly a Orange.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.grapefruit.png"),
                Health = 300f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.15f,
                SpawnMaxPerChunk = 0.2f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(1, 2, 3, 4, 5),
                SurfaceOffset = 1.75f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("woodscraps")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("grapefruit", 1f, 0.55f, 0.55f)
                }
            });

            BuildingEntityRegistry.Register("greengrapevine", new CustomBuildingEntityDefinition
            {
                Name = "Green Grape Vine",
                Description = "Just like the Purple Grape Vine, this plant seems to grow tasty green grapes.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.greengrape.png"),
                Health = 220f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.15f,
                SpawnMaxPerChunk = 0.2f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(1, 2, 3, 4, 5),
                SurfaceOffset = 2f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("greengrape", 1f, 0.55f, 0.7f)
                }
            });

            BuildingEntityRegistry.Register("mangotree", new CustomBuildingEntityDefinition
            {
                Name = "Mango Tree",
                Description = "This tree grows vibrant fruit that are quite sweet.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.mango.png"),
                Health = 340f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.1f,
                SpawnMaxPerChunk = 0.2f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(3, 4, 5),
                SurfaceOffset = 2.75f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("mango"),
                    BuildingEntityRegistry.AddDrop("woodscraps")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("mango", .5f, 0.55f, 0.55f),
                    BuildingEntityRegistry.AddDrop("mango", .5f, 0.55f, 0.55f),
                }
            });

            BuildingEntityRegistry.Register("peanutplant", new CustomBuildingEntityDefinition
            {
                Name = "Peanut Plant",
                Description = "It's small size can make it difficult to spot and harvest, but you found it.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.peanut.png"),
                Health = 160f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.05f,
                SpawnMaxPerChunk = 0.15f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(3, 4, 5),
                SurfaceOffset = 1f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("peanut"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("peanut", 1f, 0.55f, 0.7f),
                    BuildingEntityRegistry.AddDrop("peanut", 1f, 0.55f, 0.7f),
                    BuildingEntityRegistry.AddDrop("peanut", .5f, 0.55f, 0.7f),
                    BuildingEntityRegistry.AddDrop("peanut", .5f, 0.55f, 0.7f),
                }
            });

            BuildingEntityRegistry.Register("pineappleplant", new CustomBuildingEntityDefinition
            {
                Name = "Pineapple Plant",
                Description = "You'd expect something like this to grow more then just one, maybe that one fruit is very good?",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.pineapple.png"),
                Health = 240f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.15f,
                SpawnMaxPerChunk = 0.25f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(3, 4, 5),
                SurfaceOffset = 1.75f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("pineapple", 1f, 0.55f, 0.5f)
                }
            });

            BuildingEntityRegistry.Register("pumpkinvine", new CustomBuildingEntityDefinition
            {
                Name = "Pumpkin Vine",
                Description = "What grows from this vine is both heavy and delicious.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.pumpkin.png"),
                Health = 210f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.05f,
                SpawnMaxPerChunk = 0.1f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(1, 2, 3, 4, 5),
                SurfaceOffset = 1f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("pumpkin"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("pumpkin", 1f, 0.55f, 0.45f)
                }
            });

            BuildingEntityRegistry.Register("tomatovine", new CustomBuildingEntityDefinition
            {
                Name = "Wild Tomato Vine",
                Description = "The fruit on this plant seem to grow as one clump together.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.tomato.png"),
                Health = 180f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.05f,
                SpawnMaxPerChunk = 0.15f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(1, 2, 3, 5),
                SurfaceOffset = 0.9f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("tomato"),
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("tomato", 1f, 0.55f, 0.65f)
                }
            });

            BuildingEntityRegistry.Register("turnippatch", new CustomBuildingEntityDefinition
            {
                Name = "Turnip Patch",
                Description = "It's colour is a lot more unique compared to most other fruit and vegetable.",
                Sprite = AssetLoader.LoadEmbeddedSprite("Sprites.BuildingEntites.turnip.png"),
                Health = 180f,
                HitSoundReferenceId = "rustle",
                Placement = BuildingPlacementType.Floor,
                SpawnMinPerChunk = 0.15f,
                SpawnMaxPerChunk = 0.2f,
                SpawnLayers = BuildingEntityRegistry.LayersToMask(1, 2, 5),
                SurfaceOffset = 0.9f,
                RequireGround = true,
                GenerationStyle = BuildingGenerationStyle.Standard,
                AlwaysDrop = new[]
                {
                    BuildingEntityRegistry.AddDrop("foliage")
                },
                ItemsDropOnDestroy = new[]
                {
                    BuildingEntityRegistry.AddDrop("turnip", 1f, 0.55f, 0.6f)
                }
            });
        }
    }
}
