using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectTutorialTriggerRegion : MonoBehaviour
{
    public List<UIInputButtonFadeInOut> uiButtons;
    public DeflectTutorialUI part1;
    public DeflectTutorialUI part2;
    public DeflectTutorialUI part3;
    public float timeToWait = 1f;
    private bool tutorialDone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !tutorialDone)
        {
            tutorialDone = true;
            StartCoroutine(StartTutorial());
        }
    }

    private IEnumerator StartTutorial()
    {
        // Disable player inputs
        PlayerInput.instance.DisableGamePlayInputs();

        // Show all UI buttons
        foreach (var button in uiButtons)
        {
            button.Show();
        }

        // Wait for timeToWait seconds, then show part1
        yield return new WaitForSeconds(timeToWait);
        part1.Show();

        // Wait for timeToWait seconds, then show part2
        yield return new WaitForSeconds(timeToWait);
        part2.Show();

        // Wait for timeToWait seconds, then show part3
        yield return new WaitForSeconds(timeToWait);
        part3.Show();

        // Wait for timeToWait seconds, then enable player inputs
        yield return new WaitForSeconds(timeToWait);
        PlayerInput.instance.EnableGamePlayInputs();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && tutorialDone)
        {
            // Hide all UI buttons and parts
            foreach (var button in uiButtons)
            {
                button.Hide();
            }
            part1.Hide();
            part2.Hide();
            part3.Hide();
        }
    }
}
