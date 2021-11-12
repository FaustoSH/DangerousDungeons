using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockablesController : MonoBehaviour
{
    public GameObject arma;
    public Image inventario;
    public Sprite[] spriteInventario;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag=="Player")
        {
            if(arma.name=="ConjuntoDeArmas")
            {
                PlayerPrefs.SetInt("Espada", 1);
                if(PlayerPrefs.GetInt("Hacha")==0)
                {
                    inventario.sprite = spriteInventario[1];
                }else
                {
                    inventario.sprite = spriteInventario[3];
                }
            }else if(arma.name=="BigAxe")
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
            Destroy(arma.transform.parent.gameObject);
        }
    }
}
