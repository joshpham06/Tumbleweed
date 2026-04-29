using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar HealthBar;
    public Outline Outline;
    public RangeIndicator AttackRangeIndicator;
    public RangeIndicator DetectionRangeIndicator;
    public Transform Target;
    public AIDestinationSetter DestinationSetter;
    public AIPath AIPath;
    
    public int Damage;
    public float AttackRange = 2f;
    public float DetectionRange = 5f;
    public float Speed = 2.5f;
    
    private float MaxHealth = 40f;
    private float CurrentHealth;
    
    void Awake()
    {
        Outline.enabled = false;
        CurrentHealth = MaxHealth;
        HealthBar.Initialize(MaxHealth);
        AIPath.maxSpeed = Speed;
        
        InitializeIndicators();
    }

    void Update()
    {
        if (InRange())
        {
            DestinationSetter.target = Target;
        }
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
        //AttackRangeIndicator.gameObject.SetActive(true);
        //DetectionRangeIndicator.gameObject.SetActive(true);
    }
    
    public void UnhighlightEnemy()
    {
        Outline.enabled = false;
        //AttackRangeIndicator.gameObject.SetActive(false);
        //DetectionRangeIndicator.gameObject.SetActive(false);
    }
    
    private bool InRange()
    {
        if (Vector3.Distance(transform.position, Target.position) <= DetectionRange) return true;
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
