using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "groundDetection", story: "update [IsGrounded] with [GroundDetector]", category: "Action", id: "1abb31864f0f626fcda8dda3113e987a")]
public partial class GroundDetectionAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsGrounded;
    [SerializeReference] public BlackboardVariable<GroundDetector> GroundDetector;

    protected override Status OnStart()
    {
        bool a = GroundDetector.Value.IsGrounded();
        if (a)
        {
            IsGrounded.Value = true;
            return Status.Success;
        }
        else
        {
            IsGrounded.Value = false;
            return Status.Failure;
        }
        //return Status.Success;
    }

   
}

