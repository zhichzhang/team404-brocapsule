using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

/// <summary>
/// used to sprint to a fixed distance from player to perform certain attack
/// this action calculate speed based on duration and distance
/// </summary>
[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GoToAttackLocation",
    description: "Used to sprint to a fixed distance from player to perform certain attack, this action calculate speed based on duration and distance",
    story: "Go to attack location", category: "Action", id: "85adcdfd5b221639182c01fa53379638")]
public partial class GoToAttackLocationAction : Action
{
    [Tooltip("game object of enemy, find in black board")]
    [SerializeReference] public BlackboardVariable<GameObject> self;

    [Tooltip("player reference, find in black board")]
    [SerializeReference] public BlackboardVariable<Player> player;

    [Tooltip("facing direction of enemy, find in black board")]
    [SerializeReference] public BlackboardVariable<int> facingDir;

    [Tooltip("sprint duration, should be roughly the same as attack spline animation duration that is parallel to this node")]
    [SerializeReference] public BlackboardVariable<float> sprintDuration;

    [Tooltip("distance from player that this attack should happend")]
    [SerializeReference] public BlackboardVariable<float> attackDistance;

    // speed of enemy, calculated based on distance and duration
    private float speed;

    // timer for sprint duration
    private float timer;

    // rigidbody of enemy
    private Rigidbody2D rb;

    protected override Status OnStart()
    {
        // face player
        float distance = (self.Value.transform.position - player.Value.transform.position).x;
        if (distance > 0)
        {
            if (facingDir.Value == 1)
            {
                self.Value.transform.Rotate(0, 180, 0);
                facingDir.Value = -1;
            }
        }
        else
        {
            if (facingDir.Value == -1)
            {
                self.Value.transform.Rotate(0, 180, 0);
                facingDir.Value = 1;
            }
        }
        // calculate speed, initiate timer and rb
        speed = (Mathf.Abs(distance) - attackDistance) / sprintDuration.Value;
        timer = sprintDuration.Value;
        rb = self.Value.GetComponent<Rigidbody2D>();
        // return immediately if already close enough
        if (speed < 0)
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // move enemy
        timer -= Time.deltaTime;
        rb.linearVelocityX = speed * facingDir.Value;
        
        // stop when timer is up
        if (timer <= 0)
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        rb.linearVelocityX = 0;
    }
}

