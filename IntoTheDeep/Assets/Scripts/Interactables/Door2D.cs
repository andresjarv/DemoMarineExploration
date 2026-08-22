using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Door2D : MonoBehaviour
{
    [Header("Estilo de Apertura")]
    [Tooltip("Si está activo, la puerta se hace transparente. Si está apagado, se desliza.")]
    [SerializeField] private bool modoDesvanecer = true;

    [Header("Ajustes de Desvanecimiento")]
    [SerializeField] private float velocidadFade = 2f;

    [Header("Ajustes de Deslizamiento (Alternativa)")]
    [SerializeField] private Vector3 openOffset = new Vector3(3, 0, 0);
    [SerializeField] private float openSpeed = 2f;

    private SpriteRenderer spriteRenderer;
    private Collider2D doorCollider;
    private bool isOpen = false;
    private Vector3 targetPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<Collider2D>();
        targetPosition = transform.position + openOffset;
    }

    void Update()
    {
        // Si la puerta está abierta y decidimos usar el modo físico (deslizamiento)
        if (isOpen && !modoDesvanecer)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openSpeed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        if (isOpen) return;
        isOpen = true;

        // CRUCIAL: Apagamos la colisión físicamente en el frame cero para permitir el paso,
        // sin importar cuánto tarde la animación visual en completarse.
        if (doorCollider != null)
            doorCollider.enabled = false;

        if (modoDesvanecer)
        {
            StartCoroutine(FadeOutRoutine());
        }
    }

    private IEnumerator FadeOutRoutine()
    {
        // Guardamos el color actual de la puerta
        Color color = spriteRenderer.color;

        // Mientras el canal Alfa (transparencia) sea mayor a cero, lo vamos restando
        while (color.a > 0)
        {
            color.a -= Time.deltaTime * velocidadFade;
            spriteRenderer.color = color;
            yield return null; // Esperamos al siguiente frame
        }

        // Al terminar el efecto visual, desactivamos el objeto para ahorrar memoria
        gameObject.SetActive(false);
    }
}