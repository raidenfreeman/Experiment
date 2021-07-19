using UnityEngine;
using UnityEngine.UI;

public class ConnectionUI : MonoBehaviour
{

    public Text outputText;
    public InputField ipInput;

    public void OnConnectClick()
    {
        WriteToText("Initiating connection to '"+ipInput.text+"'...");
    }
    public void OnCancelClick()
    {
        WriteToText("Cancelling...");
        WriteToText("Cancelled connection");
    }

    public ScrollRect scrollRect;

    private void WriteToText(string text)
    {
        outputText.text += "\n"+text;
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }
}
