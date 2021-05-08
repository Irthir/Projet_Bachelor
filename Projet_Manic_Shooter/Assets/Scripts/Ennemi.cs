using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
/********************************************************\
 * BUT      : G�rer la vie d'un ennemi tant qu'il existe.
 * ENTREE   : Les variables de l'ennemi et ses collisions ainsi que sa prise de d�g�ts.
 * SORTIE   : La vie de l'ennemi et sa mort le cas �ch�ant.
\********************************************************/
{
    public Minuteur c_Minuteur = null;
    public Compteur c_Compteur = null;
    public CompteursJoueur c_CompteursJoueur = null;

    public GameObject Apparence=null;
    public GameObject Motif=null;
    public CapsuleCollider Collider;
    public int n_Vie = 10;
    public int n_ValeurEnnemi = 10;

    private bool b_Vaincu = false;
    private double d_defaite;
    private double d_naissance;

    // Start is called before the first frame update
    void Start()
    /********************************************************\
     * BUT      : Initialiser les r�f�rences de la classe.
     * ENTREE   : Les r�f�rences de la classe � initialiser.
     * SORTIE   : La cr�ation termin�e de l'ennemi et l'ajout de l'ennemi � la liste des ennemis.
    \********************************************************/
    {
        if (c_Minuteur == null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

        if (c_Compteur == null)
        {
            c_Compteur = GameObject.Find("GameManager").GetComponent<Compteur>();
        }

        if (c_CompteursJoueur == null)
        {
            c_CompteursJoueur = GameObject.Find("GameManager").GetComponent<CompteursJoueur>();
        }

        if (Motif==null)
        {
            Motif = gameObject.transform.GetChild(1).gameObject;
        }

        c_Compteur.AjouteEnnemi(gameObject);
        d_naissance = c_Minuteur.GetTemps();
    }

    // Update is called once per frame
    void Update()
    /********************************************************\
     * BUT      : Si l'ennemi est mort mais que le temps a �t� remont�, il doit revenir � la vie.
                  Si l'ennemi est mort depuis trop longtemps, il doit �tre d�truit.
     * ENTREE   : La mort potentielle de l'ennemi, le temps actuel et la date de mort de l'ennemi.
     * SORTIE   : La destruction ou la r�surrection de l'ennemi.
    \********************************************************/
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
    /********************************************************\
     * BUT      : G�rer les d�g�ts pris par l'ennemi lors de la collision avec un tir du joueur.
     * ENTREE   : Le tir du joueur et les �l�ments du tir et de l'ennemi.
     * SORTIE   : La gestion des d�g�ts et de la collision de l'ennemi.
    \********************************************************/
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
    /********************************************************\
     * BUT      : Infliger des d�g�ts � l'ennemi.
     * ENTREE   : Les d�g�ts � infliger.
     * SORTIE   : La perte de vie et la d�faite dans le cas �ch�ant.
    \********************************************************/
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


    public void Vaincu(bool b_MortParEnvironnement=false)
    /********************************************************\
     * BUT      : G�rer la d�faite de l'ennemi.
     * ENTREE   : La d�faite de l'ennemi.
     * SORTIE   : La d�sactivation de l'ennemi en attendant la mort ou la r�surrection de l'ennemi.
    \********************************************************/
    {
        if (b_MortParEnvironnement)
        {
            //Pas de score
        }
        else
        {
            //Du score !
            c_CompteursJoueur.ChangeScore(n_ValeurEnnemi);
        }

        b_Vaincu = true;

        d_defaite = c_Minuteur.GetTemps();

        //c_Trajectoire.enabled = false;
        Collider.enabled = false;
        Apparence.SetActive(false);
        Motif.SetActive(false);

        n_Vie = 0;

        c_Compteur.MortEnnemi(gameObject);
    }

    void Resurrection()
    /********************************************************\
     * BUT      : Ressusciter l'ennemi quand le temps a �t� remont� � un moment o� il �tait vivant et d�j� n�.
     * ENTREE   : La r�surrection de l'ennemi.
     * SORTIE   : La remise en place de l'ennemi et de ses motifs.
    \********************************************************/
    {
        b_Vaincu = false;
 
        //c_Trajectoire.enabled = true;
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
