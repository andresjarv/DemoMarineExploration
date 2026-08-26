using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LightDroneController : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Arrastra aquí el Transform de tu Jugador")]
    [SerializeField] private Transform jugador;

    [Header("Ajustes de Velocidad")]
    [SerializeField] private float velocidadVuelo = 12f;
    [Tooltip("A qué distancia del jugador flotará cuando no uses el mouse")]
    [SerializeField] private float alturaReposo = 1.5f;

    private Camera camaraPrincipal;
    private Rigidbody2D rb;
    private Vector2 posicionDestino;

    void Start()
    {
        camaraPrincipal = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        // Medida de seguridad: asegurarnos de que no caiga por la gravedad
        rb.gravityScale = 0f;
        // Para que se mueva por código pero choque con paredes
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    // Update lee los controles del jugador (se ejecuta cada frame visual)
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 posicionMouse = Input.mousePosition;
            posicionDestino = camaraPrincipal.ScreenToWorldPoint(posicionMouse);
        }
        else
        {
            posicionDestino = (Vector2)jugador.position + new Vector2(0, alturaReposo);
        }
    }

    // FixedUpdate aplica las físicas (se ejecuta sincronizado con el motor de colisiones)
    void FixedUpdate()
    {
        // MoveTowards calcula el camino paso a paso
        Vector2 nuevaPosicion = Vector2.MoveTowards(rb.position, posicionDestino, velocidadVuelo * Time.fixedDeltaTime);

        // MovePosition le dice al motor de físicas: "Avanza hacia allá, pero frena si hay un muro"
        rb.MovePosition(nuevaPosicion);
    }
}