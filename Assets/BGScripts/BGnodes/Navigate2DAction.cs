using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Navigate2D", story: "2D Navigate [Agent] to [Player]", category: "Action", id: "2a64789508e0c1c7c34d21b97e8ce710")]
public partial class Navigate2DAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Player> Player;
    [SerializeReference] public BlackboardVariable<float> moveSpeed;
    [SerializeReference] public BlackboardVariable<float> stopTreshold;
    [SerializeReference] public BlackboardVariable<int> facingDir;

    protected override Status OnStart()
    {
        if (Player.Value == null)
        {
            return Status.Failure;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Player.Value == null)
        {
            return Status.Failure;
        }

        Rigidbody2D rbAgent = Agent.Value.GetComponent<Rigidbody2D>();
        Vector2 dist = Player.Value.transform.position - Agent.Value.transform.position;
        if (Math.Abs(dist.x) < stopTreshold)
        {
            rbAgent.linearVelocityX = 0;
            return Status.Success;
        }
        if (facingDir.Value*dist.x < 0)
        {
            facingDir.Value = facingDir.Value * -1;
            Agent.Value.transform.Rotate(0, 180, 0);
        }
        rbAgent.linearVelocityX = facingDir.Value* moveSpeed;
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

