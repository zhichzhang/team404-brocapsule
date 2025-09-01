using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "resetWeapon", story: "reset [weaponController]", category: "Action", id: "454b06534cd7087467123b37962a88e4")]
public partial class ResetWeaponAction : Action
{
    [SerializeReference] public BlackboardVariable<DroneWeaponController> WeaponController;

    protected override Status OnStart()
    {
        WeaponController.Value.ResetAimPosition();
        return Status.Success;
    }

    
}

