using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "parryCheck", story: "Do parry check using [HitController] + [BodyHitController] and update [hitCounter]", category: "Action", id: "b25b0ffb09fb850a7410c1f5638e47a0")]

public partial class ParryCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<HitController> HitController;
    [SerializeReference] public BlackboardVariable<HitController> BodyHitController;
    [SerializeReference] public BlackboardVariable<int> HitCounter;
    [SerializeReference] public BlackboardVariable<bool> CheckPoint1;
    private HitController hitController;
    private HitController bodyHitController;
    protected override Status OnStart()
    {
        hitController = HitController.Value;
        bodyHitController = BodyHitController.Value;
        hitController.StartAttackCheck();
        bodyHitController.StartAttackCheck();
        //Debug.Log("DDDDDDDDDDDDDss");
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (CheckPoint1.Value)
        {
            //Debug.Log("DDDDDDDDDDDDDsssssssssssss");
            //CheckPoint1.Value = false;
            return Status.Failure;
        }
        int hitResult1 = hitController.GetHitResult();
        int hitResult2 = bodyHitController.GetHitResult();
        //Debug.Log("DDDDDDDDDDDDDss");

        if (hitResult1 == 0)
        {
            //return Status.Running;
            //return Status.Failure;
        }
        else if (hitResult1 == 1)
        {
            //return Status.Running;
            hitController.StopAttackCheck();
            bodyHitController.StopAttackCheck();
            return Status.Failure;
        }
        else if (hitResult1 == 2)
        {
            HitCounter.Value++;
            hitController.StopAttackCheck();
            bodyHitController.StopAttackCheck();
            return Status.Failure;
        }
        if (hitResult2 == 0)
        {
            
            //return Status.Running;
        }
        else if (hitResult2 == 1)
        {
            hitController.StopAttackCheck();
            bodyHitController.StopAttackCheck();
            return Status.Failure;
            //return Status.Running;
        }
        else if (hitResult2 == 2)
        {
            HitCounter.Value++;
            hitController.StopAttackCheck();
            bodyHitController.StopAttackCheck();
            return Status.Failure;
        }
        return Status.Running;
        //switch (hitResult1)
        //{
        //    case 0:
        //        // dodged the attack, keep effective
        //        return Status.Running;


        //    case 1:
        //        // parried the attack, 
        //        return Status.Running;

        //    case 2:
        //        // parried the attack during ultimate
        //        IsParriedDuringUltimate.Value = true;

        //        return Status.Success;


        //}
        //switch (hitResult2)
        //{
        //    case 0:
        //        // dodged the attack, keep effective
        //        return Status.Running;

        //    case 1:
        //        // parried the attack, 
        //        return Status.Running;

        //    case 2:
        //        // parried the attack during ultimate
        //        IsParriedDuringUltimate.Value = true;

        //        return Status.Success;

        //}
        //return Status.Success;
    }

    protected override void OnEnd()
    {
        Debug.Log("DDDDDDDDDDDDDDDD");
    }
}

