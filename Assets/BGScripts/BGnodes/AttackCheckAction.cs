using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackCheck", story: "Attack check using [weaponcontroller] and [hitCounter]", category: "Action", id: "65342fb4cd0fc76dba9fdcb9a978b2e8")]
public partial class AttackCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<LancerWeaponController> Weaponcontroller;
    [SerializeReference] public BlackboardVariable<EnemyGrabController> GrabController;
    [SerializeReference] public BlackboardVariable<int> HitCounter;
    [SerializeReference] public BlackboardVariable<bool> CheckPoint1;


    protected override Status OnStart()
    {
        Weaponcontroller.Value.gameObject.SetActive(true);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        if (CheckPoint1.Value)
        {
            
            return Status.Success;
        }
        
        
        //if (!Weaponcontroller.Value.gameObject.activeSelf)
        //{
        //    HitCounter.Value++;
            
        //}
        switch (Weaponcontroller.Value.result)
        {
            case 0:
                return Status.Running;
            case 1:
                //Weaponcontroller.Value.gameObject.SetActive(false);
                Debug.Log("Hit");
                return Status.Success;
            case 2:
                Debug.Log("Parry");
                //Weaponcontroller.Value.gameObject.SetActive(false);
                HitCounter.Value++;
                if (HitCounter.Value >= 1)
                {
                    GrabController.Value.gameObject.SetActive(true);
                }
                return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        //Weaponcontroller.Value.gameObject.SetActive(false);
    }
}

