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
    public class CustomItemPatch
    {
        //Sprite appleSprite = AssetLoader.LoadEmbeddedSprite("Sprites.Apple.png");

        //public void Awake()
        //{
        //    CustomItems();
        //}

        //public void CustomItems()
        //{
        //    ItemRegistry.Register("apple", new ItemInfo
        //    {
        //        fullName = "Apple",
        //        description = "A red apple, sweet and healthy to eat",
        //        category = "food",
        //        weight = 0.5f,
        //        value = 2,
        //        usable = true,
        //        decayMinutes = 30f,
        //        scaleWeightWithCondition = true,
        //        tags = "fruit" + "cangetwet",
        //        rec = new Recognition(4),
        //        useAction = (body, item) =>
        //        {
        //            body.Eat(5f, 2);
        //            body.Drink(1f);
        //            body.happiness += 0.75f;
        //            item.condition -= 1f;
        //            //Sound.Play("eatCrunch", body.transform.position);
        //        },
        //        qualities = new List<CraftingQuality>
        //        {
        //            new CraftingQuality("produce")
        //        },
        //    }, appleSprite);
        //}
    }
}
