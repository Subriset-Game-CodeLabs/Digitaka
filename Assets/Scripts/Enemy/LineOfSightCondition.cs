using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Line Of Sight", story: "Check [Target] with Line Of Sight [Detector]", category: "Conditions", id: "1fb683669e9d1c2721dbedd3c2a751cb")]
public partial class LineOfSightCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<LineOfSightDetector> Detector;

    public override bool IsTrue()
    {
        return Detector.Value.PerformDetection(Target.Value) != null;
    }

}
