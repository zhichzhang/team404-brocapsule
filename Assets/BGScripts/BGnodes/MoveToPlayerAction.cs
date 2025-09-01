using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToPlayer", story: "move to player", category: "Action", id: "337142733709933be50d30bca747bb7c")]
public partial class MoveToPlayerAction : Action
{
    [Tooltip("enemy game object, find in blackboard")]
    [SerializeReference] public BlackboardVariable<GameObject> self;
    [Tooltip("player detector, find in blackboard")]
    [SerializeReference] public BlackboardVariable<PlayerDetectorV2> playerDetector;
    [Tooltip("player reference, find in blackboard")]
    [SerializeReference] public BlackboardVariable<Player> player;
    [Tooltip("facing direction of enemy, find in blackboard")]
    [SerializeReference] public BlackboardVariable<int> facingDir;
    [Tooltip("moving speed of chasing, normally larger than patrol speed")]
    [SerializeReference] public BlackboardVariable<float> moveSpeed;
    [Tooltip("stop treshold, if distance to player is smaller than this, stop moving")]
    [SerializeReference] public BlackboardVariable<float> stopTreshold;
    [Tooltip("max time to chase player, if can't get close enough after this time, change back to patrol state")]
    [SerializeReference] public BlackboardVariable<float> maxChaseTime;
    [Tooltip("max time to wait for player to re-enter detection range, if lost sight of player for this time, change back to patrol state")]
    [SerializeReference] public BlackboardVariable<float> maxLostSightTime;
    [Tooltip("true if able to get close to player, should attack after; false if not able to get clost to player enough")]
    [SerializeReference] public BlackboardVariable<bool> chaseResult;
    private Rigidbody2D rb;
    private float chaseTimer;
    private float lostSightTimer;
    private bool lostSight = false;

    protected override Status OnStart()
    {
        // this should not happen, but in case player is null, return with chase result false
        if (player.Value == null)
        {
            chaseResult.Value = false;
            return Status.Success;
        }
        rb = self.Value.GetComponent<Rigidbody2D>();
        chaseTimer = maxChaseTime.Value;
        lostSight = false;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        chaseTimer -= Time.deltaTime;

        // if lost sight of player for long enough, return with chase result false
        Player targetPlayer = playerDetector.Value.isPlayerInRange();
        if (targetPlayer == null)
        {
            if (!lostSight)
            {
                lostSight = true;
                lostSightTimer = maxLostSightTime.Value;
            }
            else
            {
                lostSightTimer -= Time.deltaTime;
                if (lostSightTimer < 0)
                {
                    player.Value = null;
                    chaseResult.Value = false;
                    rb.linearVelocityX = 0;
                    return Status.Success;
                }
            }
        }
        else
        {
            lostSight = false;
        }


        // if can't get close enough to player after maxTime, return with chase result false
        if (chaseTimer < 0)
        {
            
            player.Value = null;
            chaseResult.Value = false;
            rb.linearVelocityX = 0;
            return Status.Success;
        }

        // try facing player while chasing
        Vector2 dist = player.Value.transform.position - self.Value.transform.position;
        if (facingDir.Value * dist.x < 0)
        {
            facingDir.Value = facingDir.Value * -1;
            self.Value.transform.Rotate(0, 180, 0);
        }

        // stop if close enough
        if (Math.Abs(dist.x) < stopTreshold)
        {
            if (!lostSight)
            {
                rb.linearVelocityX = 0;
                chaseResult.Value = true;
                return Status.Success;
            }
            else
            {
                // edge case: lost sight when player close enough
                // if lost sight, stop moving and wait for the change timer to end
                // if player re-enters detection range, attack will be triggered
                // chasing is still enabled
                rb.linearVelocityX = 0;
                return Status.Running;
            }
        }

        // move towards player if not close enough
        rb.linearVelocityX = facingDir.Value * moveSpeed;
        return Status.Running;

    }

    protected override void OnEnd()
    {
        
    }
}

