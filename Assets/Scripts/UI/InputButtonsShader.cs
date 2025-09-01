using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonsShader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Color defaultColor;
    [Header("W")]
    public List<Image> ImagesToShadeWhenWPressed;
    public Color ColorWhenWPressed;
    [Header("A")]
    public List<Image> ImagesToShadeWhenAPressed;
    public Color ColorWhenAPressed;
    [Header("S")]
    public List<Image> ImagesToShadeWhenSPressed;
    public Color ColorWhenSPressed;
    [Header("D")]
    public List<Image> ImagesToShadeWhenDPressed;
    public Color ColorWhenDPressed;
    [Header("E")]
    public List<Image> ImagesToShadeWhenEPressed;
    public Color ColorWhenEPressed;
    [Header("Space")]
    public List<Image> ImagesToShadeWhenSpacePressed;
    public Color ColorWhenSpacePressed;
    [Header("J")]
    public List<Image> ImagesToShadeWhenJPressed;
    public Color ColorWhenJPressed;
    [Header("K")]
    public List<Image> ImagesToShadeWhenKPressed;
    public Color ColorWhenKPressed;
    [Header("L")]
    public List<Image> ImagesToShadeWhenLPressed;
    public Color ColorWhenLPressed;
    

    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = PlayerInput.instance;
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInput.Xinput > 0)
        {
            foreach (Image image in ImagesToShadeWhenDPressed)
            {
                image.color = ColorWhenDPressed;
            }
        }
        else
        {
            foreach (Image image in ImagesToShadeWhenDPressed)
            {
                image.color = defaultColor;
            }
        }

        if (playerInput.Xinput < 0)
        {
            foreach (Image image in ImagesToShadeWhenAPressed)
            {
                image.color = ColorWhenAPressed;
            }
        }
        else
        {
            foreach (Image image in ImagesToShadeWhenAPressed)
            {
                image.color = defaultColor;

            }
        }

        if (playerInput.Yinput > 0)
        {
            foreach (Image image in ImagesToShadeWhenWPressed)
            {
                image.color = ColorWhenWPressed;
            }
        }
        else
        {
            foreach (Image image in ImagesToShadeWhenWPressed)
            {
                image.color = defaultColor;
            }
        }

        if (playerInput.Yinput < 0)
        {
            foreach (Image image in ImagesToShadeWhenSPressed)
            {
                image.color = ColorWhenSPressed;
            }
        }
        else
        {
            foreach (Image image in ImagesToShadeWhenSPressed)
            {
                image.color = defaultColor;
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            foreach (Image image in ImagesToShadeWhenEPressed)
            {
                image.color = ColorWhenEPressed;
            }
        }
        else
        {
            foreach (Image image in ImagesToShadeWhenEPressed)
            {
                image.color = defaultColor;
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
            foreach (Image image in ImagesToShadeWhenJPressed)
            {
                image.color = ColorWhenJPressed;
            }
        }
        else
        {
            foreach (Image image in ImagesToShadeWhenJPressed)
            {
                image.color = defaultColor;
            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            foreach (Image image in ImagesToShadeWhenKPressed)
            {
                image.color = ColorWhenKPressed;
            }
        }
        else
        {
            foreach (Image image in ImagesToShadeWhenKPressed)
            {
                image.color = defaultColor;

            }
        }

        if (Input.GetKey(KeyCode.L))
        {
            foreach (Image image in ImagesToShadeWhenLPressed)
            {
                image.color = ColorWhenLPressed;
            }
        }
        else
        {
            foreach (Image image in ImagesToShadeWhenLPressed)
            {
                image.color = defaultColor;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            foreach (Image image in ImagesToShadeWhenSpacePressed)
            {
                image.color = ColorWhenSpacePressed;
            }
        }
        else
        {
            foreach (Image image in ImagesToShadeWhenSpacePressed)
            {
                image.color = defaultColor;
            }
        }
    }
}
