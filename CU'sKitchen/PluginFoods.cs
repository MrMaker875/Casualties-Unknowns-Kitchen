using System;
using System.Collections.Generic;
using CUCoreLib.Data;
using CUCoreLib.Helpers;
using CUCoreLib.Registries;
using UnityEngine;

namespace CU_sKitchen
{
    public partial class Plugin
    {




        public void CustomItems()
        {
            ItemRegistry.Register("acorn", new ItemInfo
            {
                fullName = "Acorn",
                description = "A small, tough nut. Not great raw.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 240f,
                destroyAtZeroCondition = true,
                weight = 0.25f,
                scaleWeightWithCondition = true,
                value = 1,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(1.5f, 0.05f);
                    body.Drink(-4.55f);
                    body.happiness += -20f;
                    body.talker.EatBad();
                    body.sicknessAmount += 5f;
                    body.limbs[0].pain += 10f;
                    body.limbs[0].muscleHealth -= 5f;

                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("nut") }

            }, AssetLoader.LoadEmbeddedSprite("Sprites.Acorn.png"));

            ItemRegistry.Register("almond", new ItemInfo
            {
                fullName = "Almond",
                description = "A rich little nutrient-filled nut. Healthy and tasty, with earthy flavours. Great crunch.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 240f,
                destroyAtZeroCondition = true,
                weight = 0.1f,
                scaleWeightWithCondition = true,
                value = 2,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(2f, 0.05f);
                    body.talker.EatGood();
                    body.Drink(-0.25f);
                    body.happiness += 0.5f;
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("nut") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Almond.png"));
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
                    body.talker.EatGood();
                    body.happiness += 0.25f;
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality>
                {
                    new CraftingQuality("produce"),
                    new CraftingQuality("sliceable")
                },
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Apple.png"));

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
                useAction = (body, item) =>
                {
                    body.Eat(7f, 0.6f);
                    body.happiness += 0.75f;
                    body.talker.EatGood();
                    item.condition -= 0.125f;
                },
            }, AssetLoader.LoadEmbeddedSprite("Sprites.ApplePie.png"));


