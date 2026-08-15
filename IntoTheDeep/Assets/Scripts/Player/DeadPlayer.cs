using UnityEngine;




public class DeadPlayer : MonoBehaviour
{
    private Vector2 posicionInicial; // Para guardar la posición donde empieza

    private void Start()
    {
        // Al inicio, guardamos la posición actual como la posición de respawn
        posicionInicial = transform.position;
    }

    // Esta función es llamada por el enemigo
    public void Die()
    {
        Debug.Log("¡El jugador ha muerto!");

        // 1. Mostrar la pantalla de Game Over
        GameOverManager.Instancia.MostrarGameOver();

        // 2. Desactivar el control del jugador (opcional pero recomendado)
        // Si tienes un script de movimiento, obtén su referencia y desactívalo.
        GetComponent<SwimmingController>().enabled = false;

        GetComponent<ControlSphere>().enabled = false; 

        // 3. Resetear la posición (Opcional: Esperar unos segundos antes)
        // Para hacerlo instantáneo:
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.position = posicionInicial;
        // Reactivar el movimiento si lo desactivaste
        GetComponent<SwimmingController>().enabled = true;
    }
}