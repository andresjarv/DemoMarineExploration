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
    {   // Sobrescribimos la instancia global con el Manager de ESTA escena.
        // Al no usar DontDestroyOnLoad, este objeto morirá de forma natural al salir del nivel,
        // dejando el espacio limpio para el LevelManager del siguiente nivel.
        Instance = this;
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