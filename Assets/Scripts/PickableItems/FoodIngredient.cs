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
    /// Set in editor, the point where to place the right hand
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    [SerializeField]
    private Transform rightHandAnchor;



    /// <summary>
    /// Set in editor, the point where to place the left hand
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    public Transform LeftHandAnchor
    {
        get
        {
            return leftHandAnchor;
        }
    }

    /// <summary>
    /// Set in editor, the point where to place the left hand
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    [SerializeField]
    private Transform leftHandAnchor;
    /// <summary>
    /// Set in editor, the point where to place the right hand
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    public Transform RightHandAnchor
    {
        get
        {
            return rightHandAnchor;
        }
    }

    /// <summary>
    /// Set in editor, used to position on surfaces
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    [SerializeField]
    private Transform placementAnchor;
    /// <summary>
    /// Set in editor, used to position on surfaces
    /// </summary>
    /// <remarks>
    /// Don't modify, should be readonly but stupid editor can't handle it
    /// </remarks>
    public Transform PlacementAnchor
    {
        get
        {
            return placementAnchor;
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
