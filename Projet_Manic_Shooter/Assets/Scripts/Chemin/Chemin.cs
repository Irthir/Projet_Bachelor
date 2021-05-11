using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemin : MonoBehaviour
/********************************************************\
 * BUT      : R�aliser un gestionnaire de trajectoires et de cibles.
 * ENTREE   : Le temps, les cibles et l'�tat du chemin.
 * SORTIE   : La mise � jour du chemin � suivre et la sauvegarde pour le retour dans le temps.
\********************************************************/
{
    [SerializeField]
    public GameObject[] o_PointDePassage;
    [SerializeField]
    public float[] f_Delai;
    [SerializeField]
    TrajectoireChercheCible c_TrajectoireChercheCible = null;
    int n_Etat = 0;
    Minuteur c_Minuteur=null;

    double d_Actuel=0.0f;
    Vector3[] v_PositionPrecedentes = new Vector3[5];
    Vector3[] v_CiblesPrecedentes = new Vector3[5];
    int[] n_EtatsPrecedents = new int[5];

    void Start()
    //BUT : Initialiser le chemin et les tableaux de sauvegarde.
    {
        StartCoroutine(ChangeCible(f_Delai[n_Etat]));
  
        if (c_Minuteur==null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

        for (int i = 0; i < v_PositionPrecedentes.Length; i++)
        {
            v_PositionPrecedentes[i] = transform.position;
        }

        for (int i = 0; i < v_CiblesPrecedentes.Length; i++)
        {
            v_CiblesPrecedentes[i] = o_PointDePassage[n_Etat].transform.position;
        }

        for (int i = 0; i < n_EtatsPrecedents.Length; i++)
        {
            n_EtatsPrecedents[i] = n_Etat;
        }
        
        if (c_TrajectoireChercheCible == null)
        {
            c_TrajectoireChercheCible = gameObject.GetComponent<TrajectoireChercheCible>();
        }
    }

    // Update is called once per frame
    void Update()
    //BUT : Mettre � jour les sauvegardes, la cible, et utiliser les sauvegardes en cas de retour dans le temps.
    {
        if (d_Actuel>c_Minuteur.GetTemps()) // S'active en cas de retour dans le temps.
        {
            transform.position = v_PositionPrecedentes[v_PositionPrecedentes.Length - 1];
            n_Etat = n_EtatsPrecedents[n_EtatsPrecedents.Length - 1];
            if (o_PointDePassage.Length > n_Etat && o_PointDePassage[n_Etat] != null)
            {
                StopCoroutine("ChangeCible");
                StartCoroutine(ChangeCible(0.0f));
            }
        }

        if ((int)d_Actuel<(int)c_Minuteur.GetTemps()) //S'active � chaque seconde.
        {
            //Debug.Log("Sauvegarde");
            AjoutPosition(transform.position);
            if (o_PointDePassage.Length>n_Etat)
                AjoutCible(o_PointDePassage[n_Etat].transform.position);
            AjoutEtat(n_Etat);
        }

        d_Actuel = c_Minuteur.GetTemps(); //Mise � jour du temps actuel.
        
        if (o_PointDePassage.Length > n_Etat && transform.position == o_PointDePassage[n_Etat].transform.position) //S'active quand le point de passage est atteint.
        {
            n_Etat++;
            if (o_PointDePassage.Length > n_Etat && o_PointDePassage[n_Etat] != null)
            {
                StartCoroutine(ChangeCible(f_Delai[n_Etat]));
            }
        }
    }

    IEnumerator ChangeCible(float f_Delai)
    //BUT : Changer la cible apr�s l'attente pr�vue.
    {
        yield return new WaitForSeconds(f_Delai);

        Debug.Log(o_PointDePassage[n_Etat]);

        c_TrajectoireChercheCible.v_Cible = o_PointDePassage[n_Etat].transform.position;
    }

    void AjoutPosition(Vector3 v_Position)
    //BUT : Mettre � jour le tableau de sauvegarde des positions.
    {
        for (int i = v_PositionPrecedentes.Length - 1; i > 0 ; i--)
        {
            v_PositionPrecedentes[i] = v_PositionPrecedentes[i - 1];
        }
        v_PositionPrecedentes[0] = v_Position;
    }

    void AjoutCible(Vector3 v_Cible)
    //BUT : Mettre � jour le tableau de sauvegarde des cibles.
    {
        for (int i = v_CiblesPrecedentes.Length - 1; i > 0; i--)
        {
            v_CiblesPrecedentes[i] = v_CiblesPrecedentes[i - 1];
        }
        v_CiblesPrecedentes[0] = v_Cible;
    }

    void AjoutEtat(int n_Etat)
    //BUT : Mettre � jour le tableau de sauvegarde des �tats.
    {
        for (int i = n_EtatsPrecedents.Length - 1; i > 0; i--)
        {
            n_EtatsPrecedents[i] = n_EtatsPrecedents[i - 1];
        }
        n_EtatsPrecedents[0] = n_Etat;
    }
}
