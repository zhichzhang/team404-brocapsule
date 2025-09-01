using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChasePause", 
    description: "Wait for N seconds and do detection, if player is still in detection range, chase; else go back to patrol",
    story: "Try detect player in [n] seconds", category: "Action", id: "5e4b6d9cefe5331558f1d6fd58949efb")]
public partial class ChasePauseAction : Action
{
    [Tooltip("wait time before start redetecting player")]
    [SerializeReference] public BlackboardVariable<float> N;

    [Tooltip("player detector, find in blackboard")]
    [SerializeReference] public BlackboardVariable<PlayerDetectorV2> playerDetector;

    [Tooltip("current state of enemy, find in black board")]
    [SerializeReference] public BlackboardVariable<AiState> currentState;

    [Tooltip("player refernce, find in black board")]
    [SerializeReference] public BlackboardVariable<Player> player;

    // result of detection, true if player is detected, false if not, refer to OnEnd() for usage
    private bool detectResult;

    // timer for waiting
    private float timer;

    protected override Status OnStart()
    {
        // initialize timer
        timer = N.Value;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // pause for N seconds and try to detect player
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Player targetPlayer = playerDetector.Value.isPlayerInRange();
            if (targetPlayer != null)
            {
                detectResult = true;
                return Status.Success;
            }
            else
            {
                detectResult = false;
                return Status.Success;
            }
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (detectResult)
        {
            currentState.Value = AiState.Chase;
        }
        else
        {
            player.Value = null;
            currentState.Value = AiState.Patrol;
        }
    }
}

