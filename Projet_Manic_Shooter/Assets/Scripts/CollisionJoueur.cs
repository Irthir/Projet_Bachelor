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
    public PlayerCharacter c_Character = null;
    public GameManager c_Manager = null;
    public bool b_BombeTerre = false;

    // Start is called before the first frame update
    void Start()
    //BUT : Initialiser les références.
    {
        if (o_Joueur == null)
        {
            o_Joueur = GameObject.Find("Joueur");
        }

        if (c_Character==null)
        {
            c_Character = o_Joueur.GetComponent<PlayerCharacter>();
        }

        if (c_Compteur==null)
        {
            c_Compteur = GameObject.Find("GameManager").GetComponent<CompteursJoueur>();
        }

        if (c_Manager==null)
        {
            c_Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            else if(!c_Character.b_Invincible)
            {
                if (b_BombeTerre)
                {
                    //Debug.Log("BombeTerre");
                    b_BombeTerre = false;
                    c_Character.NettoieEcran();
                }
                else
                {
                    c_Manager.MortJoueur();
                }
            }
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Ennemi"))
        {
            if (!c_Character.b_Invincible)
            {
                c_Manager.MortJoueur();
            }
        }
    }
}
