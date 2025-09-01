using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FallWhenNoGround", story: "[Self] fall when [isFalling] is true", category: "Action", id: "a45553eef453352dfcc165b73469b8b3")]
public partial class FallWhenNoGroundAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<bool> IsFalling;
    protected override Status OnStart()
    {
        if (!IsFalling) return Status.Failure;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!IsFalling) return Status.Failure;
        Self.Value.GetComponent<Rigidbody2D>().linearVelocityX = 0;
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

