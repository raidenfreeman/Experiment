using UnityEngine;
/// <summary>
/// The data container of every item
/// </summary>
public interface IPickableItem
{
    Transform LeftHandAnchor { get; }

    Transform RightHandAnchor { get; }

    Transform PlacementAnchor { get; }
}