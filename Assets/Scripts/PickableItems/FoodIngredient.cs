using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FoodIngredient : MonoBehaviour, IPickableItem, IPreparable
{
    /// <summary>
    /// Occurs when the food is prepared
    /// </summary>
    public event EventHandler PreparedFood;

    /// <summary>
    /// The time needed to complete preparing the item
    /// </summary>
    public readonly float timeToPrepare = 5;

    /// <summary>
    /// How much % is completed
    /// </summary>
    public int completionPercentage
    {
        get
        {
            return (int)(timeSpentPreparing / timeToPrepare * 100f);
        }
    }

    /// <summary>
    /// The amount of time invested in preparing this item
    /// </summary>
    private float timeSpentPreparing = 0;

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
        if (surface.GetComponent<SinkSurface>())
        {
            throw new ArgumentException("Can't place food in Sink", nameof(surface));
        }
        throw new NotImplementedException();
    }

    /// <summary>
    /// Increments preparation by time
    /// </summary>
    /// <param name="timeToAdd">The time in milliseconds to add</param>
    /// <returns>The percentage of preparation</returns>
    public int Prepare(float timeToAdd)
    {
        if (timeToAdd > 0)
        {
            sb.transform.parent.gameObject.SetActive(true);
        }
        timeSpentPreparing += timeToAdd;
        if (timeSpentPreparing >= timeToPrepare)
        {
            timeSpentPreparing = timeToPrepare;
            PreparationComplete();
        }
        sb?.UpdateBar(completionPercentage, 100);
        return completionPercentage;
    }
    public SimpleHealthBar sb;

    /// <summary>
    /// Called when the item is prepared
    /// </summary>
    void PreparationComplete()
    {
        PreparedFood?.Invoke(this, new EventArgs());
    }
}
