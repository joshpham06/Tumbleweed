using System;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour, IsDamageable
{
    public Transform Target;
    public AIDestinationSetter DestinationSetter;
    public AIPath AIPath;
    public Outline Outline;
    
    public HealthBar HealthBar;
    public RangeIndicator AttackRangeIndicator;
    public RangeIndicator DetectionRangeIndicator;
    public AutoAttack AutoAttack;
    
    public float AttackDamage = 5;
    public float AttackRange = 2f;
    public float AttackSpeed = 1f;
    public float Speed = 2.5f; 
    public float MaxHealth = 20f;
    
    private float DetectionRange = GameParameters.PlayerAttackRange;
    private float CurrentHealth;
    
    public static event Action OnEnemyKilled;
    
    void Awake()
    {
        Outline.enabled = false;
        CurrentHealth = MaxHealth;
        HealthBar.Initialize(MaxHealth);
        AIPath.maxSpeed = Speed;
        AutoAttack.SetAttackSpeed(AttackSpeed);
        AutoAttack.SetDamage(AttackDamage);
        
        //InitializeIndicators();
    }

    void Update()
    {
        if (InDetectionRange())
            DestinationSetter.target = Target;
        if (InAttackRange())
            AutoAttack.SetTarget(Target);
        else if (!InAttackRange())
            AutoAttack.ClearTarget();
    }

    public void TakeDamage(float damage)
    {
        DestinationSetter.target = Target;
        CurrentHealth -= damage;
        HealthBar.SetHealth(CurrentHealth);
        Player.Instance.Heal(damage * GameParameters.PlayerLifestealMultiplier);

        if (CurrentHealth <= 0)
        {
            KillEnemy();
        }
    }
    
    private void KillEnemy()
    {
        OnEnemyKilled?.Invoke();
        Destroy(transform.parent.gameObject);
    }

    public void HighlightEnemy()
    {
        Outline.enabled = true;
        //AttackRangeIndicator.gameObject.SetActive(true);
        //DetectionRangeIndicator.gameObject.SetActive(true);
    }
    
    public void UnhighlightEnemy()
    {
        Outline.enabled = false;
        //AttackRangeIndicator.gameObject.SetActive(false);
        //DetectionRangeIndicator.gameObject.SetActive(false);
    }
    
    private bool InDetectionRange()
    {
        if (Vector3.Distance(transform.position, Target.position) <= DetectionRange) return true;
        return false;
    }
    
    private bool InAttackRange()
    {
        if (Vector3.Distance(transform.position, Target.position) <= AttackRange) return true;
        return false;
    }

    private void InitializeIndicators()
    {
        AttackRangeIndicator.Initialize(AttackRange);
        DetectionRangeIndicator.Initialize(DetectionRange);
        AttackRangeIndicator.gameObject.SetActive(true);
        DetectionRangeIndicator.gameObject.SetActive(true);
    }
}
