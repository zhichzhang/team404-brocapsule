using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "updateColor", story: "update weapon color with [WeaponColorer]", category: "Action", id: "e7f126634f1f5c58f858b440e362dd0c")]
public partial class UpdateColorAction : Action
{
    [SerializeReference] public BlackboardVariable<GeneralColorController> WeaponColorer;
    [SerializeReference] public BlackboardVariable<int> WeaponHitCounter;
    protected override Status OnStart()
    {
        switch (WeaponHitCounter.Value)
        {
            case 0:
                WeaponColorer.Value.SetColor(Color.gray);
                break;
            default:
                WeaponColorer.Value.SetColor(Color.red);
                break;

        }
        return Status.Success;
    }

   
}

