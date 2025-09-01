using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "OnGrabUpdate", story: "update [isWeaponGrabbed] with [GrabController]", category: "Action", id: "438d4a28b1952871c61c5bccdc7e6573")]
public partial class OnGrabUpdateAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsWeaponGrabbed;
    [SerializeReference] public BlackboardVariable<EnemyGrabController> GrabController;
    protected override Status OnStart()
    {
        if (GrabController.Value.result == 1)
        {
            IsWeaponGrabbed.Value = true;
            return Status.Success;
        }
        else
        {
            IsWeaponGrabbed.Value = false;
            return Status.Failure;
        }
        
    }

    
}

