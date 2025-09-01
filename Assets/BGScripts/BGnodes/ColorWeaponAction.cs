using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "colorWeapon", story: "color my weapon red with [WeaponColorer]", category: "Action", id: "8a917727c4a5dafec0003754d05acae4")]
public partial class ColorWeaponAction : Action
{
    [SerializeReference] public BlackboardVariable<GeneralColorController> WeaponColorer;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

