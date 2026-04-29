using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour 
{
	public RangeIndicator RangeIndicator;

	private int collectibleCount;
	private GameController gameController;
	
	void Awake()
	{
		gameController = GetComponentInParent<GameController>();
		RangeIndicator.Initialize(GameParameters.PlayerAttackRange);
		
		collectibleCount = 0;
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive(false);
			collectibleCount = collectibleCount + 1;
			gameController.OnPickUpCollectible(collectibleCount);
		}
	}
}