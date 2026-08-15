using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class DoorExit : MonoBehaviour
{
    [Header("Configuración Visual")]
    [Tooltip("Arrastra aquí el SpriteRenderer de la puerta")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Tooltip("Color o feedback visual cuando la puerta se abre")]
    [SerializeField] private Color openColor = Color.green; // Si tienes un sprite de puerta abierta, cambiaríamos el sprite en lugar del color

    private bool isOpen = false;

    // Esta función la conectaremos en el Inspector al OnAllKeysCollected del LevelManager
    public void OpenDoor()
    {
        isOpen = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = openColor; // Feedback visual
        }

        Debug.Log("¡La puerta se ha abierto!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Solo verificamos colisión si la puerta ya fue abierta por el LevelManager
        if (isOpen && collision.CompareTag("Player"))
        {
            Debug.Log("Nivel completado. Cargando el siguiente...");
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        // Calculamos el índice del siguiente nivel de forma dinámica
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Verificamos si ese nivel existe en la configuración de Unity
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("¡Demo completada! Volviendo al inicio.");
            // Si no hay más niveles, recargamos el primer nivel (o un futuro Menú Principal)
            SceneManager.LoadScene(0);
        }
    }
}