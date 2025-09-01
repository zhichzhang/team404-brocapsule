using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DeactivateAttackBox", story: "Deactivate my [hitBoxController]", category: "Action", id: "825edb79bb7d957646685972ad50ff48")]
public partial class DeactivateAttackBoxAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyHitBoxBase> HitBoxController;
    protected override Status OnStart()
    {
        if (HitBoxController.Value.gameObject.activeSelf) {

            HitBoxController.Value.gameObject.SetActive(false);
        }
        return Status.Success;
    }

    
}

