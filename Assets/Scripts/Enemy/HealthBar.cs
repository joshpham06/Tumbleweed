using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Fill;
    
    private float MaxHealth;

    public void Initialize(float maxHealth)
    {
        MaxHealth = maxHealth;
        SetHealth(MaxHealth);
    }
    public void SetHealth(float currentHealth)
    {
        Fill.fillAmount = currentHealth / MaxHealth;
    }
}