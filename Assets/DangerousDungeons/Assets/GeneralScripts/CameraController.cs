/*
    Autor: Fausto Sánchez Hoya
    Descripción: este código se encarga de posicionar la cámara continuamente en una vista isométrica.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float distancia;
    public GameObject mike;
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
    }

    void Update()
    {
        //La posición de la cámara viene determianda por la posición de Mike tal como se explica en la documentación escrita.
        Vector3 mikePosition = new Vector3(mike.transform.position.x, mike.transform.position.y + distancia, mike.transform.position.z - distancia);
        transform.position = mikePosition;
    }
}
