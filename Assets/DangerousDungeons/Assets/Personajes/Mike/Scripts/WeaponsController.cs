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
        if (contador >= 5)
        {
            multiplicador = 1;
            contador = 0;
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" && Mike.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Atacando")) //Se comprueba que se ha impactado a un enemigo mientras se atacaba
        {
            contador++;
            Debug.Log("Contador-->" + contador + "  Multiplicador-->" + multiplicador + " Inventario-->"+ PlayerPrefs.GetInt("Inventario"));
            switch (Mike.GetComponent<MikeController>().ataqueEnCurso) //Se comprueba qué clase de ataque era
            {
                case 1: //Definitiva
                    if (PlayerPrefs.GetInt("Inventario") == 2)//Como las definitivas son diferentes se hace la distinción.
                    {
                        collision.gameObject.GetComponent<ZombieController>().vida -= 7 * multiplicador;
                    }
                    break;
                case 2: //Ataque débil
                    collision.gameObject.GetComponent<ZombieController>().vida -= 1*multiplicador;
                    break;
                case 3: //Ataque fuerte
                    collision.gameObject.GetComponent<ZombieController>().vida -= 3*multiplicador;
                    break;

            }
            Debug.Log(collision.gameObject.GetComponent<ZombieController>().vida);
        }
    }
}
