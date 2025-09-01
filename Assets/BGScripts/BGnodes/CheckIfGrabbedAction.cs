using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "checkIfGrabbed", story: "update [isWeaponGrabbed] with [GrabController]", category: "Action", id: "208a9eef90dda4807f5dbeeb0cf026aa")]
public partial class CheckIfGrabbedAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsWeaponGrabbed;
    [SerializeReference] public BlackboardVariable<EnemyHitBoxBase> GrabController;
    [SerializeReference] public BlackboardVariable<int> WeaponHitCounter;
    protected override Status OnStart()
    {

        //if (WeaponHitCounter.Value >= 1)
        //{
        //    if (!IsWeaponGrabbed.Value && !GrabController.Value.gameObject.activeSelf)
        //    {
        //        GrabController.Value.gameObject.SetActive(true);
        //        //Debug.Log("Grababledddddddddddd");
                
        //    }
        //    //return Status.Success;
        //}
        //else
        //{
        //    GrabController.Value.gameObject.SetActive(false);
        //    //return Status.Failure;
        //}
        //}
        if (GrabController.Value.result == 1)
        {
            
            if (!IsWeaponGrabbed.Value)
            {

                
                IsWeaponGrabbed.Value = true;
                WeaponHitCounter.Value = 0;

                
                return Status.Success;
            }

        }
        return Status.Failure;
    }


}

