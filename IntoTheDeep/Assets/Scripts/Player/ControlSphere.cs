using UnityEngine;


public class ControlSphere : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float velocidad = 12f;
    [SerializeField] private Camera cam;
    [SerializeField] private float fuerzaSeguimiento = 50f;

    private Rigidbody2D rb;
    private Vector2 posicionObjetivo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (cam == null)
            cam = Camera.main;

        // Oculta el cursor y lo mantiene dentro de la pantalla del juego
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        // Obtener posición del mouse ajustando la profundidad Z de la cámara
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Mathf.Abs(cam.transform.position.z);

        posicionObjetivo = cam.ScreenToWorldPoint(mouseScreen);
    }

    void FixedUpdate()
    {
        //// Mueve la esfera suavemente usando el motor de físicas
        //Vector2 nuevaPos = Vector2.MoveTowards(rb.position, posicionObjetivo, velocidad * Time.fixedDeltaTime);
        //rb.MovePosition(nuevaPos);
        Vector2 direccion = (posicionObjetivo - rb.position);

        // Aplica fuerza proporcional a la distancia del cursor
        rb.AddForce(direccion * fuerzaSeguimiento);

        // Aplica rozamiento/resistencia para que no acelere infinitamente
        rb.linearVelocity *= 0.9f; // O ajusta el 'Linear Drag' directamente en el Inspector
    }
}