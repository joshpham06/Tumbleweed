using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public GameObject ProjectilePrefab;

    private Transform Target;
    private float Timer;
    private float AttackSpeed;
    private float AttackDamage;
    public bool IsPlayerAttack;

    void Awake()
    {
        Timer = 0f;
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        
        if (Target == null) return;
        
        if (Timer <= 0f)
        {
            Timer = AttackSpeed;
            SpawnProjectile();
        }
    }

    public void SetDamage(float damage)
    {
        AttackDamage = damage;
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        AttackSpeed = attackSpeed;
    }

    public void SetTarget(Transform target)
    {
        if (Target == target) return;
        Target = target;
    }

    public void ClearTarget()
    {
        Target = null;
    }

    private void SpawnProjectile()
    {
        if (Target == null) return;

        Vector3 direction = (Target.position - transform.position).normalized;
        direction.y = 0f;

        if (direction == Vector3.zero) return;

        GameObject proj = Instantiate(ProjectilePrefab, transform.position, Quaternion.LookRotation(direction));
        proj.GetComponent<Projectile>().Initialize(Target, AttackDamage);
    }
}