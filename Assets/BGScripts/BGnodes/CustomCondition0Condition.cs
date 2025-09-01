using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CustomCondition0", story: "[Player] null or not [IsGrounded] but [checkPoint1] not passed", category: "Conditions", id: "6f23d1ff68794e506a9de713f506b6d6")]
public partial class CustomCondition0Condition : Condition
{
    [SerializeReference] public BlackboardVariable<Player> Player;
    [SerializeReference] public BlackboardVariable<bool> IsGrounded;
    [SerializeReference] public BlackboardVariable<bool> CheckPoint1;

    public override bool IsTrue()
    {
        if (CheckPoint1)
        {
            return false;
        }
        else
        {
            if (Player == null || !IsGrounded)
            {
                return true;
            }
            return false;
        }
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
