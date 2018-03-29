using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts;
using InControl;



// Responsible for handling picking up and droping pickable items.
// Should not hold any reference to the item (you can get it from the anchor if needed)
public class PlayerPickup : MonoBehaviour
{

    //public ReactiveProperty<IPickableItem> heldItem = new ReactiveProperty<IPickableItem>(new NoItem());

    [SerializeField]
    private float reachRadius;

    [SerializeField]
    private Transform holdingAnchor;

    [SerializeField]
    private Transform leftHand;

    [SerializeField]
    private Transform rightHand;

    // TODO: Replace with animation on the final model
    private readonly Vector3 leftHandOriginalPosition = new Vector3(-0.6f, 0, 0.3f);
    private readonly Vector3 rightHandOriginalPosition = new Vector3(0.6f, 0, 0.3f);

    private bool isHoldingItem { get { return holdingAnchor.childCount > 0; } }

    private Transform heldItem { get { return holdingAnchor.GetChild(0); } }

    private Vector3 forwardReach
    {
        get
        {
            return transform.forward * reachRadius;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + forwardReach);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, reachRadius);
        if (collidersDebug != null)
        {
            Gizmos.color = Color.blue;
            foreach (var col in collidersDebug)
            {
                if (col != selectedColliderDebug)
                {
                    Gizmos.DrawLine(transform.position, col.transform.position);
                }
                else
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(transform.position, col.transform.position);
                    Gizmos.color = Color.blue;
                }
            }
        }
    }

    Collider[] collidersDebug; //for debug
    Collider selectedColliderDebug;
    void Update()
    {
        //InputManager.OnDeviceAttached


        var a1 = InputManager.ActiveDevice;
        var b1 = a1.Action3;
        var c1 = b1.IsPressed;
        //var d
        //var e

        if ((InputManager.ActiveDevice?.Action3?.IsPressed ?? false) && !isHoldingItem)
        {
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            GetFacedObject()?.GetComponent<IInteractibleSurface>()?.TryInteract();
            
            //var interactible = colliderSelected.gameObject.GetComponent<PickableItem>();

            //var angles = collidersHit.Select(x => Vector3.Angle(this.forwardReach, x.transform.position - this.transform.position)).ToArray();
            //Debug.Log("Object Interacted with:" + colliderSelected.transform.parent);

        }
        if (InputManager.ActiveDevice?.Action1?.WasPressed ?? false)
        {
            if (isHoldingItem)
            {
                DropItem();
            }
            else
            {
                PickUpItem();
            }
        }
    }

    private void DropItem()
    {
        var heldItem = this.heldItem?.GetComponent<IPickableItem>();
        if (heldItem == null)
        {
            return;
        }
        var surface = GetFacedObject()?.GetComponent<PlacementSurface>();
        //TODO: Iterate over sorted list of faced surfaces.
        if (surface != null && surface.TryPlaceItem(heldItem))
        {
            leftHand.localPosition = leftHandOriginalPosition;
            rightHand.localPosition = rightHandOriginalPosition;
        }
    }

    private void PickUpItem()
    {
        var surface = GetFacedObject()?.GetComponent<PlacementSurface>();
        if (surface != null)
        {
            var item = surface.TryPickUpItem();
            if (item != null)
            {
                var itemTransform = (item as MonoBehaviour).transform;
                itemTransform.parent = holdingAnchor;
                itemTransform.GetComponent<Rigidbody>().isKinematic = true;
                itemTransform.GetComponent<Collider>().enabled = false;
                itemTransform.localPosition = Vector3.zero;
                itemTransform.localRotation = Quaternion.identity;

                leftHand.localPosition = transform.InverseTransformPoint(item.LeftHandAnchor.position);
                rightHand.localPosition = transform.InverseTransformPoint(item.RightHandAnchor.position);
            }
                //TODO: If it fails, try the next closest surface
                //getfacedobject should be replaced by a function that orders surfaces by angular proximity in a list
        }
    }

    /// <summary>
    /// Gets the game object, in range of the player, prefering the one he faces
    /// </summary>
    /// <returns>A game object with a collider in the Interactibles layer</returns>
    private GameObject GetFacedObject()
    {
        var collidersHit = Physics.OverlapSphere(transform.position, reachRadius, 1 << 8);
        collidersDebug = collidersHit;
        var colliderSelected = collidersHit.SelectItemBy(
            (a, b) =>
                Vector3.Angle(this.forwardReach, a.transform.position - this.transform.position) <
                Vector3.Angle(this.forwardReach, b.transform.position - this.transform.position)
        );
        selectedColliderDebug = colliderSelected;
        return colliderSelected?.gameObject;
    }

    /*Collider SelectAlignedCollider(Collider[] colliders)
    {
        //if (colliders.Length <= 1)
        //{
        //    return colliders
        //}
        //return
    }*/
}