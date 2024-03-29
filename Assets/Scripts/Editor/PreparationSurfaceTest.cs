﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Reflection;

namespace CoreSource
{
    public class PreparationSurfaceTest
    {
        [Test]
        public void TryInteract_NoItemOnSurface_ReturnFalse()
        {
            var surface = new PreparationSurface();
            Assert.IsFalse(surface.TryInteract());
        }
    }

    public class PreparationSurface
    {
        public bool TryInteract()
        {
            return false;
        }
    }
}


namespace UnityBridge
{
    public class PreparationSurfaceTest
    {
        [Test]
        public void TryInteract_NoItemOnSurface_ReturnFalse()
        {
            var surface = new GameObject().AddComponent<PreparationSurface>();
            Assert.IsFalse(surface.TryInteract());
        }

        [Test]
        public void TryInteract_NotInteractibleItemOnSurface_ReturnFalse()
        {
            var surface = new GameObject().AddComponent<PreparationSurface>();
            var pot = new GameObject().AddComponent<Pot>();
            typeof(PreparationSurface)
                .GetProperty("placedItem", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(surface, pot);
            Assert.IsFalse(surface.TryInteract());
        }

        [Test]
        public void TryInteract_NotPreparableItem_ReturnFalse()
        {
            var surface = new GameObject().AddComponent<PreparationSurface>();
            var dish = new GameObject().AddComponent<Dish>();
            typeof(PreparationSurface)
                .GetProperty("placedItem", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(surface, dish);
            Assert.IsFalse(surface.TryInteract());
        }

        [Test]
        public void TryInteract_InteractibleItemOnSurface_ReturnTrue()
        {
            var surface = new GameObject().AddComponent<PreparationSurface>();
            var tomato = new GameObject().AddComponent<FoodIngredient>();
            typeof(PreparationSurface)
                .GetProperty("placedItem", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(surface, tomato);
            Assert.IsTrue(surface.TryInteract());
        }

        [UnityTest]
        public IEnumerator TryInteract_InteractibleItemOnSurface_CompletionIncreased()
        {
            var surface = new GameObject().AddComponent<PreparationSurface>();
            var tomato = new GameObject().AddComponent<FoodIngredient>();
            typeof(PreparationSurface)
                .GetProperty("placedItem", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(surface, tomato);
            var startingPercentage = tomato.completionPercentage;
            yield return null;
            surface.TryInteract();
            var timePrepared = typeof(FoodIngredient)
                .GetField("timeSpentPreparing", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(tomato);
            Assert.AreEqual(Time.deltaTime, timePrepared);
        }
    }
}