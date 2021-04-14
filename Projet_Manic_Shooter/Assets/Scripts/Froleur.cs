using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Froleur : MonoBehaviour
/********************************************************\
 * BUT      : Gérer les gains du score du joueur quand il frôle une attaque ennemie.
 * ENTREE   : La collision avec la sphère de frôlement.
 * SORTIE   : Le gain du score et l'absorption le cas échéant.
\********************************************************/
{
    public GameObject o_Joueur = null;
    public CompteursJoueur c_CompteurJoueur = null;

    // Start is called before the first frame update
    void Start()
    //BUT : Initialiser les références.
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
    //BUT : Gérer la collision du frôleur avec les attaques ennemies.
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
                //Debug.Log("Frôlement");
                c_CompteurJoueur.ChangeScore(1);
            }
        }
    }
}
