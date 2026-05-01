using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform Target;
    
    private float RotateSpeed = 500f;
    private float Radius = 0.5f;

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
            print("Distance:  " + Vector3.Distance(transform.position, Target.position) + "   |   Radius: " + Radius);
            Target.GetComponentInParent<IsDamageable>().TakeDamage(GameParameters.ProjectileDamage);
            Destroy(gameObject);
        }
    }
}