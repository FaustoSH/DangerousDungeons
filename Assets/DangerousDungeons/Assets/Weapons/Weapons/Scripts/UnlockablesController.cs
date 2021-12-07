/*
    Autor: Fausto Sánchez Hoya
    Descripción: este código se encarga de la gestión de inventario cuando se desbloquea un nuevo objeto
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockablesController : MonoBehaviour
{
    public Image inventario;
    public Sprite[] spriteInventario;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag=="Player") //Cuando se detecta una colisión con el jugador
        {
            if(this.name=="ConjuntoDeArmas") //Se comprueba qué arma es con la que se ha colisionado
            {
                PlayerPrefs.SetInt("Espada", 1); //Se desbloquea el arma
                if(PlayerPrefs.GetInt("Hacha")==0) //Se elige el sprite del inventario 
                { 
                    inventario.sprite = spriteInventario[1];
                }else
                {
                    inventario.sprite = spriteInventario[3];
                }
            }else if(this.name=="BigAxe") //Idem para la otra arma
            {
                PlayerPrefs.SetInt("Hacha", 1);
                if (PlayerPrefs.GetInt("Espada") == 0)
                {
                    inventario.sprite = spriteInventario[2];
                }
                else
                {
                    inventario.sprite = spriteInventario[3];
                }
            }
            Destroy(this.transform.parent.gameObject);
        }
    }
}
