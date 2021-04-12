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
    {
        l_Ennemis = new List<Transform>();
        n_TailleListe = 0;
    }

    public void AjouteEnnemi(GameObject Ennemi)
    {
        if (!l_Ennemis.Contains(Ennemi.transform))
        {
            l_Ennemis.Add(Ennemi.transform);
            n_TailleListe++;
        }
    }

    public void MortEnnemi(GameObject Ennemi)
    {
        if (l_Ennemis.Contains(Ennemi.transform))
        {
            l_Ennemis.Remove(Ennemi.transform);
            n_TailleListe--;
        }
    }

    public void VideListe()
    {
        l_Ennemis.Clear();
        n_TailleListe = 0;
    }
}
