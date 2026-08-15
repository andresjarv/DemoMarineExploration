using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class KeyItem : MonoBehaviour
{
    [SerializeField] private Image yellowKeyImage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Avisar al manager
            LevelManager.Instance.AddKey();

            //Opcional: particulas de recoleccion            

            //Activamos el icono de la llave en el HUD
            if (yellowKeyImage != null)
            {
                // Como ahora es un componente Image, usamos .gameObject para encender el objeto completo
                yellowKeyImage.gameObject.SetActive(true);
                
            }

            //Opcional: particulas de recoleccion

            //Destruimos el objeto de la llave en la escena para simular que fue recolectada
            Destroy(gameObject);

        }        

        
    }

}
