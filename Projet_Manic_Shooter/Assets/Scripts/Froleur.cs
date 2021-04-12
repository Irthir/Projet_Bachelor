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
    public GameObject o_Joueur;

    // Start is called before the first frame update
    void Start()
    {
        if (o_Joueur == null)
        {
            o_Joueur = GameObject.Find("Joueur");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Danmaku"))
        {
            if (other.tag == o_Joueur.tag)
            {
                Debug.Log("Absorption");
            }
            else
            {
                Debug.Log("Frôlement");
            }
        }
    }
}
