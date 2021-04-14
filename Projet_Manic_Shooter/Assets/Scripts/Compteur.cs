using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compteur : MonoBehaviour
/********************************************************\
 * BUT      : Stocker les compteurs non liés au joueur.
 * ENTREE   : Les ennemis quand ils apparaissent ou disparaissent.
 * SORTIE   : L'état actuel des ennemis en jeu.
\********************************************************/
{
    public List<Transform> l_Ennemis;
    public int n_TailleListe = 0;

    // Start is called before the first frame update
    void Start()
    //BUT : Initialiser la liste.
    {
        l_Ennemis = new List<Transform>();
        n_TailleListe = 0;
    }

    public void AjouteEnnemi(GameObject Ennemi)
    //BUT : Ajouter un ennemi à la liste.
    {
        if (!l_Ennemis.Contains(Ennemi.transform))
        {
            l_Ennemis.Add(Ennemi.transform);
            n_TailleListe++;
        }
    }

    public void MortEnnemi(GameObject Ennemi)
    //BUT : Retirer un ennemi de la liste.
    {
        if (l_Ennemis.Contains(Ennemi.transform))
        {
            l_Ennemis.Remove(Ennemi.transform);
            n_TailleListe--;
        }
    }

    public void VideListe()
    //BUT : Vider la liste.
    {
        l_Ennemis.Clear();
        n_TailleListe = 0;
    }

    #region code pour le singleton
    //Groupe rendant le script unique.
    private static Compteur instance = null;

    // Game Instance Singleton
    public static Compteur Instance
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
