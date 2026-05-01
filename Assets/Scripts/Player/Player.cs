using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IsDamageable
{
	public RangeIndicator RangeIndicator;
	public HealthBar HealthBar;
	
	private GameController GameController;
	private int CollectibleCount;
	private float CurrentHealth;
	
	void Awake()
	{
		GameController = GetComponentInParent<GameController>();
		RangeIndicator.Initialize(GameParameters.PlayerAttackRange);
		HealthBar.Initialize(GameParameters.PlayerMaxHealth);
		
		CollectibleCount = 0;
		CurrentHealth = GameParameters.PlayerMaxHealth;
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive(false);
			CollectibleCount = CollectibleCount + 1;
			GameController.OnPickUpCollectible(CollectibleCount);
		}
	}
	
	public void TakeDamage(float damage)
	{
		CurrentHealth -= damage;
		HealthBar.SetHealth(CurrentHealth);

		if (CurrentHealth <= 0)
		{
			KillPlayer();
		}
	}

	private void KillPlayer()
	{
		GameController.StateUpdate(GameController.GameStates.GameLost);
		Destroy(transform.parent.gameObject); // this also destroys the camera. might be fine though
	}
}