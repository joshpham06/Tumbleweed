using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour 
{
	
	void Awake()
	{
		RangeIndicator rangeIndicator = GetComponent<RangeIndicator>();
		rangeIndicator.Initialize(GameParameters.PlayerAttackRange);
	}
}