using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowUIInputButtonWhenInRegion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<UIInputButtonFadeInOut> uiButtons;
    public List<ReferenceLine> referenceLines;
    public bool requireOnSpear = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {



            if (requireOnSpear)
            {
                if (PlayerInfo.instance.player.currentInteractingSpear != null)
                {
                    if (referenceLines.Count != 0)
                    {
                        foreach (var referenceLine in referenceLines)
                        {
                            referenceLine.Show();
                        }
                    }
                    foreach (var button in uiButtons)
                    {
                        button.Show();
                    }
                }
            }
            else
            {
                if (referenceLines.Count != 0)
                {
                    foreach (var referenceLine in referenceLines)
                    {
                        referenceLine.Show();
                    }
                }
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
            if (referenceLines.Count != 0)
            {
                foreach (var referenceLine in referenceLines)
                {
                    referenceLine.Hide();
                }
            }
            foreach (var button in uiButtons)
            {
                button.Hide();
            }
        }
    }

}
