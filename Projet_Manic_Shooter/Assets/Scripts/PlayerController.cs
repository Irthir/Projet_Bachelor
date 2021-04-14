using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
/********************************************************\
 * BUT      : Gérer les contrôles et  actions du joueur.
 * ENTREE   : Les inputs du joueur.
 * SORTIE   : Les mouvements et actions du personnage joueur.
\********************************************************/
{
    //Header
    public PlayerCharacter c_Character = null;
    public Minuteur c_Minuteur = null;


    public float f_Vitesse = 10.0f;
    public float f_Ralentissement = 7.0f;

    public float f_TauxTir = 0.15f;
    public float f_AugmentationTauxTir;
    private float nextFire = 0.0f;

    public float f_AugmentationVitesseTir = 5.0f;
    public float f_VitesseTir = 20.0f;

    //Functions

    // Start is called before the first frame update
    void Start()
    /********************************************************\
     * BUT      : Mettre en place les variables dont le PlayerController a besoin au lancement
     * ENTREE   : Les références de scripts et variables de GameDesign
     * SORTIE   : Les variables qui seront utilisées, initialisées.
    \********************************************************/
    {
        if (c_Character == null)
        {
            c_Character = gameObject.GetComponent<PlayerCharacter>();
        }

        if (c_Minuteur == null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

        c_Character.f_Vitesse = f_Vitesse;
        c_Character.f_VitesseTir = f_VitesseTir;
        c_Character.f_TauxTir = f_TauxTir;
    }

    // Update is called once per frame
    void Update()
    /********************************************************\
     * BUT      : Récupérer les inputs du joueur et les appliquer.
     * ENTREE   : Les inputs du joueur.
     * SORTIE   : L'appelle des fonctions relatives à ces inputs.
    \********************************************************/
    {
        if (c_Minuteur.b_Actif)
        {
            //Réception des inputs.
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + c_Character.f_TauxTir;
                c_Character.Tir();
            }

            if (Input.GetButtonDown("Suivant"))
            {
                switch (gameObject.tag)
                {
                    case "Feu":
                        gameObject.tag = "Bois";
                        break;
                    case "Bois":
                        gameObject.tag = "Terre";
                        break;
                    default:
                        gameObject.tag = "Feu";
                        break;
                }
            }

            if (Input.GetButtonDown("Precedent"))
            {
                switch (gameObject.tag)
                {
                    case "Bois":
                        gameObject.tag = "Feu";
                        break;
                    case "Terre":
                        gameObject.tag = "Bois";
                        break;
                    default:
                        gameObject.tag = "Terre";
                        break;
                }
            }

            if (Input.GetButtonDown("Arcane"))
            {
                gameObject.tag = "Arcane";
            }

            if (Input.GetButtonDown("Concentration"))
            {
                c_Character.f_TauxTir = f_TauxTir - f_AugmentationTauxTir;
                c_Character.f_VitesseTir = f_VitesseTir + f_AugmentationVitesseTir;
                c_Character.f_Vitesse = f_Vitesse - f_Ralentissement;
            }

            if (Input.GetButtonUp("Concentration"))
            {
                c_Character.f_TauxTir = f_TauxTir;
                c_Character.f_VitesseTir = f_VitesseTir;
                c_Character.f_Vitesse = f_Vitesse;
            }

            if (Input.GetButtonDown("Bombe"))
            {
                c_Character.Bombe();
            }

            if (Input.GetButtonDown("Pause"))
            {
                c_Minuteur.Pause();
            }
        }
        else if (Input.GetButtonDown("Pause"))
        {
            c_Minuteur.Play();
        }
    }

    //Fixed Update is called before each fixed physic step
    void FixedUpdate()
    /********************************************************\
     * BUT      : Déplacer le joueur selon des mouvements physiques.
     * ENTREE   : Les inputs axiaux du joueur.
     * SORTIE   : Les déplacements du joueur en de l'axe, la vitesse et le temps.
    \********************************************************/
    {
        //Associations des inputs pour le déplacement
        float f_moveHorizontal = Input.GetAxis("Horizontal");
        float f_moveVertical = Input.GetAxis("Vertical");
        float f_Delta = Time.fixedDeltaTime;
        c_Character.Deplacement(f_moveHorizontal, f_moveVertical, f_Delta);
    }

    #region code pour le singleton
    //Groupe rendant le script unique.
    private static PlayerController instance = null;

    // Game Instance Singleton
    public static PlayerController Instance
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
