using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    [SerializeField] private float shakeForce;
    // Start is called before the first frame update
    public static CameraShakeManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(shakeForce);
    }

}
