using UnityEngine;
using UnityEngine.SceneManagement; // Crucial para el manejo de escenas

public class GameOverManager : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Arrastra aquí el panel de Game Over (el objeto padre)")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Scripts a desactivar")]
    [Tooltip("Arrastra aquí al Player para apagar su script de movimiento")]
    [SerializeField] private MonoBehaviour playerMovementScript;

    [Tooltip("Arrastra aquí a la Esfera para apagar su seguimiento")]
    [SerializeField] private MonoBehaviour sphereControlScript;

    private void Start()
    {
        // Nos aseguramos de que el panel esté apagado al arrancar el nivel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // El script del oxígeno llamará a esta función cuando llegue a 0
    public void TriggerGameOver()
    {
        Debug.Log("Oxígeno agotado: Iniciando Game Over");

        // 1. Encendemos la interfaz
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // 2. Apagamos el input del jugador (las animaciones y partículas seguirán)
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        // 3. Apagamos el seguimiento de la esfera de luz
        if (sphereControlScript != null)
        {
            sphereControlScript.enabled = false;
        }
    }

    // Esta es la función que conectaremos al Botón de tu UI
    public void RestartLevel()
    {
        Debug.Log("Recargando escena...");
        // Recarga dinámicamente la escena en la que estemos (sirve para Nivel 1, 2 o 3)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
