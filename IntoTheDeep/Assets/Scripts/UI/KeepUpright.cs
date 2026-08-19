
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeepUpright : MonoBehaviour
{
    void LateUpdate()
    {
        // Esto mantiene la UI siempre mirando hacia arriba independientemente del jugador
        transform.rotation = Quaternion.identity;
    }
}