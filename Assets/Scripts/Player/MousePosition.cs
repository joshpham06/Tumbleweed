using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public static Vector3 WorldPosition;
    public LayerMask GroundLayer;

    private Camera Camera;

    void Awake()
    {
        Camera = Camera.main;
    }

    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, GroundLayer))
            WorldPosition = hit.point;
    }

    public Vector3 GetWorldPosition()
    {
        return WorldPosition;
    }

    public void SetMousePosition(Vector3 position)
    {
        WorldPosition = position;
    }
}