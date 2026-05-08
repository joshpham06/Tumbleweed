using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardToMove : MonoBehaviour
{
    public AIDestinationSetter DestinationSetter;
    public Transform PlayerDestination;

    public static bool IsMoving;
    private Vector2 Input;

    void Update()
    {
        if (Input.sqrMagnitude < 0.01f) return;

        Vector3 direction = new Vector3(Input.x, 0f, Input.y).normalized;
        PlayerDestination.position = transform.position + direction * 0.5f;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Input = context.ReadValue<Vector2>();
        IsMoving = Input.sqrMagnitude > 0.01f;

        if (context.canceled)
            PlayerDestination.position = transform.position;
    }

    public bool GetIsMoving()
    {
        return IsMoving;
    }
}