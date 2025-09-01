using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "resetWeaponLocation", story: "reset my [weapon] location", category: "Action", id: "7b0c44fdc7abe24fe2e6becf56dc4c36")]
public partial class ResetWeaponLocationAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Weapon;
    [SerializeReference] public BlackboardVariable<Vector3> offsetToParent;

    protected override Status OnStart()
    {
        if (Weapon.Value.transform.parent == null)
        {
            Debug.LogError("Weapon is not attached to any parent, can't reset location");
            return Status.Failure;
        }
        Weapon.Value.transform.localPosition = offsetToParent;
        Weapon.Value.transform.localRotation = Quaternion.identity;
        return Status.Success;
    }

    
}

