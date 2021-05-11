using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject o_Joueur=null;
    public Transform t_Apparition = null;
    public PlayerCharacter c_Character = null;
    public CompteursJoueur c_CompteurJoueur = null;
    public Minuteur c_Minuteur = null;

    // Start is called before the first frame update
    void Start()
    //BUT : Initialiser les références.
    {
        if (o_Joueur==null)
        {
            o_Joueur = GameObject.Find("Joueur");
        }

        if (t_Apparition==null)
        {
            t_Apparition = GameObject.Find("Apparition").GetComponent<Transform>();
        }

        if (c_Character==null)
        {
            c_Character = GameObject.Find("Joueur").GetComponent<PlayerCharacter>();
        }

        if (c_CompteurJoueur == null)
        {
            c_CompteurJoueur = gameObject.GetComponent<CompteursJoueur>();
        }

        if (c_Minuteur == null)
        {
            c_Minuteur = gameObject.GetComponent<Minuteur>();
        }
    }

    public void MortJoueur()
    //BUT : Gérer la mort du joueur.
    {
        c_CompteurJoueur.ChangeVie(-1.0f);
        o_Joueur.SetActive(false);
        if (c_CompteurJoueur.f_Vie>0.0f)
        {
            float f_Temps = 1.0f;
            StartCoroutine("ReapparitionJoueur", f_Temps);
        }
        else
        {
            c_Minuteur.Pause();
        }
        
    }

    IEnumerator ReapparitionJoueur(float f_Attente=1.0f)
    //BUT : Faire réapparaitre le joueur.
    {
        yield return new WaitForSeconds(f_Attente);
        o_Joueur.SetActive(true);
        o_Joueur.transform.position = t_Apparition.position;
        c_Character.Explosion();
        c_Character.NettoieEcran();
        c_CompteurJoueur.SetBombe(4.0f);
        c_Character.b_Invincible = true;
        c_Character.d_MomentInvincible = c_Minuteur.GetTemps();
    }

    #region code pour le singleton
    //Groupe rendant le script unique.
    private static GameManager instance = null;

    // Game Instance Singleton
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
}
