using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    // Singleton
    public static GameOverManager Instancia { get; private set; }

    [SerializeField] private GameObject GameOverPanel; // Referencia al panel de UI que creamos

    private void Awake()
    {
        // Configurar el Singleton
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject); // Para que no se destruya al cambiar de escena si añades más niveles
        }
    }

    private void Start()
    {
        // Asegurarnos de que el panel está oculto al empezar
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(false);
        }
    }

    public void MostrarGameOver()
    {
        if (GameOverPanel != null)
        {
            Debug.Log("Intentando mostrar panel");
            GameOverPanel.SetActive(true);
            // Puedes añadir sonido de muerte aquí
            // Time.timeScale = 0; // Pausar el juego si quieres (tendrás que gestionarlo para el reinicio)
        }
        else
        {
            Debug.LogError("No se ha asignado el Panel de Game Over en el GameOverManager");
        }
    }

    // Función para el botón de Reiniciar (opcional)
    public void ReiniciarNivel()
    {
        // Aquí podrías recargar la escena actual, o usar la lógica de resetear posición que ya hicimos.
        // Si reinicias posición, no olvides ocultar el panel de nuevo y reactivar el tiempo si lo pausaste.
        GameOverPanel.SetActive(false);
        Time.timeScale = 1;

        // O, para recargar la escena (necesitas using UnityEngine.SceneManagement;):
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}