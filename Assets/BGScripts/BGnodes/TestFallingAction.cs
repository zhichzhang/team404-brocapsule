using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TestFalling", story: "update [isFalling] using [IsGrounded]", category: "Action", id: "0f558b0317e66f67421f597c9daf4300")]
public partial class TestFallingAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsFalling;
    [SerializeReference] public BlackboardVariable<bool> IsGrounded;
    [SerializeReference] public BlackboardVariable<float> time;
    private float timer;

    protected override Status OnStart()
    {
        
        if (IsGrounded.Value)
        {
            IsFalling.Value = false;
            return Status.Failure;
        }
        if (IsFalling.Value)
        {
            return Status.Success;
        }
        timer = time.Value;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        timer -= Time.deltaTime;
        if (IsGrounded.Value)
        {
            IsFalling.Value = false;
            return Status.Failure;
        }
        if (timer < 0)
        {
            Debug.Log("Set Falling Value");
            IsFalling.Value = true;
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

