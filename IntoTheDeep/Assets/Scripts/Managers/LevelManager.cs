using UnityEngine;
using UnityEngine.Events; // Necesario para los eventos del inspector

public class LevelManager : MonoBehaviour
{
    // Patrón Singleton para acceso global rápido
    public static LevelManager Instance { get; private set; }

    [Header("Configuración de Nivel")]
    [Tooltip("¿Cuántas llaves se necesitan para abrir la salida en este nivel?")]
    [SerializeField] private int totalKeysNeeded = 1;

    private int keysCollected = 0;

    [Header("Eventos")]
    [Tooltip("Se ejecuta cuando el jugador recolecta todas las llaves necesarias")]
    public UnityEvent OnAllKeysCollected;

    private void Awake()
    {
        // Configuración básica del Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Hay más de un LevelManager en la escena. Destruyendo el duplicado.");
            Destroy(gameObject);
        }
    }

    // Método que llamarán las llaves al ser tocadas
    public void AddKey()
    {
        keysCollected++;
        Debug.Log($"Llave recogida. Llevas {keysCollected} de {totalKeysNeeded}");

        // Actualizar HUD aquí (puedes conectarlo a tu script de UI luego)

        if (keysCollected >= totalKeysNeeded)
        {
            Debug.Log("¡Todas las llaves recolectadas! Activando eventos de salida...");
            OnAllKeysCollected?.Invoke(); // Dispara lo que sea que hayas configurado en el Inspector
        }
    }
}