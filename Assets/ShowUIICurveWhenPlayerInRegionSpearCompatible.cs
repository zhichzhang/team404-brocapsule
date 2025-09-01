using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ShowUIICurveWhenPlayerInRegionSpearCompatible : MonoBehaviour
{
    public List<UIInputButtonFadeInOut> uiButtons;
    public List<ReferenceLine> referenceLines;
    public bool requireOnSpear = false;
    private bool started = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (started) return;
        if (collision.CompareTag("Player"))
        {
            if (PlayerInfo.instance.player.currentInteractingSpear != null)
            {
                started = true;
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
            
            StartCoroutine(HideUIAfterDelay());
        }
    }

    private IEnumerator HideUIAfterDelay()
    {
        yield return new WaitForSeconds(0.6f);

        started = false;
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
