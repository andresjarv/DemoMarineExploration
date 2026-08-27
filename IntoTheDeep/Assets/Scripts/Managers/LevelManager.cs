using UnityEngine;
using UnityEngine.UI; // CRUCIAL: Necesario para manipular elementos del Canvas
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    // Patrón Singleton para acceso global rápido
    public static LevelManager Instance { get; private set; }

    [Header("Configuración de Nivel")]
    [Tooltip("¿Cuántas llaves se necesitan para abrir la salida en este nivel?")]
    [SerializeField] private int totalKeysNeeded = 1;
    private int keysCollected = 0;

    [Header("Interfaz Visual (HUD)")]
    [Tooltip("Arrastra aquí las 3 imágenes (Iconos) desde el Canvas")]
    [SerializeField] private Image[] keyIcons;
    [Tooltip("El sprite de la llave recolectada (a color)")]
    [SerializeField] private Sprite filledKeySprite;
    [Tooltip("El sprite de la llave pendiente (silueta/oscura)")]
    [SerializeField] private Sprite emptyKeySprite;

    [Header("Eventos")]
    [Tooltip("Se ejecuta cuando el jugador recolecta todas las llaves necesarias")]
    public UnityEvent OnAllKeysCollected;

    private void Awake()
    {
        // Sobrescribimos la instancia global con el Manager de ESTA escena.
        Instance = this;
    }

    private void Start()
    {
        // Inicializamos la interfaz para que muestre las siluetas correctas nada más empezar
        UpdateKeyUI();
    }

    // Método que llamarán las llaves al ser tocadas
    public void AddKey()
    {
        keysCollected++;
        Debug.Log($"Llave recogida. Llevas {keysCollected} de {totalKeysNeeded}");

        // Actualizamos las siluetas del HUD
        UpdateKeyUI();

        if (keysCollected >= totalKeysNeeded)
        {
            Debug.Log("¡Todas las llaves recolectadas! Activando eventos de salida...");
            OnAllKeysCollected?.Invoke();
        }
    }

    // Lógica para actualizar las imágenes en pantalla
    private void UpdateKeyUI()
    {
        // Medida de seguridad: Si olvidaste conectar las imágenes en el Inspector, el juego no se rompe
        if (keyIcons == null || keyIcons.Length == 0) return;

        for (int i = 0; i < keyIcons.Length; i++)
        {
            // Evaluamos si este icono es requerido para este nivel (ej. en Nivel 1 solo pide 1 llave, i = 0)
            if (i < totalKeysNeeded)
            {
                keyIcons[i].gameObject.SetActive(true);

                // Si el jugador ya recogió esta llave, le ponemos el sprite a color; si no, la silueta oscura
                keyIcons[i].sprite = (i < keysCollected) ? filledKeySprite : emptyKeySprite;

                // Restauramos el color blanco por defecto para que la imagen original se vea correctamente
                keyIcons[i].color = Color.white;
            }
            else
            {
                // Si el nivel pide menos de 3 llaves, apagamos los iconos sobrantes para que no confundan
                keyIcons[i].gameObject.SetActive(false);
            }
        }
    }
}