using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Splines;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SplineAttack", 
    description:"spline animation that enable attack box, have different sync mode, refer to code and tool tips",
    story: "Spline Attack", category: "Action", id: "f1e2f4624e63d615942d20d535a1e6ad")]
public partial class SplineAttackAction : Action
{
    
    public enum ATTACKMODE
    {
        SINGLE,
        MULTIPLE
    }
    public enum STOPMODE
    {
        SPLINE,
        BOOL
    }
    [Tooltip("single mode: attack is turned off after one successful hit/parry. multiple mode: attack can hit multiple times")]
    [SerializeReference] public BlackboardVariable<ATTACKMODE> attackMode;

    // bool stop mode is useful when you want your attack check to continue after spline animation ends
    [Tooltip("ignore this value for single mode attack, resetTime is the time to reset the attack once hit")]
    [SerializeReference] public BlackboardVariable<float> resetTime;

    [Tooltip("SPLINE: end node when spline end. BOOL: end node when given bool true")]
    [SerializeReference] public BlackboardVariable<STOPMODE> stopMode;

    [Tooltip("boolean to stop node, ignore if stop mode is spline")]
    [SerializeReference] public BlackboardVariable<bool> stopSignal;

    [Tooltip("splineAni will be the spline animation passed in from the blackboard; spline animates are usually under weapon in inspector")]
    [SerializeReference] public BlackboardVariable<SplineAnimate> splineAni;
    
    [Tooltip("attackController is the controller attached to attackBox component, might have different controller for different size attack check, if this attack has no damage check, like a precast, do not pass in any controller")]
    [SerializeReference] public BlackboardVariable<LancerWeaponController> attackContrl;
   
    [Tooltip("max parry before can be grabbed, should be in the blackboard")]
    [SerializeReference] public BlackboardVariable<int> maxParry;

    [Tooltip("knock back controller for knock back effect on parry, should be in blackboard")]
    [SerializeReference] public BlackboardVariable<KnockBackController> knockBackContrl;

    private bool isAttackEffective;
    private float attackTimer;

    protected override Status OnStart()
    {
        splineAni.Value.Restart(true);
        attackContrl.Value.gameObject.SetActive(true);
        isAttackEffective = true;
        attackTimer = 0;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        attackTimer -= Time.deltaTime;
        if (!splineAni.Value.IsPlaying && stopMode == STOPMODE.SPLINE)
        {
            return Status.Success;
        }
        if (stopSignal && stopMode == STOPMODE.BOOL)
        {
            return Status.Success;
        }

        if (attackContrl.Value != null)
        {
            if (isAttackEffective)
            {
                //Debug.Log("Attacddddddddddddd");
                switch (attackContrl.Value.result)
                {
                    case 0:
                        break;

                    case 1:
                        isAttackEffective = false;
                        if (attackMode == ATTACKMODE.MULTIPLE)
                        {
                            TryResetAttack();
                        }
                        break;

                    case 2:
                        if (knockBackContrl.Value != null)
                        {
                            //knockBackContrl.Value.KnockBack();
                        }
                        isAttackEffective = false;
                        if (attackMode == ATTACKMODE.MULTIPLE)
                        {
                            TryResetAttack();
                        }
                        break;

                }
            }
        }
        
        return Status.Running;
        
    }

    protected override void OnEnd()
    {
        // should not deactivate attack check here, as animation on body could still be playing
        //Debug.Log("SplineAttackAction End");
        //attackContrl.Value.gameObject.SetActive(false);
    }

    private void TryResetAttack()
    {
        if (attackTimer > 0) { return; }
        attackContrl.Value.gameObject.SetActive(true);
        isAttackEffective = true;
        attackTimer = resetTime.Value;
    }
}

