using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float RotateSpeed = 200f;
    public float Radius = 0.2f;
    public LayerMask EnemyLayer;

    private Transform Target;

    public void Initialize(Transform target)
    {
        Target = target;
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

        if (Vector3.Distance(transform.position, Target.position) <= Radius)
        {
            Target.GetComponentInParent<Enemy>().TakeDamage(GameParameters.ProjectileDamage);
            Destroy(gameObject);
        }
    }
}