using Pathfinding;
using UnityEngine;

public class BallRotator : MonoBehaviour
{
    private AIPath PlayerAI;
    private Rigidbody PlayerRigidbody;

    public float rollSpeedMultiplier = 1f;

    void Awake()
    {
        PlayerAI = GetComponent<AIPath>();
        PlayerRigidbody = GetComponent<Rigidbody>();

        PlayerAI.enableRotation = false;
        PlayerAI.maxAcceleration = Mathf.Infinity;
        PlayerAI.slowdownDistance = 0.1f;
        PlayerAI.endReachedDistance = 0.2f;
    }

    void FixedUpdate()
    {
        Vector3 velocity = PlayerAI.desiredVelocity;

        if (velocity.sqrMagnitude < 0.01f) return;

        Vector3 rollAxis = Vector3.Cross(Vector3.up, velocity.normalized);

        float radius = transform.localScale.x * 0.5f;
        float angle = velocity.magnitude * Time.fixedDeltaTime * (180f / Mathf.PI) / radius * rollSpeedMultiplier;

        Quaternion delta = Quaternion.AngleAxis(angle, rollAxis);
        PlayerRigidbody.MoveRotation(delta * PlayerRigidbody.rotation);
    }
}