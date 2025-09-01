using System;
using Unity.Behavior;
using UnityEngine;
using Composite = Unity.Behavior.Composite;
using Unity.Properties;
using System.Collections.Generic;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Weighted Random", story: "Execute a random children given [weights]", category: "Flow", id: "5b503b3668b4a98d23c4b90f423017b8")]
public partial class WeightedRandomSequence : Composite
{
    [SerializeReference] public BlackboardVariable<List<float>> Weights;
    [Tooltip("Weights list for children, for a certain children, the possibily is 0 if no weights provided(list shorter than children count)")]
    int m_RandomIndex = 0;
    protected override Status OnStart()
    {
        if (Children.Count == 0)
            return Status.Failure;

        List<float> effectiveWeights = new List<float>(Children.Count);
        for (int i = 0; i < Children.Count; i++)
        {
            effectiveWeights.Add(i < Weights.Value.Count ? Weights.Value[i] : 0);
        }

       
        float totalWeight = effectiveWeights.Sum();
        if (totalWeight == 0)
            return Status.Failure;  

        float randomPoint = UnityEngine.Random.value * totalWeight;
        float currentSum = 0;

        for (int i = 0; i < effectiveWeights.Count; i++)
        {
            currentSum += effectiveWeights[i];
            if (randomPoint <= currentSum)
            {
                m_RandomIndex = i;
                break;
            }
        }

        var status = StartNode(Children[m_RandomIndex]);
        return status == Status.Success || status == Status.Failure ? status : Status.Waiting;
    }

    protected override Status OnUpdate()
    {
        var status = Children[m_RandomIndex].CurrentStatus;
        if (status == Status.Success || status == Status.Failure)
            return status;

        return Status.Waiting;
    }

}

