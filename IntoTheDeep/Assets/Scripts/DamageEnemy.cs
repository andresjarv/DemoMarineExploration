using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    // Esta función se llama cuando algo entra en el colisionador del enemigo (si es Trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Algo entró en el trigger: " + other.name);
        // Verificamos si lo que colisionó tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Intentamos obtener el componente 'JugadorMuerte' del jugador
            JugadorMuerte scriptJugador = other.GetComponent<JugadorMuerte>();

            // Si el jugador tiene el script, llamamos a su función de muerte
            if (scriptJugador != null)
            {
                Debug.Log("Jugador detectado, llamando a Morir()");
                scriptJugador.Morir();
            }
            else
            {
                Debug.LogWarning("El objeto con etiqueta 'Player' no tiene el componente 'JugadorMuerte'");
            }

        }
    }

    public class EnemigoSalud : MonoBehaviour
    {
        public float vida = 50f;

        public void RecibirDanio(float cantidad)
        {
            vida -= cantidad;
            Debug.Log("Enemigo herido. Vida restante: " + vida);

            if (vida <= 0)
            {
                Morir();
            }
        }

        void Morir()
        {
            Debug.Log("Enemigo derrotado");
            Destroy(gameObject);
        }
    }

    // Si tu colisionador NO es Trigger, usa esta función en su lugar:
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JugadorMuerte scriptJugador = collision.gameObject.GetComponent<JugadorMuerte>();
            if (scriptJugador != null)
            {
                scriptJugador.Morir();
            }
        }
    }
    */
}