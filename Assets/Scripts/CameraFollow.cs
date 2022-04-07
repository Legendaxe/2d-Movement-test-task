using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] protected GameObject player;


    private void LateUpdate()
    {
        Vector3 temp = (transform.position);
        temp.x = player.transform.position.x;
        temp.y = player.transform.position.y;
        transform.position = temp;
    }
}
