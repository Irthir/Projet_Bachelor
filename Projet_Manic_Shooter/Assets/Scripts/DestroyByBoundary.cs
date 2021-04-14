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

    #region code pour le singleton
    //Groupe rendant le script unique.
    private static DestroyByBoundary instance = null;

    // Game Instance Singleton
    public static DestroyByBoundary Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
}
