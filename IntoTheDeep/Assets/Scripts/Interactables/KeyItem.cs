using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KeyItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 1. Avisamos al Manager. 
            // EL MANAGER es ahora el único responsable de encender el icono correcto en el HUD.
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.AddKey();
            }

            // 2. Reproducimos el sonido global
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxLlave);
            }

            // 3. Opcional: Instanciar aquí el prefab de partículas de recolección

            // 4. Destruimos el objeto físico de la escena
            Destroy(gameObject);
        }
    }
}