using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public GameObject Mike;
    private int daño;
    public int contador;
    public int multiplicador;
    private void Start()
    {
        contador = 0;
        multiplicador = 1;
    }

    private void Update()
    {
        if (contador >= 5) //Cuando el contador de golpes es mayor que 5 se reinicia el contador y el multiplicador (Definitiva de la espada)
        {
            multiplicador = 1;
            contador = 0;
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" && Mike.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Atacando")) //Se comprueba que se ha impactado a un enemigo mientras se atacaba
        {
            contador++; //Se aumenta el contador de golpes el cual utiliza la definitiva de la espada
            switch (Mike.GetComponent<MikeController>().ataqueEnCurso) //Se comprueba qué clase de ataque era
            {
                case 1: //Definitiva
                    if (PlayerPrefs.GetInt("Inventario") == 2)//Como las definitivas son diferentes se hace la distinción.
                    {
                        collision.gameObject.GetComponent<EnemyController>().vida -= 7 * multiplicador;
                    }
                    //Como la definitiva de la espada es un power up entonces se edita desde el Mike controller
                    break;
                case 2: //Ataque débil
                    collision.gameObject.GetComponent<EnemyController>().vida -= 1*multiplicador;
                    break;
                case 3: //Ataque fuerte
                    collision.gameObject.GetComponent<EnemyController>().vida -= 3*multiplicador;
                    break;

            }
        }
    }
}
