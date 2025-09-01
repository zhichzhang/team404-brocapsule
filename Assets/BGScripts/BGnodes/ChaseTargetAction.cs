using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseTarget", 
    description: "Chase player, if can't get close enough after max time, or player go out of detection range, go to chase pause state. Other wise go to attack state",
    story: "chase player", category: "Action", id: "a106be05ef4b8e7b98d7e6f6a1358bfe")]
public partial class ChaseTargetAction : Action
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

    [Tooltip("current state of enemy, find in black board")]
    [SerializeReference] public BlackboardVariable<AiState> currentState;

    // result of chase, true if player is caught, false if can't get close enough, refer to OnEnd() for usage
    private bool chaseResult;

    // ridigbody 2d of enemy
    private Rigidbody2D rb;

    // timer for chasing, if can't get close enough after max time, return with chase result false
    private float chaseTimer;

    protected override Status OnStart()
    {
        // player should not be null, but in case it is, return with chase result false
        if (player.Value == null)
        {
            chaseResult = false;
            return Status.Success;
        }
        rb = self.Value.GetComponent<Rigidbody2D>();
        chaseTimer = maxChaseTime.Value;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        chaseTimer -= Time.deltaTime;
        // if can't get close enough to player after maxTime, return with chase result false
        if (chaseTimer < 0)
        {

            chaseResult = false;
            return Status.Success;
        }

        // if suddenly lost sight of player, return with chase result false
        Player targetPlayer = playerDetector.Value.isPlayerInRange();
        if (targetPlayer == null)
        {
            chaseResult = false;
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
            chaseResult = true;
            return Status.Success;
        }

        // move towards player if not close enough
        rb.linearVelocityX = facingDir.Value * moveSpeed;
        return Status.Running;
    }

    protected override void OnEnd()
    {
        rb.linearVelocityX = 0;
        if (chaseResult)
        {
            currentState.Value = AiState.Attack;
        }
        else
        {
            currentState.Value = AiState.ChasePause;
        }
    }
}

