using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "sprint", story: "[Self] sprint foward", category: "Action", id: "c31255c86adbf87133484c34ba51e496")]
public partial class SprintAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> Duration;
    private float timer;
    private Rigidbody2D rb;

    protected override Status OnStart()
    {
        timer = Duration.Value;
        rb = Self.Value.GetComponent<Rigidbody2D>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer >= 0)
        {
            rb.linearVelocity = Self.Value.transform.right * Speed.Value;
            return Status.Running;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            return Status.Success;
        }
        //return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

