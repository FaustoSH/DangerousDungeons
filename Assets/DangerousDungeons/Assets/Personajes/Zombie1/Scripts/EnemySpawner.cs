/*
    Autor: Fausto Sánchez Hoya
    Descripción: este código se encarga de spawnear los enemigos por el mapa
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Enemigo a spawnerar
    public GameObject enemy;

    //Contador de tiempo de spawneo
    private float contador;

    //Variables para elegir el punto de aparición
    private int semilla;
    private Transform[] spawnersPositions;

    //Variables para el manejo de tiempos de ronda
    private int segundosRonda;
    private int anteriorZombiesMuertos;
    private int contadorSiguienteFase;
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
        //El contador siempre está corriendo y cuando llega a los segundos de la ronda spawnea un enemigo
        int zombiesMatados = GameObject.Find("Mike").GetComponent<MikeController>().zombiesMuertos;
        contador += 1 * Time.deltaTime;
        if (contador >= segundosRonda)
        {
            contador = 0;
            SpawnEnemy();
                
        }
        //Estos segundos van definidos por el número de zombies muertos
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
        Instantiate(enemy, posicion, transform.rotation); //Elige un punto de spawneo aleatorio y lo toma como posición.
    }
}
