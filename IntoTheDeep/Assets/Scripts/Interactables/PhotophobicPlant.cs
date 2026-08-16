using UnityEngine;

public class PhotophobicPlant : MonoBehaviour
{
    [Header("Configuración Visual")]
    [Tooltip("Velocidad a la que la planta se encoge al darle la luz")]
    [SerializeField] private float shrinkSpeed = 5f;
    [Tooltip("El tamaño que tendrá cuando la luz le pegue (ej. muy pequeña en Y)")]
    [SerializeField] private Vector3 minScale = new Vector3(1f, 0.1f, 1f); // Se aplasta hacia abajo

    [Header("Referencias")]
    [Tooltip("El collider que impide el paso del jugador")]
    [SerializeField] private Collider2D blockerCollider;

    private Vector3 originalScale;
    private bool isIlluminated = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // 1. Animación suave de cambio de tamaño
        Vector3 targetScale = isIlluminated ? minScale : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * shrinkSpeed);

        // 2. Activar/Desactivar el bloqueo físico
        if (blockerCollider != null)
        {
            // Si la planta está casi en su tamaño mínimo, apagamos el collider para dejar pasar al jugador
            float distanceToMin = Vector3.Distance(transform.localScale, minScale);
            blockerCollider.enabled = distanceToMin > 0.15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si lo que entró al área es la esfera
        if (collision.CompareTag("LightSphere"))
        {
            isIlluminated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LightSphere"))
        {
            isIlluminated = false;
        }
    }
}