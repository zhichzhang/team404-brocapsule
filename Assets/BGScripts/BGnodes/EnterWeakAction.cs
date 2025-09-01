using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "enterWeak", story: "stun [Self] and disable [Weapon] for [n] seconds if [IsWeaponGrabbed]", category: "Action", id: "63d6af5696118bb748d9eb87aa72763b")]
public partial class EnterWeakAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Weapon;
    [SerializeReference] public BlackboardVariable<float> N;
    [SerializeReference] public BlackboardVariable<bool> IsWeaponGrabbed;
    [SerializeReference] public BlackboardVariable<EnemyHitBoxBase> GrabHitBoxController;
    private float timer = 0;
    
    protected override Status OnStart()
    {
        if (IsWeaponGrabbed.Value)
        {
            timer = N.Value;
            //GrabHitBoxController.Value.gameObject.SetActive(false);
            Weapon.Value.SetActive(false);
            Self.Value.GetComponentInChildren<SpriteRenderer>().transform.localPosition = new Vector3(0, 0, 0);
            Self.Value.GetComponentInChildren<SpriteRenderer>().transform.localRotation = Quaternion.identity;
            return Status.Running;
            
        }
        //Debug.Log(timer);
        return Status.Failure;
        //if (IsParriedDuringUltimate.Value)
        //{
        //    Self.Value.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;   
        //    return Status.Running;
        //}
        //else
        //{
        //    return Status.Failure;
        //}
    }




    protected override Status OnUpdate()
    {
        timer -= Time.deltaTime;
        Self.Value.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        if (timer <= 0)
        {
            Weapon.Value.SetActive(true);
            IsWeaponGrabbed.Value = false;
            GrabHitBoxController.Value.result = 0;// important, set to zero or is grabbed will be set true in update
            return Status.Success;
        }
        //Debug.Log("Dropping weapon");
        return Status.Running;
    }

    //protected override void OnEnd()
    //{
    //}
}

