using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshPro Text;
    public float FloatSpeed = 1f;
    public float FadeDuration = 1f;

    private float Timer;
    private Camera Camera;

    void Awake()
    {
        Camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.rotation = Camera.transform.rotation;
    }
    
    public void Initialize(string text, Color color)
    {
        Text.text = text;
        Text.color = color;
    }

    void Update()
    {
        Timer += Time.deltaTime;
        transform.position += Vector3.up * FloatSpeed * Time.deltaTime;
        
        float alpha = Mathf.Lerp(1f, 0f, Timer / FadeDuration);
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, alpha);

        if (Timer >= FadeDuration)
            Destroy(gameObject);
    }
}