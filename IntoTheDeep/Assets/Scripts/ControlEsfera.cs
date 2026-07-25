using System.Collections;
using UnityEngine;
using static DamageEnemy;

public class ControlCirculoMultifuncional : MonoBehaviour
{
    public enum EstadoEsfera { Normal, Lanza, Escudo }

    [Header("Estados y Sprites")]
    public EstadoEsfera estadoActual = EstadoEsfera.Normal;
    public Sprite spriteNormal;
    public Sprite spriteLanza;
    public Sprite spriteEscudo;

    [Header("Configuración de Movimiento")]
    public float velocidadSeguimiento = 15f;
    public float suavizadoRotacion = 10f;

    [Header("Ataque Lanza")]
    public float fuerzaEstocada = 5f;
    public float duracionEstocada = 0.15f;
    public float danioLanza = 25f;
    private bool estaAtacando = false;

    [Header("Habilidades")]
    public float cooldownEscudo = 3f;
    private bool esModoLanza = true;
    private bool escudoDisponible = true;

    private SpriteRenderer sRenderer;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Solo seguimos al cursor si no estamos en medio de una estocada
        if (!estaAtacando)
        {
            MoverHaciaCursor();
        }

        GestionarInputs();
    }

    void MoverHaciaCursor()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, velocidadSeguimiento * Time.deltaTime);

        if (esModoLanza)
        {
            Vector2 direccion = (Vector2)mousePos - (Vector2)transform.position;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angulo - 90f), suavizadoRotacion * Time.deltaTime);
        }
    }

    void GestionarInputs()
    {
        // Toggle Lanza (Tecla 1)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            estadoActual = (estadoActual == EstadoEsfera.Lanza) ? EstadoEsfera.Normal : EstadoEsfera.Lanza;
            ActualizarVisuales();
        }

        // Toggle Escudo (Tecla 2)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            estadoActual = (estadoActual == EstadoEsfera.Escudo) ? EstadoEsfera.Normal : EstadoEsfera.Escudo;
            ActualizarVisuales();
        }

        // Acción Clic Izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            if (estadoActual == EstadoEsfera.Lanza && !estaAtacando) StartCoroutine(EjecutarEstocada());
            else if (estadoActual == EstadoEsfera.Escudo && escudoDisponible) StartCoroutine(ActivarEscudo());
        }
    }

    void ActualizarVisuales()
    {
        switch (estadoActual)
        {
            case EstadoEsfera.Normal:
                sRenderer.sprite = spriteNormal;
                transform.localScale = Vector3.one;
                break;
            case EstadoEsfera.Lanza:
                sRenderer.sprite = spriteLanza;
                // Ajusta escala si tu sprite es muy pequeño/grande
                break;
            case EstadoEsfera.Escudo:
                sRenderer.sprite = spriteEscudo;
                break;
        }
    }

    IEnumerator EjecutarEstocada()
    {
        estaAtacando = true;
        Vector3 posicionInicial = transform.position;
        // Calculamos la dirección hacia adelante de la lanza
        Vector3 direccionAtaque = transform.up;
        Vector3 posicionFinal = posicionInicial + (direccionAtaque * fuerzaEstocada);

        float tiempo = 0;
        while (tiempo < duracionEstocada)
        {
            transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempo / duracionEstocada);
            tiempo += Time.deltaTime;
            yield return null;
        }

        estaAtacando = false;
    }

    // DETECCIÓN DE DAÑO
    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (esModoLanza && estaAtacando)
        {
            // Buscamos si el objeto tiene el script de salud
            EnemigoSalud enemigo = otro.GetComponent<EnemigoSalud>();
            if (enemigo != null)
            {
                enemigo.RecibirDanio(danioLanza);
            }
        }
    }

    IEnumerator ActivarEscudo()
    {
        escudoDisponible = false;
        sRenderer.color = new Color(0.5f, 0.5f, 1f, 0.5f);
        yield return new WaitForSeconds(cooldownEscudo);
        sRenderer.color = Color.white;
        escudoDisponible = true;
    }
}