using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol2D", story: "[Self] 2D patrol Left and Right if [Player] not null", category: "Action", id: "fc81bdee5716a80d517a5cd12f4cc735")]
public partial class Patrol2DAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Player> Player;
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> MaxTimeFlip;
    [SerializeReference] public BlackboardVariable<int> FacingDir;
    [SerializeReference] public BlackboardVariable<bool> IsGrounded;
    [SerializeReference] public BlackboardVariable<bool> IsWalled;
    private float timer;


    protected override Status OnStart()
    {
        if (Player.Value != null)
        {
            return Status.Failure;
        }
        timer = MaxTimeFlip;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Player.Value != null)
        {
            return Status.Failure;
        }
        timer -= Time.deltaTime;
        
        if (!IsGrounded || IsWalled)
        {
            Flip();
            timer = MaxTimeFlip;
        }
        Rigidbody2D rbSelf = Self.Value.GetComponent<Rigidbody2D>();
        
        
        if (timer > 0)
        {
            rbSelf.linearVelocityX = FacingDir.Value * Speed.Value;
        }
        else
        {
            if (UnityEngine.Random.value < 0.5)
            {
                Flip();
            }
            
            timer = MaxTimeFlip;
        }
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    private void Flip()
    {
        FacingDir.Value = FacingDir.Value * -1;
        Self.Value.transform.Rotate(0, 180, 0);
    }
}

