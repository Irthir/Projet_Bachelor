using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    public Minuteur c_Minuteur = null;
    public Trajectoire c_Trajectoire;
    public GameObject Apparence;
    public CapsuleCollider Collider;
    public int n_Vie = 10;

    private bool b_Vaincu = false;
    private double d_defaite;

    // Start is called before the first frame update
    void Start()
    {
        if (c_Minuteur == null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (b_Vaincu)
        {
            if (c_Minuteur.GetTemps()>(5+d_defaite))
            {
                //Destroy(gameObject);
                Resurrection();
            }
        }
    }

    void OnTriggerEnter(Collider TirJoueur)
    {
        if (TirJoueur.gameObject.layer == LayerMask.NameToLayer("Magie"))
        {
            if (TirJoueur.tag != gameObject.tag)
            {
                string Element = gameObject.tag;
                string Attaque = gameObject.tag;
                if ((Element == "Bois" && Attaque == "Feu") || (Element == "Terre" && Attaque == "Bois") || (Element == "Feu" && Attaque == "Terre"))
                {
                    n_Vie -= 2;
                }
                else
                {
                    n_Vie--;
                }

                if (n_Vie <= 0)
                {
                    Vaincu();
                }
            }

            Destroy(TirJoueur.gameObject);
        }
    }


    void Vaincu()
    {
        b_Vaincu = true;

        d_defaite = c_Minuteur.GetTemps();

        c_Trajectoire.enabled = false;
        Collider.enabled = false;
        Apparence.SetActive(false);
    }

    void Resurrection()
    {
        b_Vaincu = false;
 
        c_Trajectoire.enabled = true;
        Collider.enabled = true;
        Apparence.SetActive(true);
    }
}
