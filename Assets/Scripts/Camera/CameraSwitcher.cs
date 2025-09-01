using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CameraSwitcher instance;
    private Animator cameraAni;
    private string currentCamera;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        cameraAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SwitchCamera(string name)
    {
        currentCamera = name;
        cameraAni.SetBool(name, true);
    }

    public void SwitchFromFixedtoFollowPlayer()
    {
        if (currentCamera == null)
        {
            Debug.Log("camera is already following player");
            return;
        }
        
        cameraAni.SetBool(currentCamera, false);
        currentCamera = null;
    }

}
