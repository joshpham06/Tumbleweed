using UnityEngine;
using Pathfinding;

public class MovementParticles : MonoBehaviour
{
    public Transform Target; 
    public AIPath AIPath;
    
    private ParticleSystem Particles;

    void Awake()
    {
        Particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z);
        
        if (AIPath.velocity.sqrMagnitude > 0.1f)
        {
            if (!Particles.isPlaying)
                Particles.Play();
        }
        else
        {
            if (Particles.isPlaying)
                Particles.Stop();
        }
    }
}