using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar HealthBar;
    
    public int Damage;
    public float Speed;
    
    private float MaxHealth = 100f;
    private float CurrentHealth;

    void Awake()
    {
        CurrentHealth = MaxHealth;
        HealthBar.Initialize(MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        HealthBar.SetHealth(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }

    public void HighlightEnemy()
    {
        //highlight enemy when selected (cell shading? or just change color)
    }
}
