/*
    Autor: Fausto Sánchez Hoya
    Descripción: este código se encarga de las acciones principales y más generales que debe realizar un enemigo 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{    
    //Animator
    private Animator animator;

    //Contadores
    private float tiempoMuerto;
    private float enfriamiento;

    //Variable para el control de la vida
    public int vida;
    private int vidaAnterior;

    private GameObject Mike;
    private NavMeshAgent navMeshAgent;

    //Variable para la contabilización de las muertes.
    private bool muerteNotificada;

    void Start()
    {
        //La inicialización de la vida se hace mediante la interfaz ya que en un primer momento se pretendía desarrollar varios tipos de enemigos.
        animator = this.gameObject.GetComponent<Animator>();
        tiempoMuerto = 0;
        vidaAnterior = vida;
        navMeshAgent = GetComponent<NavMeshAgent>();
        Mike = GameObject.Find("Mike");
        enfriamiento = 1.0f; //La implementación del enfriamiento está por el mismo motivo que en el Mike controller
    }

    private void Update()
    {
        animator.SetInteger("Vida", vida); //La vida del animator se está actualizando constantemente para efectuar la animación de meurte lo antes posible
        if (vida <= 0) //Comprueba que se haya muerto
        {
            //Comienza un temporizador de 7 segundos que tras ese tiempo hace desaparecer el gameObject
            tiempoMuerto += 1.0f * Time.deltaTime;
            if (!muerteNotificada)
            {
                Mike.GetComponent<MikeController>().zombiesMuertos++;
                Debug.Log("ZombiesMuertos-->" + Mike.GetComponent<MikeController>().zombiesMuertos);
                muerteNotificada = true;
            }

            if (tiempoMuerto>=7)
            {
                
                //Después de 7 segundos muerto se elimina al enemigo para que no gaste memoria
                Destroy(this.gameObject);
            }
        }else if(vida<vidaAnterior) //Si se detecta una bajada en la vida significa que ha recibido un golpe por lo que debe realizar la animación
        {
            animator.SetTrigger("Golpe");
            vidaAnterior = vida;
        }else //En el caso de que no esté siendo atacado fija su posición de destino como la de Mike
        {
            navMeshAgent.destination = Mike.transform.position;
            if(navMeshAgent.remainingDistance<=1.1 && navMeshAgent.pathStatus==NavMeshPathStatus.PathComplete && navMeshAgent.remainingDistance!=Mathf.Infinity)
            {
                //Tras llegar a la posición se espera 1 segundo y se comienza a atacar.
                transform.LookAt(Mike.transform.position);
                animator.SetBool("Andar", false);
                enfriamiento -= 1.0f * Time.deltaTime;
                if (enfriamiento <= 0)
                {
                    gameObject.GetComponent<AudioSource>().Pause();
                    animator.SetBool("Ataque", true);
                    enfriamiento = 1.0f;
                }
                

            }
            else
            {
                //Si no está al lado de Mike se activa la animación y los sonidos pertinentes
                if(!gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    gameObject.GetComponent<AudioSource>().Play();
                }
                animator.SetBool("Andar", true);
                animator.SetBool("Ataque", false);
            }
               
        }
    }
}
