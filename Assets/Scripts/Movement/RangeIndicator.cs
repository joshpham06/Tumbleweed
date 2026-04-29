using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public Transform Target;

    public void Initialize(float attackRange)
    {
        float diameter = attackRange * 2f;
        transform.localScale = new Vector3(diameter, diameter, 1f);
    }
    
    void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x, 0.01f, Target.position.z);
    }
}