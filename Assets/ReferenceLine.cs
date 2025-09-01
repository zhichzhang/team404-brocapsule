using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ReferenceLine : MonoBehaviour
{
    private List<Light2D> lights;
    private bool isActive = false;
    public float lightChangeTime = 0.05f;
    public float lightActivationDelay = 0.05f; // Delay between each light activation
    public float finalIntensity = 7f;
    public float finalVolumetricIntensity = 0.1f;
    private float activationTimer = 0f;
    private int currentLightIndex = 0;

    void Start()
    {
        // Get all Light2D components in the children of this GameObject
        lights = new List<Light2D>(GetComponentsInChildren<Light2D>());
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    Show();
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    Hide();
        //}

        // Handle light activation/deactivation
        if (isActive)
        {
            if (currentLightIndex < lights.Count)
            {
                activationTimer += Time.deltaTime;
                if (activationTimer >= lightActivationDelay)
                {
                    activationTimer = 0f;
                    currentLightIndex++;
                }
            }
        }
        else
        {
            if (currentLightIndex > 0)
            {
                activationTimer += Time.deltaTime;
                if (activationTimer >= lightActivationDelay)
                {
                    activationTimer = 0f;
                    currentLightIndex--;
                }
            }
        }

        // Update light intensities
        for (int i = 0; i < lights.Count; i++)
        {
            if (i < currentLightIndex)
            {
                lights[i].intensity = Mathf.Lerp(lights[i].intensity, finalIntensity, Time.deltaTime / lightChangeTime);
                lights[i].volumeIntensity = Mathf.Lerp(lights[i].volumeIntensity, finalVolumetricIntensity, Time.deltaTime / lightChangeTime);
            }
            else
            {
                lights[i].intensity = Mathf.Lerp(lights[i].intensity, 0, Time.deltaTime / lightChangeTime);
                lights[i].volumeIntensity = Mathf.Lerp(lights[i].volumeIntensity, 0, Time.deltaTime / lightChangeTime);
            }
        }
    }

    public void Show()
    {
        isActive = true;
    }

    public void Hide()
    {
        isActive = false;
    }
}