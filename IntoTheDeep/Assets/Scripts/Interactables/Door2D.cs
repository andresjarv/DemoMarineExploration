using UnityEngine;

public class Door2D : MonoBehaviour
{
    [SerializeField] private bool disableOnOpen = true; // Si es true, el objeto simplemente se oculta
    [SerializeField] private Vector3 openOffset = new Vector3(3, 0, 0); // Desplazamiento para mover la puerta
    [SerializeField] private float openSpeed = 2f; // Velocidad con la que se mueve la puerta

    private bool isOpen = false;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position + openOffset;
    }

    void Update()
    {
        // Si no se desactiva por completo, se desplaza suavemente al activarse
        if (isOpen && !disableOnOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openSpeed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;

        if (disableOnOpen)
        {
            // Oculta la puerta y elimina su collider para dejar pasar
            gameObject.SetActive(false);
        }
    }
}