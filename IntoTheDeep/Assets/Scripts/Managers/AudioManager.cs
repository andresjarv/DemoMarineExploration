using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Fuentes de Audio (Motores)")]
    [Tooltip("El AudioSource que reproducirá los efectos rápidos (SFX)")]
    [SerializeField] private AudioSource sfxSource;

    [Tooltip("El AudioSource que reproducirá la música en bucle (BGM)")]
    [SerializeField] private AudioSource musicSource; // <-- NUEVO CANAL

    [Header("Clips de Sonido (Efectos)")]
    public AudioClip sfxOxigeno;
    public AudioClip sfxLlave;
    public AudioClip sfxPuerta;
    public AudioClip sfxButton;
    public AudioClip sfxExplotion;

    [Header("Clips de Sonido (Música)")]
    public AudioClip bgmFondo; // <-- TU PISTA DE FONDO

    private void Awake()
    {
        // Patrón Singleton Persistente
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Al nacer el Manager, le decimos al motor de música que arranque
        if (bgmFondo != null && musicSource != null)
        {
            musicSource.clip = bgmFondo;
            musicSource.loop = true; // Crucial: Obligamos a que se repita infinitamente
            musicSource.Play();
        }
    }

    // Cualquier script de tu juego puede llamar a esta función
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            // PlayOneShot reproduce el sonido sin interrumpir otros efectos
            sfxSource.PlayOneShot(clip);
        }
    }
}