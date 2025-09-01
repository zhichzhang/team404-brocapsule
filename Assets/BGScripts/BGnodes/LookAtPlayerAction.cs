using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookAtPlayer", story: "[Self] lookat [Player]", category: "Action", id: "72378e1c3e4c9202c7481d4176e31aef")]
public partial class LookAtPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Player> Player;
    [SerializeReference] public BlackboardVariable<int> FacingDir;

    protected override Status OnStart()
    {
        if (Player.Value == null)
        {
            Debug.Log("Player is null WHEN TRYING TO LOOK");
            return Status.Success;
           
        }
        if ((Self.Value.transform.position - Player.Value.transform.position).x > 0)
        {
            if (FacingDir.Value == 1)
            {
                Self.Value.transform.Rotate(0, 180, 0);
                FacingDir.Value = -1;
            }
        }
        else
        {
            if (FacingDir.Value == -1)
            {
                Self.Value.transform.Rotate(0, 180, 0);
                FacingDir.Value = 1;
            }
        }
        return Status.Success;
    }

    
}

