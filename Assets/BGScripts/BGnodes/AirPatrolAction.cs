using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AirPatrol", story: "[Self] make an random air move using [FixedRegionController]", category: "Action", id: "00f5c5fb4b8f67e4b966578a4aac7a8e")]
public partial class AirPatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<FixedRegionPatrolController> FixedRegionController;
    [SerializeReference] public BlackboardVariable<float> PatrolSpeed;
    [SerializeReference] public BlackboardVariable<float> PatrolTime;
    private int direction;
    private float timer;
    private float time;
    private float verticalSpeed;
    private Rigidbody2D rb;
    protected override Status OnStart()
    {
        
        rb = Self.Value.GetComponent<Rigidbody2D>();
        (direction, time, verticalSpeed) = FixedRegionController.Value.getRandomLocationInRegion(Self.Value.transform.position, PatrolTime, PatrolSpeed);
        timer = time;
        //Debug.Log(time);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            return Status.Success;
        }
        rb.linearVelocityX = PatrolSpeed * direction;
        rb.linearVelocityY = verticalSpeed;

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

