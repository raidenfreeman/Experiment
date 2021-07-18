using UnityEngine;
using UnityEngine.UI;

public class ConnectionUI : MonoBehaviour
{

    public Text OutputText;
    public InputField IPInput;

    public void OnConnectClick()
    {
        WriteToText("Initiating connection to '"+IPInput.text+"'...");
    }
    public void OnCancelClick()
    {
        WriteToText("Cancelling...");
        WriteToText("Cancelled connection");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public ScrollRect scrollRect;

    private void WriteToText(string text)
    {
        OutputText.text += "\n"+text;
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }
}
