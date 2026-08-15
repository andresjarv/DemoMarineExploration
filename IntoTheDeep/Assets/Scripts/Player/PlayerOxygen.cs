using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerOxygen : MonoBehaviour
{
    [Header("Parámetros de Oxígeno")]
    [SerializeField] private float maxOxygen = 30f;
    [SerializeField] private float depletionRate = 1f;

    [Header("UI de Oxígeno")]
    [SerializeField] private Image oxygenCircle;
    [SerializeField] private Color fullColor = Color.cyan;
    [SerializeField] private Color lowColor = Color.red;
    [SerializeField] private float dangerThreshold = 0.3f;

    [Header("Efecto de Luz")]
    [SerializeField] private Light2D auraLuz;
    [SerializeField] private float radioMaximoLuz = 1.5f;
    [SerializeField] private float radioMinimoLuz = 0.2f;

    private float currentOxygen;
    private DeadPlayer scriptDead;

    void Awake()
    {
        currentOxygen = maxOxygen;
        scriptDead = GetComponent<DeadPlayer>();

        if (auraLuz != null) auraLuz.pointLightOuterRadius = radioMaximoLuz;
    }

    void Update()
    {
        UpdateOxygen();
    }

    private void UpdateOxygen()
    {
        // Consumir oxígeno constantemente
        currentOxygen = Mathf.Clamp(currentOxygen - (depletionRate * Time.deltaTime), 0, maxOxygen);

        // Actualizar Luz
        if (auraLuz != null)
        {
            float ratioOxigeno = currentOxygen / maxOxygen;
            auraLuz.pointLightOuterRadius = Mathf.Lerp(radioMinimoLuz, radioMaximoLuz, ratioOxigeno);
            auraLuz.intensity = Mathf.Lerp(0.5f, 1.0f, ratioOxigeno);
        }

        // Actualizar UI
        if (oxygenCircle != null)
        {
            float fillRatio = currentOxygen / maxOxygen;
            oxygenCircle.fillAmount = fillRatio;
            oxygenCircle.color = (fillRatio < dangerThreshold) ? lowColor : fullColor;
            oxygenCircle.transform.parent.gameObject.SetActive(currentOxygen < maxOxygen);
        }

        // Condición de Muerte
        if (currentOxygen <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (scriptDead != null)
        {
            scriptDead.Die();
        }
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // BUG CORREGIDO: Se usa += y -=, y se asegura de no pasar del máximo o mínimo usando Clamp
        if (collision.transform.CompareTag("RechargeOx"))
        {
            currentOxygen = Mathf.Clamp(currentOxygen + 20f, 0, maxOxygen);
            Debug.Log("Oxígeno sumado. Nivel actual: " + currentOxygen);
            Destroy(collision.gameObject);
        }

        if (collision.transform.CompareTag("spike"))
        {
            currentOxygen = Mathf.Clamp(currentOxygen - 10f, 0, maxOxygen);
            Debug.Log("Daño por trampa. Nivel actual: " + currentOxygen);
        }
    }
}