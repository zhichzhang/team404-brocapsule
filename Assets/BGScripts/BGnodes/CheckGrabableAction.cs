using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckGrabable", story: "update [GrabController] info with [WeaponHitCounter]", category: "Action", id: "9e796f955e78cdd11ede4f98bdbd079d")]
public partial class CheckGrabableAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyGrabController> GrabController;
    [SerializeReference] public BlackboardVariable<int> WeaponHitCounter;
    [SerializeReference] public BlackboardVariable<int> maxHit;
    [SerializeReference] public BlackboardVariable<bool> IsWeaponGrabbed;
    protected override Status OnStart()
    {
        if (WeaponHitCounter.Value >= 1)
        {
            if (!IsWeaponGrabbed.Value && !GrabController.Value.gameObject.activeSelf)
            {
                GrabController.Value.gameObject.SetActive(true);
                Debug.Log("Grababledddddddddddd");
                return Status.Success;
            }
            return Status.Success;

        }
        return Status.Failure;
        //else
        //{
        //    GrabController.Value.gameObject.SetActive(false);
        //    return Status.Failure;
        //}

    }

    
}

