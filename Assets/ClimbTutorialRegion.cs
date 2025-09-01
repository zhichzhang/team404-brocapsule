using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ClimbTutorialRegion : MonoBehaviour
{
    public DeflectTutorialUI part1;
    public DeflectTutorialUI part2;
    public DeflectTutorialUI part3;
    public DeflectTutorialUI part4;
    public float timeToWait = 2f;
    public float exitTolerance = 10f; // New public variable for tolerance
    private bool tutorialDone = false;
    private Coroutine hideCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!tutorialDone)
            {
                tutorialDone = true;
                StartCoroutine(StartTutorial());
            }
            else if (hideCoroutine != null)
            {
                // Restart the hiding coroutine if the player re-enters and it is still running
                StopCoroutine(hideCoroutine);
            }
        }
    }

    private IEnumerator StartTutorial()
    {
        // Disable player inputs
        PlayerInput.instance.DisableGamePlayInputs();

        // switch camera to a fixed camera by calling the following line
        CameraSwitcher.instance.SwitchCamera("focusClimb");

        yield return new WaitForSeconds(timeToWait);

        // Show all UI buttons
        part1.Show();

        // Wait for timeToWait seconds, then show part1
        yield return new WaitForSeconds(timeToWait);
        part2.Show();

        // Wait for timeToWait seconds, then show part2
        yield return new WaitForSeconds(timeToWait);
        part3.Show();

        // Wait for timeToWait seconds, then show part3
        yield return new WaitForSeconds(timeToWait);
        part4.Show();

        // Wait for timeToWait seconds, then enable player inputs
        yield return new WaitForSeconds(timeToWait);

        CameraSwitcher.instance.SwitchFromFixedtoFollowPlayer();

        PlayerInput.instance.EnableGamePlayInputs();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && tutorialDone)
        {
            // Start a coroutine to hide all parts after the tolerance period
            hideCoroutine = StartCoroutine(HideAfterTolerance());
        }
    }

    private IEnumerator HideAfterTolerance()
    {
        // Wait for the exit tolerance duration
        yield return new WaitForSeconds(exitTolerance);

        // Hide all UI buttons and parts
        part1.Hide();
        part2.Hide();
        part3.Hide();
        part4.Hide();
    }
}
