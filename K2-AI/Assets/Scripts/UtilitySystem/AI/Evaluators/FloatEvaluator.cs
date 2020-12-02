using UnityEngine;

[CreateAssetMenu(fileName = "FloatEvaluator", menuName = "Evaluators/FloatEvaluator")]
public class FloatEvaluator : UtilityEvaluator
{
    private FloatValue floatValue;

    public override void OnInitialize(BlackBoard bb)
    {
        floatValue = bb.GetFloatVariableValue(VariableType);
		/*if (bb.gameObject.name == "NinjaAI")
		{
			for (float f = 0; f < 1; f += 0.01f)
			{
				Debug.Log(evaluationCurve.Evaluate(f));
			}
		}*/
	}

	public override float GetMaxValue()
    {
        return floatValue.MaxValue;
    }

    public override float GetValue()
    {
        return floatValue.Value;
    }

	public override void OnReset()
	{
		//nothing
	}
}
