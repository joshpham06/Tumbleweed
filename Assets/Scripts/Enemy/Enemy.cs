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
    
    public int Damage;
    public float AttackRange = 2f;
    public float AttackSpeed = 1f;
    public float Speed = 2.5f; 
    public float MaxHealth = 20f;
    
    private float DetectionRange = GameParameters.PlayerAttackRange;
    private float CurrentHealth;
    
    void Awake()
    {
        Outline.enabled = false;
        CurrentHealth = MaxHealth;
        HealthBar.Initialize(MaxHealth);
        AIPath.maxSpeed = Speed;
        AutoAttack.SetAttackSpeed(AttackSpeed);
        
        InitializeIndicators();
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
        CurrentHealth -= damage;
        HealthBar.SetHealth(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            KillEnemy();
        }
    }

    // might not need separate method doing nothing else when the enemy is killed
    private void KillEnemy()
    {
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
