using UnityEngine;

public class PlacementSurface : MonoBehaviour
{
    /// <summary>
    /// The transform point to put the placed item
    /// </summary>
    [SerializeField]
    protected Transform placementAnchor;

    /// <summary>
    /// The item placed on the surface
    /// </summary>
    public IPickableItem placedItem { get; protected set; }

    /// <summary>
    /// Try to place an item on the surface
    /// </summary>
    /// <param name="item">The MonoBehaviour of the item to place. Must have Rigidbody attached.</param>
    /// <returns>True if successful, false otherwise</returns>
    public virtual bool TryPlaceItem(IPickableItem item)
    {
        if (placedItem != null)
        {
            return false;
        }
        placedItem = item;
        var itemTransform = (item as MonoBehaviour).transform;
        itemTransform.parent = placementAnchor;
        itemTransform.localPosition = Vector3.zero;
        itemTransform.localRotation = Quaternion.identity;
        var itemRigidbody = itemTransform.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
        {
            itemRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            Debug.LogWarning("No rigidbody attached to IPickableItem: " + placedItem.ToString());
        }
        return true;
    }

    /// <summary>
    /// Try to get the placed item
    /// </summary>
    /// <returns>The item if sucessful, null otherwise</returns>
    public virtual IPickableItem TryPickUpItem()
    {

        if (placedItem == null)
        {
            return null;
        }
        else
        {
            var itemToReturn = placedItem;
            placedItem = null;
            return itemToReturn;
            //var itemTransform = (placedItem as MonoBehaviour).transform;
            //itemTransform.parent = newAnchor;
            //itemTransform.localPosition = Vector3.zero;
            //itemTransform.localRotation = Quaternion.identity;
            //var itemRigidbody = itemTransform.GetComponent<Rigidbody>();
            //if (itemRigidbody != null)
            //{
            //    itemRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            //}
            //else
            //{
            //    Debug.LogWarning("No rigidbody attached to IPickableItem: " + placedItem.ToString());
            //}
            //return placedItem;
        }
    }
}