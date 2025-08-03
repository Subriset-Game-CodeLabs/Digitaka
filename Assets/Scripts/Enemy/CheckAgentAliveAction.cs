using System;
using Enemy;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Check Agent Alive", story: "Check If [Self] is Alive", category: "Action", id: "6a2dfca67b041a8c1aa6274ebc6fa2a7")]
public partial class CheckAgentAliveAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnUpdate()
    {
        var agentStats = Self.Value.GetComponent<EnemyStats>();
        return agentStats.IsAlive ? Status.Failure : Status.Success;
    }

}

