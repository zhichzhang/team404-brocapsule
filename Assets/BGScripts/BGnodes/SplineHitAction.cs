using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Splines;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "splineHit", story: "Play [Spline] on my weapon with hit check", category: "Action", id: "153940c1d1381f1a3d7e42b406c255c7")]
public partial class SplineHitAction : Action
{
    [SerializeReference] public BlackboardVariable<SplineAnimate> Spline;
    //private bool isEffective;
    protected override Status OnStart()
    {

        // I don't know why I can't call play here, Restart seems to work
        // when call play, the second time even if I call Restart, it won't play
        Spline.Value.Restart(true);
        //isEffective = true;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Spline.Value.IsPlaying)
        {
            // attack check logic, step isEffective to false if hit is negated
            return Status.Running;
        }
        else
        {

            return Status.Success;
        }

    }

    protected override void OnEnd()
    {
    }
}

