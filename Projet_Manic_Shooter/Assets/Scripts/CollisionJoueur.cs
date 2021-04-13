using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionJoueur : MonoBehaviour
/********************************************************\
 * BUT      : Gérer les collisions du joueur avec les ennemis et les projectiles ennemis.
 * ENTREE   : L'objet entrant en collision avec le joueur.
 * SORTIE   : L'absorption des projectile de même élément ou la perte de vie dans les autres cas.
\********************************************************/
{
    public GameObject o_Joueur = null;
    public CompteursJoueur c_Compteur = null;
    public PlayerController c_Controller = null;
    public bool b_BombeTerre = false;

    // Start is called before the first frame update
    void Start()
    //BUT : Initialiser les références.
    {
        if (o_Joueur == null)
        {
            o_Joueur = GameObject.Find("Joueur");
        }

        if (c_Controller==null)
        {
            c_Controller = o_Joueur.GetComponent<PlayerController>();
        }

        if (c_Compteur==null)
        {
            c_Compteur = GameObject.Find("GameManager").GetComponent<CompteursJoueur>();
        }
    }

    void OnTriggerEnter(Collider other)
    //BUT : Gérer la collision du joueur.
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Danmaku"))
        {
            if (other.tag == o_Joueur.tag)
            {
                //Debug.Log("Absorption");
                c_Compteur.ChangeScore(2);
            }
            else if(!c_Controller.b_Invincible)
            {
                if (b_BombeTerre)
                {
                    Debug.Log("BombeTerre");
                    b_BombeTerre = false;
                    c_Controller.NettoieEcran();
                }
                else
                {
                    c_Compteur.ChangeVie(-1.0f);
                }
            }
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Ennemi"))
        {
            if (!c_Controller.b_Invincible)
            {
                Debug.Log("Vie Perdue");
            }
        }
    }
}
