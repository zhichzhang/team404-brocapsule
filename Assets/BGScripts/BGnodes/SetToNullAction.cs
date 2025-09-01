using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetToNull", story: "Set [Reference] to null", category: "Action", id: "84b28dcccab99fd79bf9a0b0e5087e11")]
public partial class SetToNullAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Reference;
    protected override Status OnStart()
    {
        Reference.Value = null;
        return Status.Success;
    }

    
}

