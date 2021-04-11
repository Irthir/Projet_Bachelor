using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    public Minuteur c_Minuteur = null;
    public Compteur c_Compteur = null;

    public Trajectoire c_Trajectoire;
    public GameObject Apparence;
    public GameObject Motif;
    public CapsuleCollider Collider;
    public int n_Vie = 10;

    private bool b_Vaincu = false;
    private double d_defaite;
    private double d_naissance;

    // Start is called before the first frame update
    void Start()
    {
        if (c_Minuteur == null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

        if (c_Compteur == null)
        {
            c_Compteur = GameObject.Find("GameManager").GetComponent<Compteur>();
        }

        c_Compteur.AjouteEnnemi(gameObject);
        d_naissance = c_Minuteur.GetTemps();
    }

    // Update is called once per frame
    void Update()
    {
        if (b_Vaincu)
        {
            double d_Temps = c_Minuteur.GetTemps();
           
            if (d_Temps < d_naissance)
            {
                Destroy(gameObject);
            }
            else if (d_Temps < d_defaite)
            {
                Resurrection();
            }
            else if(d_Temps > (6+d_defaite))
            {
                Destroy(gameObject);
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
                    InfligeDegats(2);
                }
                else
                {
                    InfligeDegats(1);
                }
            }

            Destroy(TirJoueur.gameObject);
        }
    }

    public void InfligeDegats(int n_degats)
    {
        if (n_Vie>0)
        {
            n_Vie -= n_degats;

            if (n_Vie <= 0)
            {
                Vaincu();
            }
        }
    }


    public void Vaincu()
    {

        b_Vaincu = true;

        d_defaite = c_Minuteur.GetTemps();

        c_Trajectoire.enabled = false;
        Collider.enabled = false;
        Apparence.SetActive(false);
        Motif.SetActive(false);

        n_Vie = 0;

        c_Compteur.MortEnnemi(gameObject);
    }

    void Resurrection()
    {
        b_Vaincu = false;
 
        c_Trajectoire.enabled = true;
        Collider.enabled = true;
        Apparence.SetActive(true);
        Motif.SetActive(true);

        n_Vie = 1;

        c_Compteur.AjouteEnnemi(gameObject);

        Motif[] motifs = GetComponentsInChildren<Motif>();
        if(motifs.Length>0)
        {
            for (int i = 0; i < motifs.Length; i++)
            {
                motifs[i].RetourTemps();
            }
        }
    }
}
