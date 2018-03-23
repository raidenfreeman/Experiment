using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// A pool-stack wrapper
/// </summary>
public class Dispenser : PlacementSurface
{
    [SerializeField]
    private GameObject itemToDispense;

    [SerializeField]
    private int initialPoolCount = 15;

    private Stack<GameObject> itemPool = new Stack<GameObject>();

    public override IPickableItem TryPickUpItem()
    {
        if(base.placedItem == null)
        {
            DispenseItem();
        }
        return base.TryPickUpItem();
    }

    /// <summary>
    /// Get an item from the pool, and place it at anchor
    /// </summary>
    /// <returns>The reference to the placed object</returns>
    public GameObject DispenseItem()
    {
        // If there is already a dispensed item, return that one
        //if (itemPlacementAnchor?.childCount > 0)
        //{
        //    return itemPlacementAnchor.GetChild(0).gameObject;
        //}

        // if the pool is empty
        if (itemPool.Count <= 0)
        {
            // fill it
            PopulatePool();
        }
        // Give the top item of the stack
        var item = itemPool.Pop();
        // Enable it
        InitializeItem(item);
        return item;
    }

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        for (int i = 0; i < initialPoolCount; i++)
        {
            // Create a new item, as a child of the anchor
            var item = Instantiate(itemToDispense);
            item.gameObject.SetActive(false);
            itemPool.Push(item);
        }
    }

    private void InitializeItem(GameObject item)
    {
        var itemRigidbody = item.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
        {
            itemRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        item.gameObject.SetActive(true);
    }
}