            ItemRegistry.Register("avocado", new ItemInfo
            {
                fullName = "Avocado",
                description = "A heavy creamy vegetable with a rich texture. Mildly earthy, and melts in your mouth.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 80f,
                destroyAtZeroCondition = true,
                weight = 0.6f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(4.5f, 0.15f);
                    body.Drink(1f);
                    body.happiness += 3f;
                    body.talker.EatGood();
                    item.condition -= 0.334f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Avocado.png"));

            ItemRegistry.Register("bamboo", new ItemInfo
            {
                fullName = "Bamboo",
                description = "A sturdy bamboo stalk with a fibrous, woody texture. Absolutely not edible.",
                category = "custom",
                usable = false,
                weight = 1.5f,
                value = 2,
                rec = new Recognition(2)
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Bamboo.png"));

            ItemRegistry.Register("beansprout", new ItemInfo
            {
                fullName = "Bean Sprout",
                description = "A tender sprout with a little moisture. Goes great in stirfries.",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 20f,
                destroyAtZeroCondition = true,
                weight = 0.15f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(1.5f, 0.05f);
                    body.Drink(1.5f);
                    body.happiness += 1f;
                    body.talker.EatMediocre();

                    item.condition -= 0.2f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.BeanSprout.png"));
            ItemRegistry.Register("bellpepper", new ItemInfo
            {
                fullName = "Bell Pepper",
                description = "A sweet pepper with plenty of crunch and a fresh finish.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 30f,
                destroyAtZeroCondition = true,
                weight = 0.35f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(2.5f, 0.1f);
                    body.Drink(2f);
                    body.happiness += 1.5f;
                    item.condition -= 0.5f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.BellPepper.png"));
            ItemRegistry.Register("blueberry", new ItemInfo
            {
                fullName = "Blueberry",
                description = "A handful of berries with a nice taste but very little staying power.",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 15f,
                destroyAtZeroCondition = true,
                weight = 0.12f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(1.75f, 0.05f);
                    body.Drink(1f);
                    body.talker.EatGood();

                    body.happiness += 1.5f;
                    item.condition -= 0.2f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Blueberry.png"));
            ItemRegistry.Register("broccoli", new ItemInfo
            {
                fullName = "Broccoli",
                description = "A dense green head. Nutritious enough, but not too rewarding raw.",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 40f,
                destroyAtZeroCondition = true,
                weight = 0.5f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(2.5f, 0.1f);
                    body.Drink(0.5f);
                    body.happiness += -0.5f;
                    body.talker.EatBad();
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Broccoli.png"));
            ItemRegistry.Register("brusselsprout", new ItemInfo
            {
                fullName = "Brussel Sprout",
                description = "A bitter little sprout with a sharp green taste. Supposedly really good for you, though...",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 40f,
                destroyAtZeroCondition = true,
                weight = 0.35f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(2.2f, 0.05f);
                    body.Drink(0.25f);
                    body.happiness += -1f;
                    body.talker.EatBad();
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.BrusselSprout.png"));
            ItemRegistry.Register("cherry", new ItemInfo
            {
                fullName = "Cherry",
                description = "A sweet red cherry that is pleasant. This one is particularly enjoyable!",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 18f,
                destroyAtZeroCondition = true,
                weight = 0.12f,
                scaleWeightWithCondition = true,
                value = 5,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(1.75f, 0.05f);
                    body.Drink(5f);
                    body.happiness += 3.5f; // I wonder which of these is my favourite fruit ;)
                    item.condition -= 0.5f;
                    body.talker.EatGood();

                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Cherry.png"));
            ItemRegistry.Register("coralcreep", new ItemInfo
            {
                fullName = "Coralcreep", // Starbound reference hehe
                description = "A strange coral-like growth with a rubbery texture and a faintly unpleasant taste.",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 25f,
                destroyAtZeroCondition = true,
                weight = 0.3f,
                scaleWeightWithCondition = true,
                value = 5,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(1.5f, 0.05f);
                    body.Drink(0.5f);
                    body.happiness += -1.5f;
                    body.talker.EatBad();
                    body.sicknessAmount += 0.5f;
                    item.condition -= 0.5f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Coralcreep.png"));
            ItemRegistry.Register("corn", new ItemInfo
            {
                fullName = "Corn",
                description = "A fresh ear of corn. Cheaper to snack on than it is satisfying.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 40f,
                destroyAtZeroCondition = true,
                weight = 0.45f,
                scaleWeightWithCondition = true,
                value = 5,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(3f, 0.1f);
                    body.Drink(1f);
                    body.talker.EatMediocre();

                    body.happiness += 1f;
                    item.condition -= 0.34f;
                    if(item.condition <= 0.35f)
                    { 
                        int slot = body.SlotOf(item);
                        var obj = Utils.Create("corncob", item.transform.position, 0f).GetComponent<Item>();
                        body.PickUpItem(obj, slot);
                    }
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Corn.png"));
            ItemRegistry.Register("corncob", new ItemInfo
            {
                fullName = "Corncob",
                description = "A bare cob with very little left on it. Technically edible, barely worth it.",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 80f,
                destroyAtZeroCondition = true,
                weight = 0.2f,
                scaleWeightWithCondition = true,
                value = 1,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(1.25f, 0f);
                    body.Drink(0.25f);

                    body.happiness += -0.5f;
                    body.talker.EatBad();
                    item.condition -= 1f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Corncob.png"));
            ItemRegistry.Register("dragonfruit", new ItemInfo
            {
                fullName = "Dragonfruit",
                description = "A vivid fruit with some moisture and sweetness. The skin like a dragon's scales!",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 45f,
                destroyAtZeroCondition = true,
                weight = 0.55f,
                scaleWeightWithCondition = true,
                value = 8,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(3.25f, 0.1f);
                    body.Drink(3f);
                    body.happiness += 4.5f;
                    body.talker.EatGood();
                    item.condition -= 0.5f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Dragonfruit.png"));
            ItemRegistry.Register("eggplant", new ItemInfo
            {
                fullName = "Eggplant",
                description = "A soft purple vegetable. Not great raw.",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 45f,
                destroyAtZeroCondition = true,
                weight = 0.45f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(2.8f, 0.1f);
                    body.Drink(0.75f);
                    body.talker.EatMediocre();

                    item.condition -= 0.5f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Eggplant.png"));
            ItemRegistry.Register("garlic", new ItemInfo
            {
                fullName = "Garlic",
                description = "Sharp, dry, and intense with a lingering bite.",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 60f,
                destroyAtZeroCondition = true,
                weight = 0.15f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(1f, 0f);
                    body.Drink(-0.5f);
                    body.temperature += 1f;
                    body.limbs[0].pain += 8f;
                    body.happiness += -2f;
                    body.talker.EatMediocre();
                    body.sicknessAmount -= 0.25f;
                    item.condition -= 0.5f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Garlic.png"));
            ItemRegistry.Register("grapefruit", new ItemInfo
            {
                fullName = "Grapefruit",
                description = "A tart citrus fruit with some juice. Tangy and unique!",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 45f,
                destroyAtZeroCondition = true,
                weight = 0.5f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(2.5f, 0.1f);
                    body.Drink(4.5f);
                    body.happiness += 0.5f;
                    body.talker.EatGood();

                    body.sicknessAmount += 0.25f;
                    item.condition -= 0.34f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Grapefruit.png"));
            ItemRegistry.Register("greenapple", new ItemInfo
            {
                fullName = "Green Apple",
                description = "A crisp sour apple with a little hydration. Mmm, can never go wrong with this.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 30f,
                destroyAtZeroCondition = true,
                weight = 0.45f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(2.5f, 0.1f);
                    body.Drink(2.75f);
                    body.happiness += 1f;
                    item.condition -= 0.334f;
                    body.talker.EatGood();

                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.GreenApple.png"));
            ItemRegistry.Register("greengrape", new ItemInfo
            {
                fullName = "Green Grape",
                description = "A juicy grape cluster that vanishes almost immediately once snacked on.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 18f,
                destroyAtZeroCondition = true,
                weight = 0.1f,
                scaleWeightWithCondition = true,
                value = 6,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(1.8f, 0.05f);
                    body.Drink(1.2f);
                    body.happiness += 1f;
                    item.condition -= 0.2f;
                    body.talker.EatGood();

                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.GreenGrape.png"));
            ItemRegistry.Register("hazelnut", new ItemInfo
            {
                fullName = "Hazelnut",
                description = "A small nut with decent calories and a dry roasted taste.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 240f,
                destroyAtZeroCondition = true,
                weight = 0.1f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(2f, 0.05f);
                    body.Drink(-0.25f);
                    body.talker.EatGood();

                    body.happiness += 0.5f;
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Hazelnut.png"));
            ItemRegistry.Register("honiedwatermelonslice", new ItemInfo
            {
                fullName = "Honied Watermelon Slice",
                description = "A sweetened watermelon slice. Nicely refreshing, great to cool off with.",
                category = "food",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 25f,
                destroyAtZeroCondition = true,
                weight = 0.5f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(4.5f, 0.2f);
                    body.temperature -= 1f;
                    body.limbs[0].GetComponent<ChilledLimb>().timeLeft += 50f;
                    body.limbs[0].GetComponent<ChilledLimb>().maxTime += 50f;
                    body.Drink(4f);
                    body.happiness += 4.5f;
                    body.talker.EatGood();
                    item.condition -= 0.5f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.HoniedWatermelonSlice.png"));
            ItemRegistry.Register("mango", new ItemInfo
            {
                fullName = "Mango",
                description = "A juicy tropical fruit with bright sugar and plenty of hydration.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 45f,
                destroyAtZeroCondition = true,
                weight = 0.55f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(3.5f, 0.1f);
                    body.Drink(3f);
                    body.happiness += 2.5f;
                    body.talker.EatGood();
                    item.condition -= 0.5f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Mango.png"));
            ItemRegistry.Register("microwave", new CustomItemInfo
            {
                fullName = "Microwave",
                description = "A compact metalic box that is capable of heating up and cooking food.",
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
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Microwave.png"));
            ItemRegistry.Register("orange", new ItemInfo
            {
                fullName = "Orange",
                description = "A juicy citrus fruit with bright acidity and decent hydration. You don't like it for some reason..?",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 40f,
                destroyAtZeroCondition = true,
                weight = 0.4f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(2.75f, 0.1f);
                    body.Drink(3f);
                    body.happiness -= 1.5f;
                    body.sicknessAmount += 2f;
                    body.talker.EatMediocre();
                    item.condition -= .334f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Orange.png"));
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
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Oven.png"));
            ItemRegistry.Register("passionfruit", new ItemInfo
            {
                fullName = "Passionfruit",
                description = "A fragrant fruit with a bright taste and only modest nourishment.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 35f,
                destroyAtZeroCondition = true,
                weight = 0.3f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(2.5f, 0.1f);
                    body.Drink(2f);
                    body.happiness += 2f;
                    body.talker.EatGood();

                    item.condition -= 0.5f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Passionfruit.png"));
            ItemRegistry.Register("peach", new ItemInfo
            {
                fullName = "Peach",
                description = "A soft fruit with some sweetness. A great taste.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 35f,
                destroyAtZeroCondition = true,
                weight = 0.4f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(3f, 0.1f);
                    body.Drink(2.5f);
                    body.happiness += 2f;
                    item.condition -= 0.334f;
                    body.talker.EatGood();

                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Peach.png"));
            ItemRegistry.Register("peanut", new ItemInfo
            {
                fullName = "Peanut",
                description = "A dry little nut with some energy and fulfillment. Luckily, you aren't allergic to it.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 240f,
                destroyAtZeroCondition = true,
                weight = 0.08f,
                scaleWeightWithCondition = true,
                value = 2,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(1.75f, 0.05f);
                    body.Drink(-0.25f);
                    body.happiness += 0.5f;
                    item.condition -= 1f;
                    body.talker.EatGood();

                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Peanut.png"));
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
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Pepper.png"));
            
            ItemRegistry.Register("pineapple", new ItemInfo
            {
                fullName = "Pineapple",
                description = "A tart juicy fruit with strong sweetness and a prickly finish.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 55f,
                destroyAtZeroCondition = true,
                weight = 0.8f,
                scaleWeightWithCondition = true,
                value = 5,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(3.5f, 0.1f);
                    body.Drink(3.5f);
                    body.happiness += 2.5f;
                    body.talker.EatGood();
                    item.condition -= .2f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Pineapple.png"));
            ItemRegistry.Register("plum", new ItemInfo
            {
                fullName = "Plum",
                description = "A small stone fruit with light sweetness and soft watery flesh.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 28f,
                destroyAtZeroCondition = true,
                weight = 0.2f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(2.25f, 0.05f);
                    body.Drink(5.75f);
                    body.happiness += 1f;
                    item.condition -= 0.5f;
                    body.talker.EatGood();

                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Plum.png"));
            ItemRegistry.Register("pumpkin", new ItemInfo
            {
                fullName = "Pumpkin",
                description = "A thick squash with dense flesh and mild sweetness. Filling!",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 90f,
                destroyAtZeroCondition = true,
                weight = 0.9f,
                scaleWeightWithCondition = true,
                value = 7,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(4.25f, 0.15f);
                    body.Drink(1.5f);
                    body.happiness += 0.5f;
                    item.condition -= .334f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Pumpkin.png"));
            ItemRegistry.Register("purplegrape", new ItemInfo
            {
                fullName = "Purple Grape",
                description = "A bundle of sweet grapes. Pops right into your mouth, how convenient!",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 18f,
                destroyAtZeroCondition = true,
                weight = 0.1f,
                scaleWeightWithCondition = true,
                value = 5,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(1.8f, 0.05f);
                    body.talker.EatGood();

                    body.Drink(2.2f);
                    body.happiness += 1.5f;
                    item.condition -= .2f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.PurpleGrape.png"));
            ItemRegistry.Register("radish", new ItemInfo
            {
                fullName = "Radish",
                description = "A peppery root vegetable with a sharp earthy crunch.",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 50f,
                destroyAtZeroCondition = true,
                weight = 0.25f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(1.75f, 0.05f);
                    body.Drink(0.75f);
                    body.happiness += -0.5f;
                    body.talker.EatMediocre();
                    item.condition -= .334f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Radish.png"));
            ItemRegistry.Register("rambutan", new ItemInfo
            {
                fullName = "Rambutan",
                description = "A soft sweet fruit with gentle hydration and a floral finish.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 32f,
                destroyAtZeroCondition = true,
                weight = 0.25f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(2.5f, 0.1f);
                    body.Drink(2f);
                    body.happiness += 2f;
                    item.condition -= .334f;
                    body.talker.EatGood();

                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Rambutan.png"));
            ItemRegistry.Register("rice", new ItemInfo
            {
                fullName = "Rice",
                description = "Dry grains with a starchy chew and very little moisture. You'll need to cook it.",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 300f,
                destroyAtZeroCondition = true,
                weight = 0.2f,
                scaleWeightWithCondition = true,
                value = 2,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(0.75f, 0f);
                    body.Drink(-0.75f);
                    body.happiness += -1f;
                    body.talker.EatBad();
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Rice.png"));
            ItemRegistry.Register("ricestalk", new ItemInfo
            {
                fullName = "Rice Stalk",
                description = "A harvested rice stalk. Useful as an ingredient source, not a snack.",
                category = "custom",
                usable = false,
                weight = 0.25f,
                value = 1,
                rec = new Recognition(1)
            }, AssetLoader.LoadEmbeddedSprite("Sprites.RiceStalk.png"));
            ItemRegistry.Register("salmonberry", new ItemInfo
            {
                fullName = "Salmonberry",
                description = "A soft berry with a pleasant taste. It reminds you of a certain bear...", // Stardew reference ;)
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 20f,
                destroyAtZeroCondition = true,
                weight = 0.15f,
                scaleWeightWithCondition = true,
                value = 2,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(1.75f, 0.05f);
                    body.Drink(1.25f);
                    body.happiness += 2f;
                    body.talker.EatGood();

                    item.condition -= .5f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Salmonberry.png"));
            ItemRegistry.Register("squash", new ItemInfo
            {
                fullName = "Squash",
                description = "A sturdy squash with dense flesh and mild sweetness. Fairly filling.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 80f,
                destroyAtZeroCondition = true,
                weight = 0.7f,
                scaleWeightWithCondition = true,
                value = 6,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(4.5f, 0.1f);
                    body.Drink(1f);
                    body.happiness += 0.5f;
                    body.talker.EatGood();

                    item.condition -= .334f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Squash.png"));
            ItemRegistry.Register("strawberry", new ItemInfo
            {
                fullName = "Strawberry",
                description = "A sweet berry with nice flavor and barely any staying power.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 15f,
                destroyAtZeroCondition = true,
                weight = 0.12f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(1.75f, 0.05f);
                    body.Drink(1.25f);
                    body.happiness += 2f;
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Strawberry.png"));
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
                // tags = "fruit" + "cangetwet", // this isn't how you do it, it's seperated via commas (tag1, tag2)
                tags = "cangetwet",
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
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Tomato.png"));
            
            ItemRegistry.Register("turnip", new ItemInfo
            {
                fullName = "Turnip",
                description = "A plain root vegetable. It tastes mildly spicy to your tongue.",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 50f,
                destroyAtZeroCondition = true,
                weight = 0.45f,
                scaleWeightWithCondition = true,
                value = 1,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(4.5f, 0.1f);
                    body.Drink(1f);
                    item.condition -= 0.5f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Turnip.png"));
            ItemRegistry.Register("watermelon", new ItemInfo
            {
                fullName = "Watermelon",
                description = "An entire refreshing melon packed with water and light sweetness! This'll last a while.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 35f,
                destroyAtZeroCondition = true,
                weight = 1.2f,
                scaleWeightWithCondition = true,
                value = 12,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    var slot = body.SlotOf(item);
                    var obj = Utils.Create("watermelonslice", body.transform.position, 1f).GetComponent<Item>();
                    body.PickUpItem(obj, slot);
                    
                    item.condition -= .1f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Watermelon.png"));
            ItemRegistry.Register("watermelonslice", new ItemInfo
            {
                fullName = "Watermelon Slice",
                description = "A simple slice of watermelon. Refreshing!.",
                category = "custom",
                slotRotation = 45f,
                usable = true,
                usableOnLimb = false,
                decayMinutes = 20f,
                destroyAtZeroCondition = true,
                weight = 0.45f,
                scaleWeightWithCondition = true,
                value = 1,
                tags = "cangetwet",
                rec = new Recognition(2),
                useAction = (body, item) =>
                {
                    body.Eat(3.25f, 0.1f);
                    body.Drink(4f);
                    body.happiness += 2f;
                    body.limbs[0].GetComponent<ChilledLimb>().timeLeft += 30f;
                    body.limbs[0].GetComponent<ChilledLimb>().maxTime += 30f;
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.WatermelonSlice.png"));
            ItemRegistry.Register("wheat", new ItemInfo
            {
                fullName = "Wheat",
                description = "Raw wheat is dry, fibrous, and stubborn to chew. All not the attributes of an ideal snack...",
                category = "custom",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 300f,
                destroyAtZeroCondition = true,
                weight = 0.2f,
                scaleWeightWithCondition = true,
                value = 1,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(0.75f, 0f);
                    body.Drink(-1.5f);
                    body.happiness += -1f;
                    body.talker.EatBad();
                    item.condition -= 1f;
                },
                qualities = new List<CraftingQuality> { new CraftingQuality("produce") }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.Wheat.png"));
            ItemRegistry.Register("blobfleshscone", new ItemInfo
            {
                fullName = "Blob Flesh Scone",
                description = "A dense savory pastry with a rich filling and flaky bite.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 90f,
                destroyAtZeroCondition = true,
                weight = 0.55f,
                scaleWeightWithCondition = true,
                value = 11,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(9f, 0.3f);
                    body.Drink(0.5f);
                    body.happiness += 3f;
                    body.talker.EatGood();
                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.blobfleshscone.png"));
            ItemRegistry.Register("bloodnoodles", new ItemInfo
            {
                fullName = "Blood Noodles",
                description = "A dark noodle dish with real substance and flavour, though not exactly comforting to the mind.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 80f,
                destroyAtZeroCondition = true,
                weight = 0.6f,
                scaleWeightWithCondition = true,
                value = 12,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(8.5f, 0.25f);
                    body.Drink(1f);
                    body.happiness -= 3f;
                    body.sicknessAmount += 2.35f;
                    body.talker.EatMediocre();

                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.bloodnoodles.png"));
            ItemRegistry.Register("breadcrumbs", new ItemInfo
            {
                fullName = "Breadcrumbs",
                description = "Loose crumbs with a dry crunch and only a little substance. Not too appetizing.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 180f,
                destroyAtZeroCondition = true,
                weight = 0.1f,
                scaleWeightWithCondition = true,
                value = 6,
                tags = "cangetwet",
                rec = new Recognition(1),
                useAction = (body, item) =>
                {
                    body.Eat(4.5f, 0f);
                    body.Drink(-5.5f);
                    body.happiness += -1f;
                    body.talker.EatBad();
                    item.condition -= 0.25f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.breadcrumbs.png"));
            ItemRegistry.Register("candiedgeofruit", new ItemInfo
            {
                fullName = "Candied Geofruit",
                description = "Geofruit preserved in sugar into a glossy, sticky, absolutely phenomenal treat.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 140f,
                destroyAtZeroCondition = true,
                weight = 0.4f,
                scaleWeightWithCondition = true,
                value = 8,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(5.25f, 0.15f);
                    body.Drink(3f);
                    body.happiness += 3.5f;
                    body.talker.EatGood();
                    body.energy += 6f;
                    body.caffeinated += 8f;
                    body.sicknessAmount -= 3f;
                    item.condition -= 0.5f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.candiedgeofruit.png"));
            ItemRegistry.Register("cremeofmushroomsoup", new ItemInfo
            {
                fullName = "Creme of Mushroom Soup",
                description = "A creamy soup that is warming and edible. It reminds you of something..",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 60f,
                destroyAtZeroCondition = true,
                weight = 0.5f,
                scaleWeightWithCondition = true,
                value = 10,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(5.5f, 0.2f);
                    body.Drink(8.5f);
                    body.happiness += 2.5f;
                    body.talker.EatGood();
                    body.temperature += 1f;
                    body.sicknessAmount = Mathf.Max(0f, body.sicknessAmount - 0.4f);
                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.cremeofmushroomsoup.png"));
            ItemRegistry.Register("fleshwrappedcactus", new ItemInfo
            {
                fullName = "Flesh-Wrapped Cactus",
                description = "A rough improvised meal with some moisture and a hearty bite.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 70f,
                destroyAtZeroCondition = true,
                weight = 0.6f,
                scaleWeightWithCondition = true,
                value = 9,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(8f, 0.2f);
                    body.Drink(2.5f);
                    body.happiness += 1.5f;
                    body.talker.EatGood();
                    body.energy += 5f;
                    body.temperature += 0.35f;
                    body.sicknessAmount += 4.25f;
                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.fleshwrappedcactus.png"));
            ItemRegistry.Register("frieddough", new ItemInfo
            {
                fullName = "Fried Dough",
                description = "A simple fried snack. Filling enough, but heavy and dry on its own.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 90f,
                destroyAtZeroCondition = true,
                weight = 0.35f,
                scaleWeightWithCondition = true,
                value = 8,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(12f, 0.15f);
                    body.Drink(-5.5f);
                    body.happiness += 1.5f;
                    body.talker.EatMediocre();
                    body.energy += 4f;
                    body.sicknessAmount += 3.2f;
                    item.condition -= 0.5f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.frieddough.png"));
            ItemRegistry.Register("friedmeat", new ItemInfo
            {
                fullName = "Fried Meat",
                description = "A straightforward cooked meat portion with lots of oil and little elegance.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 70f,
                destroyAtZeroCondition = true,
                weight = 0.5f,
                scaleWeightWithCondition = true,
                value = 10,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(9f, 0.25f);
                    body.Drink(-4.25f);
                    body.happiness += 2f;
                    body.energy += 8f;
                    body.temperature += 0.5f;
                    item.condition -= 0.334f;
                    body.talker.EatGood();

                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.friedmeat.png"));
            ItemRegistry.Register("fruitglaze", new ItemInfo
            {
                fullName = "Fruit Glaze",
                description = "A sticky fruit reduction. Tasty and edible, as a snack! Great for energy and mood.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 120f,
                destroyAtZeroCondition = true,
                weight = 0.2f,
                scaleWeightWithCondition = true,
                value = 10,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(5f, 0.1f);
                    body.Drink(5f);
                    body.talker.EatGood();

                    body.happiness += 4.5f;
                    body.talker.EatGood();
                    body.energy += 13f;
                    body.caffeinated += 15f;
                    body.sicknessAmount += 5f;
                    item.condition -= 0.25f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.fruitglaze.png"));
            ItemRegistry.Register("lumalgaebroth", new ItemInfo
            {
                fullName = "Lumalgae Broth",
                description = "A thin glowing broth with hydration first and nutrition second. You can't quite pretend putting lumalgae over fire makes it a meal...",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 55f,
                destroyAtZeroCondition = true,
                weight = 0.4f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(2.5f, 0.1f);
                    body.Drink(4f);
                    body.happiness -= 3.5f;
                    body.sicknessAmount += 6f;
                    body.talker.EatBad();
                    body.radiationSickness -= 0.25f;
                    body.temperature += 0.25f;
                    body.energy += 2f;
                    item.condition -= 0.5f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.lumalgaebroth.png"));
            ItemRegistry.Register("meatstock", new ItemInfo
            {
                fullName = "Meat Stock",
                description = "A savory stock with deep flavor and some nice substance.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 65f,
                destroyAtZeroCondition = true,
                weight = 0.4f,
                scaleWeightWithCondition = true,
                value = 9,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(5.5f, 0.1f);
                    body.Drink(13.5f);
                    body.happiness += 0.5f;
                    body.talker.EatGood();

                    body.temperature += 0.35f;
                    body.energy += 4f;
                    body.sicknessAmount = Mathf.Max(0f, body.sicknessAmount - 0.15f);
                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.meatstock.png"));
            ItemRegistry.Register("organbroth", new ItemInfo
            {
                fullName = "Organ Broth",
                description = "Better then eating it raw, at the very least...",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 60f,
                destroyAtZeroCondition = true,
                weight = 0.45f,
                scaleWeightWithCondition = true,
                value = 3,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(3.25f, 0.15f);
                    body.Drink(3f);
                    body.happiness += -4.5f;
                    body.talker.EatBad();
                    body.energy += 3f;
                    body.temperature += 0.4f;
                    body.sicknessAmount += 10f;
                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.organbroth.png"));
            ItemRegistry.Register("pileofjunkfood", new ItemInfo
            {
                fullName = "Pile of Junk Food",
                description = "A greasy pile of processed snacks. Comforting, but not exactly healthy.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 160f,
                destroyAtZeroCondition = true,
                weight = 0.7f,
                scaleWeightWithCondition = true,
                value = 22,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(9.5f, 0.25f);
                    body.Drink(-0.5f);
                    body.happiness += 5f;
                    body.talker.EatGood();
                    body.energy += 10f;
                    body.sicknessAmount += 1.25f;
                    item.condition -= 0.2f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.pileofjunkfood.png"));
            ItemRegistry.Register("pilk", new ItemInfo
            {
                fullName = "Pilk",
                description = "An upsetting drink metally, but tasty drink physically. Hydrates well enough, if you can stomach it.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 50f,
                destroyAtZeroCondition = true,
                weight = 0.35f,
                scaleWeightWithCondition = true,
                value = 10,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(1.5f, 0.05f);
                    body.Drink(4f);
                    body.happiness += -1.5f;
                    body.talker.EatBad();
                    body.energy += 5f;
                    body.caffeinated += 28f;
                    body.sicknessAmount += 5f;
                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.pilk.png"));
            ItemRegistry.Register("rosetea", new ItemInfo
            {
                fullName = "Rose Tea",
                description = "A light floral tea. Very hydrating and energizing.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 45f,
                destroyAtZeroCondition = true,
                weight = 0.25f,
                scaleWeightWithCondition = true,
                value = 9,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(0.5f, 0.05f);
                    body.Drink(4.5f);
                    body.happiness += 2f;
                    body.temperature += 0.2f;
                    body.energy += 15f;
                    body.caffeinated += 22f;
                    body.sicknessAmount = Mathf.Max(0f, body.sicknessAmount - 0.6f);
                    item.condition -= 0.25f;
                    body.talker.EatGood();

                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.rosetea.png"));
            ItemRegistry.Register("sauteedmeat", new ItemInfo
            {
                fullName = "Sauteed Meat",
                description = "A properly cooked meat dish. Finally, some good food.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 80f,
                destroyAtZeroCondition = true,
                weight = 0.55f,
                scaleWeightWithCondition = true,
                value = 16,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(12.5f, 0.25f);
                    body.Drink(0.5f);
                    body.happiness += 2.5f;
                    body.talker.EatGood();
                    body.energy += 9f;
                    body.temperature += 0.55f;
                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.sauteedmeat.png"));
            ItemRegistry.Register("searedmushpear", new ItemInfo
            {
                fullName = "Seared Mushpear",
                description = "A seared mushpear with a softer texture and earthy finish. Looks like most of the opiate was cooked out. Yum!",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 55f,
                destroyAtZeroCondition = true,
                weight = 0.45f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(4.5f, 0.1f);
                    body.Drink(2.5f);
                    body.happiness += 1.5f;
                    body.temperature += 0.3f;
                    body.energy += 3f;
                    body.GetComponent<Painkillers>().opiateAmount += 0.5f;
                    body.sicknessAmount += 0.5f;
                    item.condition -= 0.5f;
                    body.talker.EatGood();

                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.searedmushpear.png"));
            ItemRegistry.Register("searedmushtail", new ItemInfo
            {
                fullName = "Seared Mushtail",
                description = "A mushtail, seared to perfection. Seems to have cut most of the sleeping effect.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 55f,
                destroyAtZeroCondition = true,
                weight = 0.45f,
                scaleWeightWithCondition = true,
                value = 4,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(4.25f, -0.1f);
                    body.Drink(1.5f);
                    body.happiness += 1f;
                    body.temperature += 0.25f;
                    body.energy -= 25f;
                    body.sicknessAmount = Mathf.Max(0f, body.sicknessAmount - 0.1f);
                    item.condition -= 0.5f;
                    body.talker.EatGood();

                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.searedmushtail.png"));
            ItemRegistry.Register("shepardspie", new ItemInfo
            {
                fullName = "Shepard's Pie",
                description = "A hearty pie with enough real substance to feel satisfying.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 110f,
                destroyAtZeroCondition = true,
                weight = 0.8f,
                scaleWeightWithCondition = true,
                value = 17,
                tags = "cangetwet",
                rec = new Recognition(5),
                useAction = (body, item) =>
                {
                    body.Eat(15.5f, 0.35f);
                    body.Drink(1f);
                    body.happiness += 3.5f;
                    body.talker.EatGood();
                    body.energy += 8f;
                    body.temperature += 0.7f;
                    body.sicknessAmount = Mathf.Max(0f, body.sicknessAmount - 0.2f);
                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.shepardspie.png"));
            ItemRegistry.Register("stuffedgeigefruitbun", new ItemInfo
            {
                fullName = "Stuffed Geigefruit Bun",
                description = "A filled bun with decent calories and a hint of sweetness.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 95f,
                destroyAtZeroCondition = true,
                weight = 0.65f,
                scaleWeightWithCondition = true,
                value = 12,
                tags = "cangetwet",
                rec = new Recognition(5),
                useAction = (body, item) =>
                {
                    body.Eat(9.25f, 0.25f);
                    body.Drink(1.5f);
                    body.happiness += 3.5f;
                    body.talker.EatGood();
                    body.energy += 7f;
                    body.caffeinated += 10f;
                    body.radiationSickness -= 0.3f;
                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.stuffedgeigefruitbun.png"));
            ItemRegistry.Register("vegetablebroth", new ItemInfo
            {
                fullName = "Vegetable Soup",
                description = "A simple broth that hydrates well. Not bad!",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 55f,
                destroyAtZeroCondition = true,
                weight = 0.4f,
                scaleWeightWithCondition = true,
                value = 9,
                tags = "cangetwet",
                rec = new Recognition(3),
                useAction = (body, item) =>
                {
                    body.Eat(8.25f, 0.1f);
                    body.Drink(14f);
                    body.happiness += 1.5f;
                    body.temperature += 0.25f;
                    body.energy += 2f;
                    body.sicknessAmount = Mathf.Max(0f, body.sicknessAmount - 0.35f);
                    item.condition -= 0.334f;
                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.vegetablebroth.png"));
            ItemRegistry.Register("wastelandsalad", new ItemInfo
            {
                fullName = "Wasteland Salad",
                description = "A mixed salad made from whatever plants you found in the wasteland. Crisp and practical.",
                category = "food",
                usable = true,
                usableOnLimb = false,
                decayMinutes = 45f,
                destroyAtZeroCondition = true,
                weight = 0.45f,
                scaleWeightWithCondition = true,
                value = 12,
                tags = "cangetwet",
                rec = new Recognition(4),
                useAction = (body, item) =>
                {
                    body.Eat(14f, 0.15f);
                    body.Drink(2f);
                    body.happiness += 2f;
                    body.energy += 4f;
                    body.sicknessAmount = Mathf.Max(0f, body.sicknessAmount - 0.5f);
                    body.temperature -= 0.15f;
                    item.condition -= 0.5f;
                    body.talker.EatGood();

                }
            }, AssetLoader.LoadEmbeddedSprite("Sprites.wastelandsalad.png"));
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
            //        body.happiness += 1f;
            //        item.condition -= 1f;
            //    },
            //}, appleSliceSprite);
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
            //        body.happiness += 1f;
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

    }
}

