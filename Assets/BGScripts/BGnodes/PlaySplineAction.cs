using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Splines;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "playSpline", story: "play [splineAnimate]", category: "Action", id: "79cb46a3e4e6a3bb31189bb86049032b")]
public partial class PlaySplineAction : Action
{
    [SerializeReference] public BlackboardVariable<SplineAnimate> SplineAnimate;

    protected override Status OnStart()
    {
        SplineAnimate.Value.Restart(true);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (SplineAnimate.Value.IsPlaying)
        {
            return Status.Running;
        }
        else
        {
            return Status.Success;
        }
    }


}

