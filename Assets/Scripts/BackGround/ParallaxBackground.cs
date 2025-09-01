using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    void Start()
    {
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(xPosition + distance, transform.position.y);
    }
}
