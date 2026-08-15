using UnityEngine;

public class CameraControl: MonoBehaviour
{
    public Transform target;
    
    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.x, transform.position.y);
    }
}
