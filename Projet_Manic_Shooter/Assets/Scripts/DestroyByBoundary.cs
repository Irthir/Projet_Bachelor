using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    /************************************************************\
     * BUT      : Détruire les objets qui quittent la zone de jeu.
     * ENTREE   : Les objets qui quittent la zone de jeu.
     * SORTIE   : La destruction de ces objets.
    \************************************************************/
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ennemi"))
        {
            other.GetComponent<Ennemi>().Vaincu(true);
        }
        else
        {
            Destroy(other.gameObject);
        }
        //Debug.Log("Destruction de " + other.name);
    }
}
