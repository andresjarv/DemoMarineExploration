using UnityEngine;

public class ControlLuzOrganica : MonoBehaviour
{
    [Header("Seguimiento y Fricción")]
    [Tooltip("Velocidad máxima a la que puede viajar la luz")]
    public float velocidadMaxima = 8f;
    [Tooltip("Cuánto tarda en alcanzar la velocidad máxima (simula la resistencia)")]
    public float tiempoAceleracion = 0.4f;
    private Vector3 velocidadActual;

    [Header("Efecto de Corriente (Flotabilidad)")]
    [Tooltip("Qué tanto sube y baja la luz orgánicamente")]
    public float amplitudOscilacion = 0.15f;
    [Tooltip("Qué tan rápido ocurre esta oscilación")]
    public float velocidadOscilacion = 2f;

    [Header("Deriva Aleatoria (Ruido)")]
    [Tooltip("Fuerza de las pequeñas corrientes irregulares")]
    public float intensidadRuido = 0.2f;
    [Tooltip("Velocidad a la que cambian estas corrientes")]
    public float velocidadRuido = 0.8f;

    private Camera cam;
    private Vector2 offsetAleatorio; // Para que el ruido no sea siempre idéntico

    void Start()
    {
        cam = Camera.main;
        // Generamos un punto de inicio aleatorio para el mapa de ruido
        offsetAleatorio = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // 1. Inercia y fricción
        // SmoothDamp crea una aceleración y desaceleración natural, como moverse en un medio denso.
        Vector3 posicionBase = Vector3.SmoothDamp(transform.position, mousePos, ref velocidadActual, tiempoAceleracion, velocidadMaxima);

        // 2. Flotabilidad rítmica
        // El seno genera un movimiento suave de arriba hacia abajo constante.
        float oscilacionY = Mathf.Sin(Time.time * velocidadOscilacion) * amplitudOscilacion;

        // 3. Corrientes impredecibles
        // El ruido de Perlin genera valores orgánicos continuos (no saltos bruscos) para desviar sutilmente el eje X e Y.
        float ruidoX = (Mathf.PerlinNoise(Time.time * velocidadRuido + offsetAleatorio.x, 0) - 0.5f) * intensidadRuido;
        float ruidoY = (Mathf.PerlinNoise(0, Time.time * velocidadRuido + offsetAleatorio.y) - 0.5f) * intensidadRuido;

        // Aplicamos la posición base calculada más los modificadores ambientales
        transform.position = posicionBase + new Vector3(ruidoX, oscilacionY + ruidoY, 0f);
    }
}