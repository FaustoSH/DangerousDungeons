using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float distancia;
    public GameObject mike;
    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mikePosition = new Vector3(mike.transform.position.x, mike.transform.position.y + distancia, mike.transform.position.z - distancia);
        transform.position = mikePosition;
    }
}
