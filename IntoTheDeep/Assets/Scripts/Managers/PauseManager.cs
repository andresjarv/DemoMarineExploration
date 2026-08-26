using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Interfaz")]
    [Tooltip("Arrastra aquí el panel visual de tu Menú de Pausa")]
    [SerializeField] private GameObject panelPausa;

    private bool juegoEnPausa = false;

    void Start()
    {
        // Asegurarnos de que el panel nazca apagado
        if (panelPausa != null) panelPausa.SetActive(false);
    }

    void Update()
    {
        // Al presionar Escape, alternamos entre pausar y reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoEnPausa) ReanudarJuego();
            else PausarJuego();
        }
    }

    public void PausarJuego()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f; // Congela el motor de físicas y el tiempo
        juegoEnPausa = true;
    }

    public void ReanudarJuego()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1f; // Restaura el tiempo a la normalidad
        juegoEnPausa = false;
    }

    public void SalirAlMenuPrincipal()
    {
        // CRÍTICO: Restaurar el tiempo antes de cambiar de escena
        Time.timeScale = 1f;

        // Usamos el sistema de Fade que ya habíamos construido
        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeToScene(0);
        }
    }

    public void SalirAlEscritorio()
    {
        Debug.Log("Cerrando el ejecutable...");
        Application.Quit(); // Esto solo tendrá efecto visual cuando compiles el .exe
    }
}