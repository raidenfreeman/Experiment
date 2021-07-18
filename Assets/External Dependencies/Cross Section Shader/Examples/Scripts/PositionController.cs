using UnityEngine;
using UnityEngine.UI;

public class PositionController : MonoBehaviour {
    public Slider XPos;
    public Slider YPos;
    public Slider ZPos;
    //public Transform ControlledObject;
    public void UpdateObjectPosition(Transform ControlledObject)
    {
        Vector3 newPosition = new Vector3(XPos?XPos.value:0, YPos?YPos.value:0, ZPos?ZPos.value:0);
        ControlledObject.localPosition = newPosition;
    }
}
