using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Froleur : MonoBehaviour
/********************************************************\
 * BUT      : G�rer les gains du score du joueur quand il fr�le une attaque ennemie.
 * ENTREE   : La collision avec la sph�re de fr�lement.
 * SORTIE   : Le gain du score et l'absorption le cas �ch�ant.
\********************************************************/
{
    public GameObject o_Joueur = null;
    public CompteursJoueur c_CompteurJoueur = null;

    // Start is called before the first frame update
    void Start()
    //BUT : Initialiser les r�f�rences.
    {
        if (o_Joueur == null)
        {
            o_Joueur = GameObject.Find("Joueur");
        }

        if (c_CompteurJoueur == null)
        {
            c_CompteurJoueur = GameObject.Find("GameManager").GetComponent<CompteursJoueur>();
        }
    }

    void OnTriggerEnter(Collider other)
    //BUT : G�rer la collision du fr�leur avec les attaques ennemies.
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Danmaku"))
        {
            if (other.tag == o_Joueur.tag)
            {
                //Debug.Log("Absorption");
                Destroy(other.gameObject);
                c_CompteurJoueur.ChangeScore(2);
            }
            else
            {
                //Debug.Log("Fr�lement");
                c_CompteurJoueur.ChangeScore(1);
            }
        }
    }
}
