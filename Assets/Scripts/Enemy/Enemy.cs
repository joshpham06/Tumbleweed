using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar HealthBar;
    public Outline Outline;
    
    public int Damage;
    public float Speed;
    
    private float MaxHealth = 40f;
    private float CurrentHealth;
    
    void Awake()
    {
        Outline.enabled = false;
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
        
        Outline.enabled = true;
    }
    
    public void UnhighlightEnemy()
    {
        Outline.enabled = false;
    }
}
