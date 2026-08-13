using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class SwimmingController : MonoBehaviour
{
    [Header("Movimiento Acu�tico")]
    [SerializeField] private float swimSpeed = 3f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float buoyancyForce = 0.5f;

    [Header("Ox�geno")]
    [SerializeField] private float maxOxygen = 30f;
    [SerializeField] private float depletionRate = 1f;
    [SerializeField] private float refillRate = 2f;
   
    [Header("Componentes y UI")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private RectTransform airBarRect; // Usamos RectTransform para UI
    [SerializeField] private Image yellowKeyImage;

    [Header("UI de Ox�geno Flotante")]
    [SerializeField] private Image oxygenCircle;
    [SerializeField] private Color fullColor = Color.cyan;
    [SerializeField] private Color lowColor = Color.red;
    [SerializeField] private float dangerThreshold = 0.3f; // 30% de ox�geno

    [Header("Efecto de Luz (Aura)")]
    // 1. Referencia a la luz del aura (debes arrastrarla en el Inspector)
    [SerializeField] private Light2D auraLuz;
    // 2. Definimos los rangos de radio (mínimo y máximo)
    // El radio 'normal' (ej. 5) cuando hay oxígeno lleno
    [SerializeField] private float radioMaximoLuz = 5f;
    // El radio 'crítico' (ej. 1) cuando casi no queda oxígeno
    [SerializeField] private float radioMinimoLuz = 1.2f;

    public bool hasYellowKey;
    private float currentOxygen;
    private Vector2 moveInput;
    private bool isUnderwater = true;
    private float targetSpeed;
    private object scriptJugador;
    private JugadorMuerte scriptMuerte;// Referencia al script de muerte

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentOxygen = maxOxygen;
        // Buscamos el script de muerte en este mismo objeto al iniciar
        scriptMuerte = GetComponent<JugadorMuerte>();
        rb.gravityScale = 0;

        // Inicializamos la UI de la llave amarilla como desactivada
        yellowKeyImage.gameObject.SetActive(false);

        // Inicializamos la luz al máximo
        if (auraLuz != null) auraLuz.pointLightOuterRadius = radioMaximoLuz;
    }

    void Update()
    {
        // 1. Captura de Input (Siempre en Update)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        targetSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : swimSpeed;

        // 2. Gesti�n de ox�geno

        UpdateOxygen();
    }

    void FixedUpdate()
    {
        // 3. Movimiento F�sico (Siempre en FixedUpdate)
        MovePlayer();
        ApplyBuoyancy();
    }

    private void MovePlayer()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            // Aplicar velocidad
            rb.linearVelocity = moveInput.normalized * targetSpeed;

            // Rotaci�n suave
            float targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            float currentAngle = rb.rotation;
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle - 90, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newAngle);
        }
        else
        {
            // Frenado suave si no hay input
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, 5f * Time.fixedDeltaTime);
        }
    }

    private void ApplyBuoyancy()
    {
        if (isUnderwater)
            rb.AddForce(Vector2.up * buoyancyForce, ForceMode2D.Force);
    }

    private void UpdateOxygen()
    {

        float rate = isUnderwater ? -depletionRate : refillRate;
        currentOxygen = Mathf.Clamp(currentOxygen + (rate * Time.deltaTime), 0, maxOxygen);

        // --- NUEVA LÓGICA DE LUZ ---
        if (auraLuz != null)
        {
            // Calculamos la proporción (0 a 1)
            float ratioOxigeno = currentOxygen / maxOxygen;

            // Usamos Mathf.Lerp para calcular el radio intermedio
            // Lerp(valorA, valorB, t) -> Si t es 1, devuelve valorB. Si t es 0, devuelve valorA.
            float nuevoRadio = Mathf.Lerp(radioMinimoLuz, radioMaximoLuz, ratioOxigeno);

            // Aplicamos el radio a la luz
            auraLuz.pointLightOuterRadius = nuevoRadio;

            // Opcional: También reducir la intensidad un poco
            auraLuz.intensity = Mathf.Lerp(0.5f, 1.0f, ratioOxigeno);
        }
        // -----------------------------

        if (oxygenCircle != null)
        {
            // El Fill Amount va de 0 a 1
            float fillRatio = currentOxygen / maxOxygen;
            oxygenCircle.fillAmount = fillRatio;

            // Feedback visual: Cambia a rojo si queda poco
            oxygenCircle.color = (fillRatio < dangerThreshold) ? lowColor : fullColor;

            // Opcional: Ocultar el c�rculo si el ox�geno est� lleno
            oxygenCircle.transform.parent.gameObject.SetActive(currentOxygen < maxOxygen);
        }


        if (currentOxygen <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("RIP: Sin oxígeno");

        if (scriptMuerte != null)
        {
            scriptMuerte.Morir(); // Llamamos a la función que ya creamos antes
        }
        else
        {
            Debug.LogError("No se encontró el script JugadorMuerte en el Player");
        }

        // Opcional: Para que no siga llamando a Die() cada frame después de morir
        this.enabled = false;
    }

    // Método para resetear el oxígeno cuando el jugador reviva
    public void ResetOxigeno()
    {
        currentOxygen = maxOxygen;
        this.enabled = true; // Reactivamos el script para que vuelva a funcionar
        if (auraLuz != null) auraLuz.pointLightOuterRadius = radioMaximoLuz; 
    }
    // Optimizado: Solo detectamos el cambio de estado una vez
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Surface")) isUnderwater = false;
        if (collision.transform.CompareTag("RechargeOx"))
        {
            currentOxygen = +20;
            Debug.Log("Oxígeno recargado al máximo");
            Destroy(collision.gameObject);
        }
                
        if (collision.transform.CompareTag("spike"))
        {
            currentOxygen = -10;
        }

        if (collision.transform.CompareTag("YKey"))
        {
            // 1. Registramos que el jugador ahora tiene la llave
            hasYellowKey = true;

            // 3. Activamos el icono de la llave en el HUD
            if (yellowKeyImage != null)
            {
                // Como ahora es un componente Image, usamos .gameObject para encender el objeto completo
                yellowKeyImage.gameObject.SetActive(true);

                // NOTA: Si en lugar de apagar el objeto, prefieres apagar solo el componente Image, usarías:
                // yellowKeyImage.enabled = true;
            }

            // 2. Destruimos el objeto de la llave en la escena para simular que fue recolectada
            Destroy(collision.gameObject);


        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Surface")) isUnderwater = true;
    }
}