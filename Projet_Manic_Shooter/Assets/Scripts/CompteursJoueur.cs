using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteursJoueur : MonoBehaviour
/********************************************************\
 * BUT      : Stocker les compteurs lli�s aux joueurs.
 * ENTREE   : La vie, le score et les bombes du joueur.
 * SORTIE   : L'�tat des variables du joueur mises � jour.
\********************************************************/
{
    public int n_Bombe;
    public int n_Vie;
    public long ln_Score;

    public void ChangeScore(int n_Score)
    {
        ln_Score += n_Score;
    }

    public void ChangeVie(int Vie)
    {
        n_Vie+=Vie;
    }

    public void ChangeBombe(int Bombe)
    {
        n_Bombe+=Bombe;
    }

    public void SetScore(long Score)
    {
        ln_Score = Score;
    }

    public void SetVie(int Vie)
    {
        n_Vie = Vie;
    }

    public void SetBombe(int Bombe)
    {
        n_Bombe = Bombe;
    }
}
