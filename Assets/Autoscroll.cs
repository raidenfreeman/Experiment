using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Autoscroll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public ScrollRect scrollRect;
	
	// Update is called once per frame
	void Update () {
        Canvas.ForceUpdateCanvases();

        //item.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
        //item.GetComponent<ContentSizeFitter>().SetLayoutVertical();

        //scrollRect.content.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
        //scrollRect.content.GetComponent<ContentSizeFitter>().SetLayoutVertical();

        scrollRect.verticalNormalizedPosition = 0;
    }
}
