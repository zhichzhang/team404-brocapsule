using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WallDetection", story: "Update [IsWalled] with [WallDetector]", category: "Action", id: "4238cda4f70ad23e0eaed07dedf958ff")]
public partial class WallDetectionAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsWalled;
    [SerializeReference] public BlackboardVariable<WallDetector> WallDetector;

    protected override Status OnStart()
    {
        bool a = WallDetector.Value.IsGrounded();
        if (a)
        {
            IsWalled.Value = true;
            return Status.Success;
        }
        else
        {
            IsWalled.Value = false;
            return Status.Failure;
        }
    }

    
}

