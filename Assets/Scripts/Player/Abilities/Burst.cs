using UnityEngine;
using UnityEngine.InputSystem;

public class Burst : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public AimIndicator AimIndicator;

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

        GameObject proj = Instantiate(ProjectilePrefab, transform.position, Quaternion.LookRotation(direction));
    }
}