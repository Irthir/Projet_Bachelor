using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    /************************************************************\
     * BUT      : Détruire les objets qui recontrent l'ennemi.
     * ENTREE   : Les objets qui rencontrent l'ennemi.
     * SORTIE   : La destruction de ces objets et de l'ennemi.
    \************************************************************/
    {
        if (other.tag == "Magie")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.tag == "Joueur")
        {
            Destroy(gameObject);
        }
    }
}
