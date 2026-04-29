using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour 
{
	public RangeIndicator RangeIndicator;
	
	void Awake()
	{
		RangeIndicator.Initialize(GameParameters.PlayerAttackRange);
	}
}