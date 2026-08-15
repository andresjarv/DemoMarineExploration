using System;
using UnityEngine;

public class PressableButton2D : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Door2D targetDoor; // Referencia a la puerta que va a abrir

    [Header("Button Settings")]
    [SerializeField] private string sphereTag = "Sphere"; // Tag del objeto que puede presionar el botón
    [SerializeField] private Color pressedColor = Color.green; // Feedback visual al presionar

    private SpriteRenderer spriteRenderer;
    private bool isPressed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Esto imprimirá en la Consola CUALQUIER objeto 2D que toque el botón
        Debug.Log("Objeto detectado por el botón: " + collision.gameObject.name + " | Tag: " + collision.tag);

        if (!isPressed && collision.CompareTag(sphereTag))
        {
            isPressed = true;

            if (spriteRenderer != null)
                spriteRenderer.color = pressedColor;

            if (targetDoor != null)
            {
                targetDoor.OpenDoor();
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // Verifica si lo que entra en contacto es el jugador
    //    if (!isPressed && collision.CompareTag("Sphere"))
    //    {
    //        Console.WriteLine("Botón presionado por: " + collision.name);
    //        isPressed = true;

    //        // Cambia el color del botón para dar feedback visual
    //        if (spriteRenderer != null)
    //            spriteRenderer.color = pressedColor;

    //        // Notifica a la puerta que debe abrirse
    //        if (targetDoor != null)
    //        {
    //            targetDoor.OpenDoor();
    //        }
    //    }
    //}
}