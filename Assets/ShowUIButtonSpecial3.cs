using System.Collections.Generic;
using UnityEngine;


public class ShowUIButtonSpecial3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<UIInputButtonFadeInOut> uiButtons;
    public LancerCreator lancerCreator;
    public LanceInRegion lanceInRegion;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("lance" + lanceInRegion.DetectInteractableLadder());
            if (!lancerCreator.haveSpawned() && !lanceInRegion.DetectInteractableLadder())
            {
                foreach (var button in uiButtons)
                {
                    button.Show();
                }
            }
            if (lanceInRegion.DetectInteractableLadder())
            {
                foreach (var button in uiButtons)
                {
                    button.Hide();
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
