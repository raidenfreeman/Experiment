﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlacementSurface))]
public class PreparationSurface : MonoBehaviour, IInteractibleSurface
{

    PlacementSurface placementSurface;

    IPickableItem itemOnSurface
    {
        get
        {
            return placementSurface.placedItem;
        }
    }

    public bool TryInteract()
    {
        // If the surface has an item, and we can interact with it
        if (itemOnSurface != null && itemOnSurface is IPreparable)
        {
            var interactibleItem = itemOnSurface as IPreparable;
            interactibleItem.Prepare(Time.deltaTime);
            return true;
        }
        return false;
    }

    // Use this for initialization
    void Awake()
    {
        placementSurface = placementSurface ?? GetComponent<PlacementSurface>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
