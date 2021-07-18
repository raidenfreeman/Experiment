using UnityEngine;

public class StoveSurface : PlacementSurface {

	void Update () {
        var cookableItem = placedItem as ICookable;
		if(cookableItem != null)
        {
            cookableItem.Cook(Time.deltaTime);
        }
	}
}
