using UnityEngine;

public class bombMovil : MonoBehaviour
{
    [SerializeField] private Transform[] pointMov;
    [SerializeField] private float VelocityMov;

    private int pointNext = 1;
    private bool order = true;

    private void Update()
    {
        if (order && pointNext + 1 >= pointMov.Length)
        {
            order = false;
        }

        if (!order && pointNext <= 0)
        {
            order = true;
        }

        if (Vector2.Distance(transform.position, pointMov[pointNext].position) < 0.1f)
        {
            if (order)
            {
                pointNext += 1;
            }
            else
            {
                pointNext -= 1;
            }

        }
        transform.position = Vector2.MoveTowards(transform.position, pointMov[pointNext].position,
            VelocityMov * Time.deltaTime);
    }

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
}    
