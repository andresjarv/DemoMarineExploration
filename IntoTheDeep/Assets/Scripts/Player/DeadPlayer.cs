using UnityEngine;

public class DeadPlayer : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Arrastra aquí el objeto que tiene el script GameOverManager")]
    [SerializeField] private GameOverManager gameOverManager;

    // Esta función es llamada por las bombas, trampas o el oxígeno al llegar a 0
    public void Die()
    {
        Debug.Log("¡El jugador ha muerto! Avisando al Manager...");

        if (gameOverManager != null)
        {
            // Le pasamos toda la responsabilidad de apagar scripts y mostrar UI al Manager
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogError("¡Cuidado! No has asignado el GameOverManager en el inspector del Player.");
        }
    }
}