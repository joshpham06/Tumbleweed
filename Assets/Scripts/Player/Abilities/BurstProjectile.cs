using UnityEngine;

public class BurstProjectile : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * GameParameters.BurstProjectileSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponentInParent<IsDamageable>()?.TakeDamage(GameParameters.BurstDamage);
            Destroy(gameObject);
        }
    }
}