using UnityEngine;

public class bombMovil : MonoBehaviour
{
    [SerializeField] private Transform[] pointMov;
    [SerializeField] private float VelocityMov;

    private int pointNext = 1;
    private bool order = true;

    private void Update()
    {
        if (order && pointNext + 1 >= pointMov.Length)
        {
            order = false;
        }

        if (!order && pointNext <= 0)
        {
            order = true;
        }

        if (Vector2.Distance(transform.position, pointMov[pointNext].position) < 0.1f)
        {
            if (order)
            {
                pointNext += 1;
            }
            else
            {
                pointNext -= 1;
            }

        }
        transform.position = Vector2.MoveTowards(transform.position, pointMov[pointNext].position,
            VelocityMov * Time.deltaTime);
    }
}    
