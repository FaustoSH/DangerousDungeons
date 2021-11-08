using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockablesController : MonoBehaviour
{
    public GameObject arma;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag=="Player")
        {
            if(arma.name=="ConjuntoDeArmas")
            {
                PlayerPrefs.SetInt("Espada", 1);
            }else if(arma.name=="BigAxe")
            {
                PlayerPrefs.SetInt("Hacha", 1);
            }
            Destroy(arma.transform.parent.gameObject);
        }
    }
}
