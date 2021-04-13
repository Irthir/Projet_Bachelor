using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CompteursJoueur : MonoBehaviour
/********************************************************\
 * BUT      : Stocker les compteurs lliés aux joueurs.
 * ENTREE   : La vie, le score et les bombes du joueur.
 * SORTIE   : L'état des variables du joueur mises à jour.
\********************************************************/
{
    public float f_Bombe;
    public float f_Vie;
    public long ln_Score;

    public GameObject o_Bombe = null;
    public GameObject o_Vie = null;
    public GameObject o_Score = null;

    void Start()
    //BUT : Initialiser les références et les valeurs de l'interface.
    {
        if (o_Bombe == null)
        {
            o_Bombe = GameObject.Find("Bombes");
        }
        if (o_Vie == null)
        {
            o_Vie = GameObject.Find("Vies");
        }
        if (o_Score == null)
        {
            o_Score = GameObject.Find("Score");
        }

        UpdateScore();
        UpdateBombe();
        UpdateVie();
    }

    public void ChangeScore(int n_Score)
    //BUT : Ajouter ou enlever du score.
    {
        ln_Score += n_Score;
        UpdateScore();
    }

    public void ChangeVie(float Vie)
    //BUT : Ajouter ou enlever de la vie.
    {
        f_Vie += Vie;
        UpdateVie();
    }

    public void ChangeBombe(float Bombe)
    //BUT : Ajouter ou enlever des bombes.
    {
        f_Bombe +=Bombe;
        UpdateBombe();
    }

    public void SetScore(long Score)
    //BUT : Mettre à jour le score.
    {
        ln_Score = Score;
        UpdateScore();
    }

    public void SetVie(float Vie)
    //BUT : Mettre à jour la vie.
    {
        f_Vie = Vie;
        UpdateVie();
    }

    public void SetBombe(float Bombe)
    //BUT : Mettre à jour les bombes.
    {
        f_Bombe = Bombe;
        UpdateBombe();
    }

    private void UpdateScore()
    //BUT : Mettre à jour l'affichage du score.
    {
        o_Score.GetComponent<Text>().text = "Score : " + (ln_Score * 10).ToString("000 000 000");
    }

    private void UpdateVie()
    //BUT : Mettre à jour l'affichage de la vie.
    {
        o_Vie.GetComponent<Text>().text = "Vies : " + f_Vie.ToString("00");
    }

    private void UpdateBombe()
    //BUT : Mettre à jour l'affichage des bombes.
    {
        o_Bombe.GetComponent<Text>().text = "Bombes : " + f_Bombe.ToString("00");
    }
}
