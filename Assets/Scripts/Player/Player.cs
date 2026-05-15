using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IsDamageable
{
	public RangeIndicator RangeIndicator;
	public HealthBar HealthBar;
	public GameObject FloatingTextPrefab;

	
	private GameController GameController;
	private float CurrentHealth;
	
	public static Player Instance { get; private set; }
	
	void Awake()
	{
		Instance = this;
		GameController = GetComponentInParent<GameController>();
		RangeIndicator.Initialize(GameParameters.PlayerAttackRange);
		HealthBar.Initialize(GameParameters.PlayerMaxHealth);
		CurrentHealth = GameParameters.PlayerMaxHealth;
		
		GameObject obj = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
		obj.GetComponent<FloatingText>().Initialize("", Color.green);
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive(false);
			GameController.OnPickUpCollectible();
		}
	}
	
	public void Heal(float amount)
	{
		CurrentHealth = Mathf.Min(CurrentHealth + amount, GameParameters.PlayerMaxHealth);
		HealthBar.SetHealth(CurrentHealth);
    
		Vector3 spawnPos = transform.position + Random.insideUnitSphere * 0.5f;
		spawnPos.y = transform.position.y + 1f;
    
		GameObject obj = Instantiate(FloatingTextPrefab, spawnPos, Quaternion.identity);
		obj.GetComponent<FloatingText>().Initialize("+" + amount, Color.green);
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