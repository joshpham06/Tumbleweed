using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float WiggleSpeed = 5f;
    public float WiggleAmount = 0.02f;
    
    private Transform Target;

    private float AttackDamage;
    private float RotateSpeed = 500f;
    private float Radius = 0.5f;
    private float WiggleTime;

    public void Initialize(Transform target, float damage)
    {
        Target = target;
        AttackDamage = damage;
    }

    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (Target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, RotateSpeed * Time.deltaTime);

        transform.position += transform.forward * GameParameters.ProjectileSpeed * Time.deltaTime;

        WiggleTime += Time.deltaTime;
        Vector3 wiggle = transform.right * Mathf.Sin(WiggleTime * WiggleSpeed) * WiggleAmount;
        transform.position += wiggle * Time.deltaTime;

        if (Vector3.Distance(transform.position, Target.position) <= Radius)
        {
            Target.GetComponentInParent<IsDamageable>().TakeDamage(AttackDamage);
            Destroy(gameObject);
        }
    }
}