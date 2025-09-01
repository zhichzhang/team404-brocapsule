using System;
using Unity.Behavior;
using UnityEngine;

/// <summary>
/// A custom player detector that trim detection box to the nearest walls/grounds
/// </summary>
public class PlayerDetectorV2 : MonoBehaviour
{
    public float maxDistanceBack;
    public float maxDistanceFront;
    public float maxDistanceUp;
    private float distanceFront;
    private float distanceBack;
    private float distanceUp;
    private float distanceDown;
    public bool showGizmo;
    private Vector2 boxCenter;
    private Vector2 boxSize;
    private BehaviorGraphAgent agent;

    private void Start()
    {
        agent = GetComponent<BehaviorGraphAgent>();
    }
    private void Update()
    {
        // trim the detection box to the nearest walls/grounds
        // only distance down to ground is mauallly set
        float upToGroundDistance = Physics2D.Raycast(transform.position, Vector2.up, 1000, LayerMask.GetMask("Ground")).distance;
        float frontToGroundDistance = Physics2D.Raycast(transform.position, transform.right, 1000, LayerMask.GetMask("Ground")).distance;
        float backToGroundDistance = Physics2D.Raycast(transform.position, -transform.right, 1000, LayerMask.GetMask("Ground")).distance;
        distanceDown = Physics2D.Raycast(transform.position, Vector2.down, 1000, LayerMask.GetMask("Ground")).distance;
        if (upToGroundDistance != 0)
        {
            distanceUp = Mathf.Min(upToGroundDistance, maxDistanceUp);
        }
        else
        {
            distanceUp = maxDistanceUp;
        }

        if (frontToGroundDistance != 0)
        {
            distanceFront = Mathf.Min(frontToGroundDistance, maxDistanceFront);
        }
        else
        {
            distanceFront = maxDistanceFront;
        }
        if (backToGroundDistance != 0)
        {
            distanceBack = Mathf.Min(backToGroundDistance, maxDistanceBack);
        }
        else
        {
            distanceBack = maxDistanceBack;
        }

        // retrieve facing direction from behavior agent blackboard
        // could just use transform.right, but this is for demonstration for how to use blackboard variable
        BlackboardVariable<int> facingDir = new BlackboardVariable<int>(1);
        agent.GetVariable<int>("facingDir",out facingDir);

        // update center and size of the detection box
        Vector2 centerOffSet = new Vector2(facingDir.Value*(distanceFront - distanceBack) / 2, (distanceUp - distanceDown) / 2);
        boxCenter = (Vector2)transform.position + centerOffSet;
        boxSize = new Vector2(distanceFront + distanceBack, distanceUp + distanceDown);

    }

    public Player isPlayerInRange()
    {
        // detect player in the detection box
        Vector2 detectBoxC1 = boxCenter + boxSize / 2;
        Vector2 detectBoxC2 = boxCenter - boxSize / 2;
        Collider2D[] colliders = Physics2D.OverlapAreaAll(detectBoxC1, detectBoxC2, LayerMask.GetMask("Player"));
        foreach (var hit in colliders)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
            {
                return player;
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(boxCenter, boxSize);
        }
    }
}
