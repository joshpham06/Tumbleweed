using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar HealthBar;
    public Outline Outline;
    public RangeIndicator RangeIndicator;
    
    public int Damage;
    public float AttackRange = 2f;
    public float Speed;
    
    private float MaxHealth = 40f;
    private float CurrentHealth;
    
    void Awake()
    {
        Outline.enabled = false;
        CurrentHealth = MaxHealth;
        HealthBar.Initialize(MaxHealth);
        RangeIndicator.Initialize(AttackRange);
        RangeIndicator.gameObject.SetActive(false);
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
        Destroy(transform.parent.gameObject);
    }

    public void HighlightEnemy()
    {
        Outline.enabled = true;
        RangeIndicator.gameObject.SetActive(true);
    }
    
    public void UnhighlightEnemy()
    {
        Outline.enabled = false;
        RangeIndicator.gameObject.SetActive(false);
    }
}
