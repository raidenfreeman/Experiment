using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Reflection;

public class PreparationSurfaceTest
{
    [Test]
    public void TryInteract_NoItemOnSurface_ReturnFalse()
    {
        var surface = new GameObject().AddComponent<PreparationSurface>();
        var placementSurface = new GameObject().AddComponent<PlacementSurface>();
        typeof(PreparationSurface).GetField("placementSurface", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(surface, placementSurface);
        Assert.IsFalse(surface.TryInteract());
    }

    [Test]
    public void TryInteract_NotInteractibleItemOnSurface_ReturnFalse()
    {
        var surface = new GameObject().AddComponent<PreparationSurface>();
        var placementSurface = new GameObject().AddComponent<PlacementSurface>();
        var pot = new GameObject().AddComponent<Pot>();
        typeof(PlacementSurface)
            .GetProperty("item", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(placementSurface, pot);
        typeof(PreparationSurface)
            .GetField("placementSurface", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(surface, placementSurface);
        Assert.IsFalse(surface.TryInteract());
    }

    [Test]
    public void TryInteract_NotPreparableItem_ReturnFalse()
    {
        var surface = new GameObject().AddComponent<PreparationSurface>();
        var placementSurface = new GameObject().AddComponent<PlacementSurface>();
        var dish = new GameObject().AddComponent<Dish>();
        typeof(PlacementSurface)
            .GetProperty("item", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(placementSurface, dish);
        typeof(PreparationSurface)
            .GetField("placementSurface", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(surface, placementSurface);
        Assert.IsFalse(surface.TryInteract());
    }

    [Test]
    public void TryInteract_InteractibleItemOnSurface_ReturnTrue()
    {
        var surface = new GameObject().AddComponent<PreparationSurface>();
        var placementSurface = new GameObject().AddComponent<PlacementSurface>();
        var tomato = new GameObject().AddComponent<FoodIngredient>();
        typeof(PlacementSurface)
            .GetProperty("item", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(placementSurface, tomato);
        typeof(PreparationSurface)
            .GetField("placementSurface", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(surface, placementSurface);
        Assert.IsTrue(surface.TryInteract());
    }

    [UnityTest]
    public IEnumerator TryInteract_InteractibleItemOnSurface_CompletionIncreased()
    {
        var surface = new GameObject().AddComponent<PreparationSurface>();
        var placementSurface = new GameObject().AddComponent<PlacementSurface>();
        var tomato = new GameObject().AddComponent<FoodIngredient>();
        typeof(PlacementSurface)
            .GetProperty("item", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(placementSurface, tomato);
        typeof(PreparationSurface)
            .GetField("placementSurface", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(surface, placementSurface);
        var startingPercentage = tomato.completionPercentage;
        yield return null;
        surface.TryInteract();
        var timePrepared = typeof(FoodIngredient)
            .GetField("timeSpentPreparing", BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(tomato);
        Assert.AreEqual(Time.deltaTime, timePrepared);
    }
}
