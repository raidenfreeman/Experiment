using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PreparationSurface : PlacementSurface, IInteractibleSurface
{
    public bool TryInteract()
    {
        // If the surface has an item, and we can interact with it
        if (placedItem != null && placedItem is IPreparable)
        {
            var interactibleItem = placedItem as IPreparable;
            interactibleItem.Prepare(Time.deltaTime);
            return true;
        }
        return false;
    }
}
