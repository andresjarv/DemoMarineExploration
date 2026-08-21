using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    // Patrón Singleton: Una única instancia global accesible desde cualquier script
    public static SceneFader Instance;

    [Header("Configuración")]
    [Tooltip("El CanvasGroup que controla la opacidad de la pantalla negra")]
    [SerializeField] private CanvasGroup fadeGroup;
    [Tooltip("Velocidad a la que la pantalla se oscurece/aclara")]
    [SerializeField] private float fadeSpeed = 2f;

    private void Awake()
    {
        // Configuramos el Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Al cargar CUALQUIER escena, la pantalla empieza negra y se aclara suavemente
        if (fadeGroup != null)
        {
            fadeGroup.alpha = 1f;
            StartCoroutine(FadeIn());
        }
    }

    // Esta es la función que llamarán tus puertas o botones
    public void FadeToScene(int sceneIndex)
    {
        StartCoroutine(FadeOutAndLoad(sceneIndex));
    }

    private IEnumerator FadeIn()
    {
        // Mientras la opacidad sea mayor a 0, la vamos restando
        while (fadeGroup.alpha > 0)
        {
            fadeGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null; // Espera al siguiente frame
        }

        // Cuando termina de aclarar, apagamos el bloqueo para que el jugador pueda hacer clic
        fadeGroup.blocksRaycasts = false;
    }

    private IEnumerator FadeOutAndLoad(int sceneIndex)
    {
        // Bloqueamos los clics para que el jugador no active cosas mientras la pantalla se apaga
        fadeGroup.blocksRaycasts = true;

        // Mientras la opacidad sea menor a 1, la vamos sumando
        while (fadeGroup.alpha < 1)
        {
            fadeGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // Cuando la pantalla está totalmente negra, cargamos la escena
        SceneManager.LoadScene(sceneIndex);
    }
}