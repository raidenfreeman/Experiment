using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PotTest
{

    [Test]
    public void TryAddTomato_NoContents_ReturnTrue()
    {
        var pot = new GameObject().AddComponent<Pot>();
        Assert.AreEqual(pot.Contents.Count, 0);
        Assert.IsTrue(pot.TryAddIngredient(ContentType.tomato));
        Assert.AreEqual(pot.Contents.Count, 1);
        Assert.IsTrue(pot.Contents.ContainsKey(ContentType.tomato));
        var numberOfTomatoesInPot = 0;
        pot.Contents.TryGetValue(ContentType.tomato, out numberOfTomatoesInPot);
        Assert.AreEqual(numberOfTomatoesInPot, 1);
    }

    [Test]
    public void TryAddTomato_TwoTomatoesContained_ReturnTrue()
    {
        var pot = new GameObject().AddComponent<Pot>();
        pot.TryAddIngredient(ContentType.tomato);
        pot.TryAddIngredient(ContentType.tomato);
        Assert.AreEqual(pot.Contents.Count, 1); // there should be 1 kind of ingredient
        Assert.IsTrue(pot.TryAddIngredient(ContentType.tomato)); // tomato should be able to be added
        Assert.AreEqual(pot.Contents.Count, 1); // there should still be be 1 ingredient
        Assert.IsTrue(pot.Contents.ContainsKey(ContentType.tomato));
        var numberOfTomatoesInPot = 0;
        pot.Contents.TryGetValue(ContentType.tomato, out numberOfTomatoesInPot);
        Assert.AreEqual(numberOfTomatoesInPot, 3); // there should be 3 tomatoes contained
    }

    [Test]
    public void TryAddOnion_TwoTomatoesContained_ReturnFalseBecauseThereIsNoSuchRecipe()
    {
        var pot = new GameObject().AddComponent<Pot>();
        pot.TryAddIngredient(ContentType.tomato);
        pot.TryAddIngredient(ContentType.tomato);
        Assert.IsFalse(pot.Contents.ContainsKey(ContentType.onion));
        Assert.AreEqual(pot.Contents.Count, 1); // there should be 1 kind of ingredient
        Assert.IsFalse(pot.TryAddIngredient(ContentType.onion)); // tomato should be able to be added
        Assert.AreEqual(pot.Contents.Count, 1); // there should still be be 1 ingredient
        Assert.IsFalse(pot.Contents.ContainsKey(ContentType.onion));
    }

}
