using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Burst : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public AimIndicator AimIndicator;
    public AbilityCooldown AbilityCooldown;

    private float CooldownTimer;
    
    void Update()
    {
        if (CooldownTimer > 0f)
            CooldownTimer -= Time.deltaTime;
    }
    
    public void OnBurst(InputAction.CallbackContext context)
    {
        if (context.started)
            AimIndicator.Show();

        if (context.canceled)
        {
            AimIndicator.Hide();
            if (CooldownTimer <= 0f)
            {
                Fire();
                AbilityCooldown.StartCooldown(GameParameters.BurstCooldown);
                CooldownTimer = GameParameters.BurstCooldown;
            }
        }
    }

    private void Fire()
    {
        Vector3 direction = (MousePosition.WorldPosition - transform.position).normalized;
        direction.y = 0f;

        if (direction == Vector3.zero) return;

        Instantiate(ProjectilePrefab, transform.position, Quaternion.LookRotation(direction));
    }
}