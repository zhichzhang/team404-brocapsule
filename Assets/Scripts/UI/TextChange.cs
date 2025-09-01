using UnityEngine;

public class TextChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMPro.TextMeshProUGUI text;
    public string newText;
    public string originalText;
    private bool isNew = false;
    public void ChangeToNew()
    {
        text.text = newText;
    }
    public void ChangeToOriginal()
    {
        text.text = originalText;
    }

    public void ToggleText()
    {
        Debug.Log("Toggling text");
        if (isNew)
        {
            Debug.Log("Changing to original");
            ChangeToOriginal();
            isNew = false;
        }
        else
        {
            Debug.Log("Changing to new");
            ChangeToNew();
            isNew = true;
        }
    }
}
