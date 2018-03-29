using System;
using System.Collections.Generic;
using UnityEngine;

public enum NonBaseIngredients
{
    tomato,
    patty,
    cheese,
    onion,
    mushroom
}

public class CombinationBase : MonoBehaviour
{
    public NonBaseIngredients[][] RecipeList = new NonBaseIngredients[][] {
        new NonBaseIngredients[] {
            NonBaseIngredients.tomato,
            NonBaseIngredients.tomato,
            NonBaseIngredients.tomato
        },
        new NonBaseIngredients[]{
            NonBaseIngredients.onion,
            NonBaseIngredients.onion,
            NonBaseIngredients.onion
        },
        new NonBaseIngredients[]{
            NonBaseIngredients.mushroom,
            NonBaseIngredients.mushroom,
            NonBaseIngredients.mushroom
        }
    };

    public void AddItem(FoodIngredient itemToAdd)
    {
        throw new NotImplementedException();
    }
}