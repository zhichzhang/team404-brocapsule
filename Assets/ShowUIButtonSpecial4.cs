using System.Collections.Generic;
using UnityEngine;


public class ShowUIButtonSpecial4 : MonoBehaviour
{
    public List<UIInputButtonFadeInOut> uiButtons;
    public LanceInRegion lanceInRegion;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if (lanceInRegion.DetectInteractableLadder())
            {
                foreach (var button in uiButtons)
                {
                    button.Show();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var button in uiButtons)
            {
                button.Hide();
            }
        }
    }
}
