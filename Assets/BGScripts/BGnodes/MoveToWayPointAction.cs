using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToWayPoint", 
    description:"Move to the next waypoint, sucess if reach waypoind a waited there for waitTime (state remain patrol), fail if player detected (state change to chase).",
    story: "move to next waypoint", category: "Action", id: "056f79d1c5ec5a347d85b04701bf686e")]
public partial class MoveToWayPointAction : Action
{
    [Tooltip("GameObject of enemy, find in blackboard")]
    [SerializeReference] public BlackboardVariable<Rigidbody2D> self;

    [Tooltip("WayPoint to move to, find in blackboard")]
    [SerializeReference] public BlackboardVariable<WayPoint2D> currentWayPoint;

    [Tooltip("speed of patrol")]
    [SerializeReference] public BlackboardVariable<float> patrolSpeed;

    [Tooltip("Facing direction, find in blackboard")]
    [SerializeReference] public BlackboardVariable<int> facingDir;

    [Tooltip("Player detector, find in blackboard")]
    [SerializeReference] public BlackboardVariable<PlayerDetectorV2> playerDetector;

    [Tooltip("Player will be updated once detected, and state will change to chase, find this variable in black board")]
    [SerializeReference] public BlackboardVariable<Player> player;

    [Tooltip("Time to wait before going to next waypoint")]
    [SerializeReference] public BlackboardVariable<float> waitTime;

    [Tooltip("Current state of enemy, find in blackboard")]
    [SerializeReference] public BlackboardVariable<AiState> currentState;

    // enemy ridigbody 2d
    private Rigidbody2D rb;

    // timer for waiting when close to waypoint
    private float timer;

    // flag for reaching waypoint, true when close enough to waypoint and wait timer will start
    private bool reachedWayPoint;

    // result of patrol, true if should go to next waypoint, false if player is detected(state change to chase)
    // refer to OnEnd() for usage
    private bool patrolResult;

    protected override Status OnStart()
    {
        if (currentWayPoint == null)
        {
            Debug.LogError("currentWayPoint is null");   
            return Status.Failure;
        }
        // change facing direction to face the next waypoint
        rb = self.Value.GetComponent<Rigidbody2D>();
        if (currentWayPoint.Value.transform.position.x > self.Value.transform.position.x) // should face right
        {
            if (facingDir.Value == -1)
            {
                self.Value.transform.Rotate(0, 180, 0);
                facingDir.Value = 1;
            }
        }
        else // should face left
        {
            if (facingDir.Value == 1)
            {
                self.Value.transform.Rotate(0, 180, 0);
                facingDir.Value = -1;
            }
        }
        reachedWayPoint = false;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // check if player is in range, if it is, return with result false
        Player targetPlayer = playerDetector.Value.isPlayerInRange();
        if (targetPlayer != null)
        {
            player.Value = targetPlayer;
            patrolResult = false;
            return Status.Success;
        }

        // wait for wait time before going to next waypoint, this allows player detection when waiting near waypoint
        if (reachedWayPoint)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                patrolResult = true;
                return Status.Success;
            }
        }


        // move towards the next waypoint, if close enough, start waiting
        rb.linearVelocityX = patrolSpeed.Value * facingDir.Value;
        if (Mathf.Abs(self.Value.transform.position.x - currentWayPoint.Value.transform.position.x) < 1f)
        {
            
            rb.linearVelocityX = 0;
            if (!reachedWayPoint) {
                reachedWayPoint = true;
                timer = waitTime.Value;
            }
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        rb.linearVelocityX = 0;
        // change the current waypoint to the next waypoint and stop moving
        if (patrolResult)
        {
            currentWayPoint.Value = currentWayPoint.Value.nextWayPoint;
            //currentState.Value = AiState.Patrol;
        }
        // if player is detected, change state to chase
        else
        {
            currentState.Value = AiState.Chase;
        }

    }
}

