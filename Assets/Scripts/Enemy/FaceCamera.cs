using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera Camera;

    void Awake()
    {
        Camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.rotation = Camera.transform.rotation;
    }
}