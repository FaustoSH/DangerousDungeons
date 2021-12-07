/*
    Autor: Fausto S�nchez Hoya
    Descripci�n: este c�digo se encarga de posicionar la c�mara continuamente en una vista isom�trica.
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
        //La posici�n de la c�mara viene determianda por la posici�n de Mike tal como se explica en la documentaci�n escrita.
        Vector3 mikePosition = new Vector3(mike.transform.position.x, mike.transform.position.y + distancia, mike.transform.position.z - distancia);
        transform.position = mikePosition;
    }
}
