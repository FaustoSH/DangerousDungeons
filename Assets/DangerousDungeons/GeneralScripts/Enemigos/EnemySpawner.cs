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
    private Transform[] spawnersPositions;
    private int segundosRonda;
    private int anteriorZombiesMuertos;
    private int contadorSiguienteFase;
    // Start is called before the first frame update
    void Start()
    {
        contador = 0;
        segundosRonda = 9;
        anteriorZombiesMuertos = 0;
        contadorSiguienteFase = 3;
        spawnersPositions = gameObject.GetComponentsInChildren<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        int zombiesMatados = GameObject.Find("Mike").GetComponent<MikeController>().zombiesMuertos;
        contador += 1 * Time.deltaTime;
        if (contador >= segundosRonda)
        {
            contador = 0;
            SpawnEnemy();
                
        }
        if(segundosRonda>1&&zombiesMatados>anteriorZombiesMuertos&&zombiesMatados%contadorSiguienteFase==0)
        {
            anteriorZombiesMuertos = zombiesMatados;
            segundosRonda--;
            contadorSiguienteFase =(int)(contadorSiguienteFase* 1.5f);
            Debug.Log("SegundosRonda-->" + segundosRonda);
        }
    }

    private void SpawnEnemy()
    {
        semilla = Environment.TickCount;
        Vector3 posicion = spawnersPositions[new System.Random(semilla).Next(1, spawnersPositions.Length)].position; //Va desde el uno para evitar coger la posición del padre
        Instantiate(enemy, posicion, transform.rotation);
    }
}
