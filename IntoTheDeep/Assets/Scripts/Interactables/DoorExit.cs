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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxPuerta);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneFader.Instance.FadeToScene(nextSceneIndex);
        else
            SceneFader.Instance.FadeToScene(0);
    }
}