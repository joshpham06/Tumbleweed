using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    public Transform Player;
    
    public float Range;

    void Awake()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * Range, transform.localScale.z);
        Show();
    }

    void Update()
    {
        Vector3 direction = (MousePosition.WorldPosition - Player.position).normalized;
        direction.y = 0f;

        Vector3 midPoint = Player.position + direction * (Range * 0.5f);
        transform.position = new Vector3(midPoint.x, 0.01f, midPoint.z);
    
        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90f, 0f, 0f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
