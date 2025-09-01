using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.UI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DamageCheckV2", 
    description: "Separete damage check node for enemy, will change state to Dead when health is 0. Should be attached to a non repeat start node",
    story: "do damage check", category: "Action", id: "45289e039c4e204aedb880022f4b5527")]
public partial class DamageCheckV2Action : Action
{

    [Tooltip("hit box controller for damage detection, find in black board")]
    [SerializeReference] public BlackboardVariable<EnemyHitController> HitBoxController;

    [Tooltip("invincible time once hit, find in black board")]
    [SerializeReference] public BlackboardVariable<float> ITimeOnHit;

    [Tooltip("initial heath, find in black board")]
    [SerializeReference] public BlackboardVariable<float> Health;

    [Tooltip("game object of enemy, find in black board")]
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    [Tooltip("on hit effect, currently just a sprite that turns off after enabled, find in black board")]
    [SerializeReference] public BlackboardVariable<GameObject> OnHitEffect;

    [Tooltip("health bar ui slider, find in black board")]
    [SerializeReference] public BlackboardVariable<Slider> Slider;

    [Tooltip("current state of enemy, find in black board")]
    [SerializeReference] public BlackboardVariable<AiState> currentState;

    // invincible timer
    private float timer;

    protected override Status OnStart()
    {
        // initialize health value
        if (Health.Value <= 0)
        {
            Health.Value = 100;
        }

        // initialize invincible timer
        timer = 0;

        // set UI slider max value
        Slider.Value.maxValue = Health.Value;
        return Status.Running;

    }

    protected override Status OnUpdate()
    {
        if (Health.Value < 0)
        {
            // change state to dead when health is 0
            // set state is done in every iteration to prevent race condition (other action may happen to reach the point to changing state)
            // in that edge case when other action happen to change state to other state, this action will change it back to dead
            currentState.Value = AiState.Dead;
            return Status.Running;
        }



        // update invincible timer
        timer -= Time.deltaTime;

        // check if enemy is hit when invincible timer is expired
        if (timer < 0)
        {
            int damage = HitBoxController.Value.GetResult();
            if (damage > 0)
            {
                // display hit effect, replace with actual effect (partical) later on
                OnHitEffect.Value.SetActive(true);
                Health.Value -= damage;
                timer = ITimeOnHit.Value;
            }
        }

        // synchronize UI slider value with health value
        Slider.Value.value = Health.Value;

        

        

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

