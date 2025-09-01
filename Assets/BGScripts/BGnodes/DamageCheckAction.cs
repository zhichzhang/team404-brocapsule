using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEditor;
using UnityEngine.UI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DamageCheck", story: "do DamageCheck using [HitBoxController]", category: "Action", id: "f21ca5f1143a724bb46b9d3fe379b84a")]
public partial class DamageCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyHitController> HitBoxController;
    [SerializeReference] public BlackboardVariable<float> ITimeOnHit;
    [SerializeReference] public BlackboardVariable<float> Health;
    [SerializeReference] public BlackboardVariable<EnemyDestroyer> Destroyer;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Drop;
    [SerializeReference] public BlackboardVariable<GameObject> OnHitEffect;
    [SerializeReference] public BlackboardVariable<Slider> Slider;
    private float timer;

    protected override Status OnStart()
    {
        //Health.Value = 100;
        if (Health.Value <= 0)
        {
            Health.Value = 100;
        }
        timer = 0;
        Slider.Value.maxValue = Health.Value;
        return Status.Running;
        
    }

    protected override Status OnUpdate()
    {
        timer -= Time.deltaTime;
        Slider.Value.value = Health.Value;
        if (timer < 0)
        {
            int damage = HitBoxController.Value.GetResult();
            if (damage > 0)
            {
                //Debug.Log("Enemy is Hit" + ", Health left is: "+ Health.Value);
                //Debug.Log(Time.time);
                OnHitEffect.Value.SetActive(true);
                Health.Value -= damage;
                timer = ITimeOnHit.Value;
            }
        }
        if (Health.Value < 0)
        {
            /// Drop weapon Here
            Vector2 pos = Self.Value.transform.position;
            GameObject gb = GameObject.Instantiate(Drop.Value, pos, Quaternion.identity);
            ///
            //Update kills
            if(ExternalDataManager.instance != null)
            {
                ExternalDataManager.instance.PlayerKills();
            }

            Destroyer.Value.DestroyMe();
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

