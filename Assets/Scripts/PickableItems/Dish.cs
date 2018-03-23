using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Dish : MonoBehaviour, IPickableItem, IWashable
{
    /// <summary>
    /// Occurs when the food is prepared
    /// </summary>
    public event EventHandler WashedDishEvent;

    /// <summary>
    /// The time needed to complete washing the item
    /// </summary>
    public readonly float timeToWash;

    /// <summary>
    /// How much % is completed
    /// </summary>
    public int completionPercentage
    {
        get
        {
            return (int)(timeSpentWashing / timeToWash * 100f);
        }
    }

    /// <summary>
    /// The amount of time invested in washing this item
    /// </summary>
    private float timeSpentWashing = 0;

    public void Drop()
    {
        throw new NotImplementedException();
    }

    public void PickUp()
    {
        throw new NotImplementedException();
    }

    public void Place(PlacementSurface surface)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Called when the item is washed
    /// </summary>
    void WashingComplete()
    {
        WashedDishEvent?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// Increments washing by time
    /// </summary>
    /// <param name="timeToAdd">The time in milliseconds to add</param>
    /// <returns>The percentage of preparation</returns>
    public int Wash(float timeToAdd)
    {
        timeSpentWashing += timeToAdd;
        if (timeSpentWashing >= timeToWash)
        {
            timeSpentWashing = timeToWash;
            WashingComplete();
        }
        return completionPercentage;
    }
}
