using Unity.Behavior;
using UnityEngine;
using UnityEngine.UI;

public class LancerCreator : MonoBehaviour
{
    public GameObject objectToGenerate;
    public float timeInterval;
    private float timer;
    private GameObject generatedObject;
    public WayPoint2D startWayPoint;
    public LanceInRegion lanceInRegion;
    public bool involveTutorial = false;
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
            if (!involveTutorial) {
                CreateLancer();
            }
            else
            {
                if (!lanceInRegion.DetectInteractableLadder())
                {
                    CreateLancer();
                }
            }
        }
    }

    private void CreateLancer()
    {
        generatedObject = Instantiate(objectToGenerate, transform.position, transform.rotation);
        BehaviorGraphAgent agent = generatedObject.GetComponent<BehaviorGraphAgent>();
        agent.SetVariableValue("currentWayPoint", startWayPoint);
        timer = timeInterval;
        SetLoadingImageAlpha(0);
    }

    public bool haveSpawned()
    {
        return generatedObject != null;
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
