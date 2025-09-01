using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Death", 
    description: "Death procedure for enemy, will instantiate drop object and destroy enemy game object",
    story: "death procedure", category: "Action", id: "05587158b5cbc5d96abd6e8369e09216")]
public partial class DeathAction : Action
{
    [Tooltip("game object of enemy, find in black board")]
    [SerializeReference] public BlackboardVariable<GameObject> self;

    [Tooltip("destorier for enemy, find in black board")]
    [SerializeReference] public BlackboardVariable<EnemyDestroyer> destroyer;

    [Tooltip("drop created when enemy dead, find in black board")]
    [SerializeReference] public BlackboardVariable<GameObject> drop;
    protected override Status OnStart()
    {

        // instantiate drop item
        Vector2 pos = self.Value.transform.position;
        GameObject gb = GameObject.Instantiate(drop.Value, pos, Quaternion.identity);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // destroy enemy
        destroyer.Value.DestroyMe();
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

