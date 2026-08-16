using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Light2D))]
public class SphereLightCollider : MonoBehaviour
{
    private CircleCollider2D sphereCollider;
    private Light2D light2D;

    private void Awake()
    {
        sphereCollider = GetComponent<CircleCollider2D>();
        light2D = GetComponent<Light2D>();

        // Aseguramos que sea un trigger para que no choque con las paredes
        sphereCollider.isTrigger = true;
    }

    private void Update()
    {
        // El radio del collider será siempre igual al radio de la luz
        sphereCollider.radius = light2D.pointLightOuterRadius;
    }
}