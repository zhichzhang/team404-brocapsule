using UnityEngine;
using UnityEngine.UI;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject objectToGenerate;
    public float timeInterval;
    private float timer;
    private GameObject generatedObject;

    public Image loadingImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetLoadingImageAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (generatedObject == null)
        {
            timer -= Time.deltaTime;
            UpdateLoadingImage();
        }

        if (timer <= 0)
        {
            generatedObject = Instantiate(objectToGenerate, transform.position, transform.rotation);
            timer = timeInterval;
            SetLoadingImageAlpha(0);
        }
    }

    private void UpdateLoadingImage()
    {
        if (loadingImage != null)
        {
            loadingImage.fillAmount = 1 - (timer / timeInterval);
            SetLoadingImageAlpha(1);
        }
    }

    private void SetLoadingImageAlpha(float alpha)
    {
        if (loadingImage != null)
        {
            Color color = loadingImage.color;
            color.a = alpha;
            loadingImage.color = color;
        }
    }
}
