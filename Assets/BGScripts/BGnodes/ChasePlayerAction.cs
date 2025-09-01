using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChasePlayer", story: "[Self] try chase player using [patrolController]", category: "Action", id: "e80254d10ada77bafbee091ee277d59b")]
public partial class ChasePlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<FixedRegionPatrolController> PatrolController;
    //[SerializeReference] public BlackboardVariable<FixedRegionPatrolController> fRPCtrl;
    [SerializeReference] public BlackboardVariable<float> chaseSpeed;
    [SerializeReference] public BlackboardVariable<float> chaseMinTime;
    [SerializeReference] public BlackboardVariable<float> chaseMaxTime;
    private Player player;
    private int direction;
    private float time;
    private float timer;
    private Rigidbody2D rb;

    protected override Status OnStart()
    {
        (direction, time) = PatrolController.Value.GetMoveToPlayer(Self.Value.transform.position, chaseSpeed, chaseMinTime, chaseMaxTime);
        if (direction == 0)
        {
            return Status.Failure;
        }
        rb = Self.Value.GetComponent<Rigidbody2D>();
        timer = time; 
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
        rb.linearVelocityX = chaseSpeed * direction;
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

