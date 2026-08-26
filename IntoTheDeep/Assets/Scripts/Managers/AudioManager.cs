using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Fuentes de Audio")]
    [Tooltip("El AudioSource que reproducirá los efectos (SFX)")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips de Sonido (Arrastra tus .wav aquí)")]
    public AudioClip sfxOxigeno;
    public AudioClip sfxLlave;
    public AudioClip sfxPuerta;

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

    // Cualquier script de tu juego puede llamar a esta función
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            // PlayOneShot reproduce el sonido sin interrumpir otros efectos que estén sonando
            sfxSource.PlayOneShot(clip);
        }
    }
}