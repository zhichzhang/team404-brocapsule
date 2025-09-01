using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "droneAttack", story: "aim and fire [bullet] to Player using [wController] and [pController]", category: "Action", id: "df80b1be58222af6d32f1ca6ec928e04")]
public partial class DroneAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Bullet;
    [SerializeReference] public BlackboardVariable<DroneWeaponController> WController;
    [SerializeReference] public BlackboardVariable<FixedRegionPatrolController> PController;
    private Player player;

    protected override Status OnStart()
    {
        player = PController.Value.isPlayerInRange();
        if (player == null)
        {
            return Status.Failure;
        }
        WController.Value.AimAtPosition(player.transform.position);
        GameObject.Instantiate(Bullet.Value, WController.Value.transform.position, WController.Value.transform.rotation);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        WController.Value.Fire();
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

