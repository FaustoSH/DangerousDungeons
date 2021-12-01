using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int radio;
    private float contador;
    private int semilla;
    // Start is called before the first frame update
    void Start()
    {
        contador = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        contador += 1 * Time.deltaTime;
        if(contador>=7)
        {
            contador = 0;
            SpawnEnemy();
                
        }
    }

    private void SpawnEnemy()
    {
        semilla = Environment.TickCount;
        Vector3 posicion = new Vector3(transform.position.x + new System.Random(semilla).Next(-radio, radio), 0, transform.position.z + new System.Random(semilla).Next(-radio, radio));
        Debug.Log(posicion);
        Instantiate(enemy, posicion, transform.rotation);
    }
}
