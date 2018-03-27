using System;
using UnityEngine;

public class PlacementSurface : MonoBehaviour
{
    /// <summary>
    /// This item will be placed on the surface OnAwake(). MUST HAVE IPickableItem component.
    /// </summary>
    /// <remarks>This exists, because you can't set through the inspector,
    /// fields/properties of interface types (like placedItem).</remarks>
    public GameObject pickableItemPlacedOnAwake;

    /// <summary>
    /// The transform point to put the placed item
    /// </summary>
    [SerializeField]
    protected Transform placementAnchor;

    /// <summary>
    /// The item placed on the surface
    /// </summary>
    [SerializeField]
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
            return TryCombineItems(item,placedItem);
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

    private bool TryCombineItems(IPickableItem item, IPickableItem placedItem)
    {
        throw new NotImplementedException();
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
        }
    }

    private void Awake()
    {
        if (pickableItemPlacedOnAwake != null)
        {
            var item = pickableItemPlacedOnAwake.GetComponent<IPickableItem>();
            if (item == null)
            {
                Debug.LogError(nameof(pickableItemPlacedOnAwake) + " does not contain a component that implements " + nameof(IPickableItem));
                return;
            }
            else
            {
                pickableItemPlacedOnAwake.transform.parent = placementAnchor;
                pickableItemPlacedOnAwake.transform.localPosition = Vector3.zero;
                pickableItemPlacedOnAwake.transform.localRotation = Quaternion.identity;
                placedItem = item;
            }
        }
    }
}