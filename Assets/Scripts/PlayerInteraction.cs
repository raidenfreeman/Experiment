using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts;
using InControl;

public class PlayerInteraction : MonoBehaviour
{

    //public ReactiveProperty<IPickableItem> heldItem = new ReactiveProperty<IPickableItem>(new NoItem());

    [SerializeField]
    private float reachRadius;

    private Vector3 forwardReach
    {
        get
        {
            return transform.forward * reachRadius;
        }
    }

    void Start()
    {
        //heldItem.Value = new NoItem();
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

        if (InputManager.ActiveDevice?.Action3?.IsPressed ?? false)
        {
            var collider = GetHitCollider();
            //var interactible = collider.gameObject.GetComponent<Surface>();


            //var angles = collidersHit.Select(x => Vector3.Angle(this.forwardReach, x.transform.position - this.transform.position)).ToArray();
            //Debug.Log("Object Interacted with:" + colliderSelected.transform.parent);

        }

        if (InputManager.ActiveDevice?.Action1?.IsPressed ?? false)
        {
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            var collider = GetHitCollider();

            //var interactible = colliderSelected.gameObject.GetComponent<PickableItem>();

            //var angles = collidersHit.Select(x => Vector3.Angle(this.forwardReach, x.transform.position - this.transform.position)).ToArray();
            //Debug.Log("Object Interacted with:" + colliderSelected.transform.parent);

        }
    }

    private Collider GetHitCollider()
    {
        var collidersHit = Physics.OverlapSphere(transform.position, reachRadius, 1 << 8);
        collidersDebug = collidersHit;
        var colliderSelected = collidersHit.SelectItemBy(
            (a, b) =>
                Vector3.Angle(this.forwardReach, a.transform.position - this.transform.position) <
                Vector3.Angle(this.forwardReach, b.transform.position - this.transform.position)
        );
        selectedColliderDebug = colliderSelected;
        return colliderSelected;
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

public static class ExtMethods
{
    public static T SelectItemBy<T>(this IEnumerable<T> enumerable, Func<T, T, bool> predicate)
    {
        var count = enumerable.Count();
        if (count == 1)
        {
            return enumerable.First();
        }
        if (count == 0)
        {
            return default(T);
        }
        return enumerable.Aggregate((max, item) => predicate(item, max) ? item : max);
    }
}
