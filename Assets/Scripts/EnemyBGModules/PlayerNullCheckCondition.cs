using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "PlayerNullCheck", story: "[Player] is null", category: "Conditions", id: "39c63470d6220a67be2fc9782659c29e")]
public partial class PlayerNullCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Player> Player;

    public override bool IsTrue()
    {
        return Player.Value == null;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
