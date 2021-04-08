using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionJoueur : MonoBehaviour
{
    public GameObject o_Joueur = null;
    public CompteursJoueur c_Compteur = null;
    public PlayerController c_Controller = null;

    // Start is called before the first frame update
    void Start()
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
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Danmaku"))
        {
            if (other.tag == o_Joueur.tag)
            {
                Debug.Log("Absorption");
            }
            else
            {
                if (!c_Controller.b_Invincible)
                {
                    Debug.Log("Vie Perdue");
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
