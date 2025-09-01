using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Splines;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WeaponSpline", 
    description: "Play provided spline animation",
    story: "Play [SplineAnimation] on my wepaon", category: "Action", id: "083e2cfa0abf64444d6b7137c0973b62")]
public partial class WeaponSplineAction : Action
{
    [Tooltip("spline anim to play, find in black board")]
    [SerializeReference] public BlackboardVariable<SplineAnimate> SplineAnimation;
    private SplineAnimate splineAni;

    protected override Status OnStart()
    {

        // I don't know why I can't call play here, Restart seems to work
        // when call play, the second time even if I call Restart, it won't play
        SplineAnimation.Value.Restart(true);
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (SplineAnimation.Value.IsPlaying)
        {
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

