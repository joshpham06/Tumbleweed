using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCooldown : MonoBehaviour
{
    public Image AbilityIcon;
    public Image CooldownOverlay;
    public Text CooldownText;

    private float Time;
    private float remainingTime;
    private bool IsOnCooldown;

    void Update()
    {
        if (!IsOnCooldown) return;

        remainingTime -= UnityEngine.Time.deltaTime;

        if (remainingTime <= 0f)
        {
            IsOnCooldown = false;
            AbilityIcon.color = Color.white;
            CooldownOverlay.fillAmount = 0f;
            CooldownText.text = "";
            return;
        }

        CooldownOverlay.fillAmount = remainingTime / Time;
        CooldownText.text = Mathf.CeilToInt(remainingTime).ToString();
    }

    public void StartCooldown(float cooldownTime)
    {
        Time = cooldownTime;
        remainingTime = cooldownTime;
        IsOnCooldown = true;

        AbilityIcon.color = Color.grey;
        CooldownOverlay.fillAmount = 1f;
    }
}