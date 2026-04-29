using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Fill;
    public Transform Target;
    
    private float MaxHealth;

    public void Initialize(float maxHealth)
    {
        MaxHealth = maxHealth;
        SetHealth(MaxHealth);
    }
    
    void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z);
    }
    
    public void SetHealth(float currentHealth)
    {
        Fill.fillAmount = currentHealth / MaxHealth;
    }

    public void Show()
    {
        
    }
    
    public void Hide()
    {
        
    }
}