using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongSpline : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject self;
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float speed = 1.0f;
    private float distancePercentage = 0.0f;
    private float splineLength = 0.0f;
    private bool canStart;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart)
        {
            distancePercentage += speed * Time.deltaTime / splineLength;
            if (distancePercentage > 1f)
            {
                distancePercentage = 0.0f;
                canStart = false;
               
                return;
            }

            Vector3 currPos = spline.EvaluatePosition(distancePercentage);
            Vector3 currTan = spline.EvaluateTangent(distancePercentage);
            //KnotLinkCollection kk = spline.KnotLinkCollection;
            transform.position = currPos;

            //Vector3 nextPos = spline.EvaluatePosition(distancePercentage + 0.01f);
            //Vector3 dir = nextPos - currPos;

            // Set the rotation based on the selected axis
            //switch (enemy.facingDir)
            //{
            //    case 1:
            //        transform.right = currTan;
            //        break;
            //    case -1:
            //        transform.right = -currTan;
            //        break;
            //}
        }
    }
    public void StartMove()
    {
        canStart = true;
    }

    public void StopMove()
    {
        canStart = false;
    }

    public bool isFinished()
    {
        return canStart;
    }
}
