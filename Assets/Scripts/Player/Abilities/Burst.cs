using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Burst : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public AimIndicator AimIndicator;

    public float Damage = 10f;

    private GameObject Projectile;
    
    void Update()
    {
        if (Projectile == null)
            return;
        Projectile.transform.position += Projectile.transform.forward * GameParameters.ProjectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponentInParent<IsDamageable>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    public void OnBurst(InputAction.CallbackContext context)
    {
        if (context.started)
            AimIndicator.Show();

        if (context.canceled)
        {
            AimIndicator.Hide();
            Fire();
        }
    }

    private void Fire()
    {
        Vector3 direction = (MousePosition.WorldPosition - transform.position).normalized;
        direction.y = 0f;

        if (direction == Vector3.zero) return;

        Projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.LookRotation(direction));
    }
}