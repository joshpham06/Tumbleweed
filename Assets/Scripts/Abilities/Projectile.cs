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
        Vector3 direction = Target != null
            ? (Target.position - transform.position).normalized
            : transform.forward;

        if (Target != null)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, RotateSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * GameParameters.ProjectileSpeed * Time.deltaTime;

        Collider[] hits = Physics.OverlapSphere(transform.position, Radius, EnemyLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<Enemy>()?.TakeDamage(GameParameters.ProjectileDamage);
            Destroy(gameObject);
        }
    }
}