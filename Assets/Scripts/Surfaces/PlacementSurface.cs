using System.Linq;
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
            return TryCombineItems(item, placedItem);
        }
        var itemTransform = (item as MonoBehaviour).transform;
        PlaceItemOnSurface(item, itemTransform);
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

    private bool TryCombineItems(IPickableItem heldItem, IPickableItem placedItem)
    {
        var item1MonoBehaviour = (heldItem as MonoBehaviour);
        if (item1MonoBehaviour == null)
        {
            return false;
        }
        var item2MonoBehaviour = (placedItem as MonoBehaviour);
        if (item2MonoBehaviour == null)
        {
            return false;
        }
        // Set the held item as the base
        var comboBase = item1MonoBehaviour.GetComponent<CombinationBase>();
        var itemToAdd = item2MonoBehaviour.GetComponent<FoodIngredient>();
        if (comboBase == null || itemToAdd == null)
        {
            // Set the placed item as the base
            comboBase = item2MonoBehaviour.GetComponent<CombinationBase>();
            itemToAdd = item1MonoBehaviour.GetComponent<FoodIngredient>();
            if (comboBase == null || itemToAdd == null)
            {
                //if no item is a combo base, they can't be combined
                return false;
            }
        }

        var gg = comboBase.RecipeList.Any(x => x.Contains(itemToAdd.IngredientType));
        if (gg)
        {
            comboBase.AddItem(itemToAdd);
        }
        //TODO: this should get the enum from the item
        //TODO: Check if it's prepared
        //TODO: actually check recipes, because this is seisse.
        return gg;
    }

    void foo(CombinationBase b, IPickableItem i)
    {

    }
    void foo(IPickableItem i, CombinationBase b)
    {

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
                PlaceItemOnSurface(item, pickableItemPlacedOnAwake.transform);
            }
        }
    }

    private void PlaceItemOnSurface(IPickableItem item, Transform itemTransform)
    {
        itemTransform.parent = placementAnchor;
        itemTransform.localPosition = Vector3.zero;
        itemTransform.localPosition = -item.PlacementAnchor.localPosition;
        itemTransform.localRotation = Quaternion.identity;
        placedItem = item;
    }
}