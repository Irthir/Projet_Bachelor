using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public RectTransform rectTerrain=null;
    public float xMin, xMax, yMin, yMax;

    public void SetScreenBounds()
    /********************************************************\
     * BUT      : Mettre en place les limites de l'écran.
     * ENTREE   : Les limites de ce que voit la caméra.
     * SORTIE   : Les limites de l'écran stockées.
    \********************************************************/
    {
        if (rectTerrain == null)
        {
            rectTerrain = GameObject.Find("Terrain").GetComponent<RectTransform>();
        }
        Rect rect = rectTerrain.rect;

        /*Vector3 v_ScreenMaxBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        Vector3 v_ScreenMinBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - Screen.width, Screen.height - Screen.height,0));
        xMax = v_ScreenMaxBoundaries.x;
        xMin = v_ScreenMinBoundaries.x;
        yMax = v_ScreenMaxBoundaries.y;
        yMin = v_ScreenMinBoundaries.y;*/

        xMin = rect.xMin;
        xMax = rect.xMax;
        yMin = rect.yMin;
        yMax = rect.yMax;
    }
}

public class PlayerController : MonoBehaviour
/********************************************************\
 * BUT      : Gérer les contrôles et  actions du joueur.
 * ENTREE   : Les inputs du joueur.
 * SORTIE   : Les mouvements et actions du personnage joueur.
\********************************************************/
{
    //Header
    private float f_speed=10.0f;
    private float f_fireRate = 20.0f;
    private float f_fireSpeed = 0.15f;

    public float f_Vitesse = 10.0f;
    public float f_Ralentissement = 7.0f;
    public float f_tilt=3.0f;
    public Boundary boundary;
    public Rigidbody rb; //rb parce qu'il n'y a qu'un seul Rigidbody par objet et que le nom rigidbody est déjà pris dans la hiérarchie.

    public GameObject o_Feu;
    public GameObject o_Arcane;
    public GameObject o_Bois;
    public GameObject o_Terre;
    public float f_VitesseTir = 20.0f;
    public float f_AugmentationVitesseTir=5.0f;

    public Transform[] MagicSpawns;
    
    public float f_TauxTir = 0.15f;
    public float f_AugmentationTauxTir;
    private float nextFire = 0.0f;

    public bool b_Invincible = false;
    public float f_TempsInvincible = 2.0f;
    private double d_MomentInvincible = 0.0f;

    private bool b_BombePossible = true;
    private double d_TempsRetour = 0.0f;

    public CompteursJoueur c_CompteurJoueur = null;
    public Compteur c_Compteur = null;

    public Minuteur c_Minuteur = null;

    //Functions

    // Start is called before the first frame update
    void Start()
    /********************************************************\
     * BUT      : Mettre en place les variables dont le PlayerController a besoin au lancement
     * ENTREE   : Les références de scripts et variables de GameDesign
     * SORTIE   : Les variables qui seront utilisées, initialisées.
    \********************************************************/
    {
        boundary.SetScreenBounds();
        rb = GetComponent<Rigidbody>();

        if (c_CompteurJoueur == null)
        {
            c_CompteurJoueur = GameObject.Find("GameManager").GetComponent<CompteursJoueur>();
        }

        if (c_Minuteur == null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

        if (c_Compteur==null)
        {
            c_Compteur = GameObject.Find("GameManager").GetComponent<Compteur>();
        }

        f_speed = f_Vitesse;
        f_fireRate = f_TauxTir;
        f_fireSpeed = f_VitesseTir;
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
                nextFire = Time.time + f_fireRate;
                Tir();
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
                f_fireRate = f_TauxTir - f_AugmentationTauxTir;
                f_fireSpeed = f_VitesseTir + f_AugmentationVitesseTir;
                f_speed = f_Vitesse - f_Ralentissement;
            }

            if (Input.GetButtonUp("Concentration"))
            {
                f_fireRate = f_TauxTir;
                f_fireSpeed = f_VitesseTir;
                f_speed = f_Vitesse;
            }

            if (Input.GetButtonDown("Bombe"))
            {
                Bombe();
            }

            //Gestion des bombes et de l'invincibilité
            if (b_Invincible)
            {
                if (d_MomentInvincible + f_TempsInvincible <= c_Minuteur.GetTemps())
                {
                    b_Invincible = false;
                }
            }

            if (!b_BombePossible)
            {
                if (c_Minuteur.GetTemps() >= d_TempsRetour)
                {
                    b_BombePossible = true;
                    ArcaneExplosion();
                }
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
        if (c_Minuteur.b_Actif)
        {
            //Associations des inputs pour le déplacement
            float f_moveHorizontal = Input.GetAxis("Horizontal");
            float f_moveVertical = Input.GetAxis("Vertical");

            //Réalisation du déplacement
            Vector3 v_movement = new Vector3(f_moveHorizontal, f_moveVertical, 0.0f);
            rb.velocity = v_movement * f_speed * Time.fixedDeltaTime;

            rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
                0.0f
            );

            rb.rotation = Quaternion.Euler(0.0f, GetComponent<Rigidbody>().velocity.x * -f_tilt, 0.0f);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Tir()
    /********************************************************\
     * BUT      : Effectuer le tir correspondant à l'élément actuel du joueur.
     * ENTREE   : L'action du tir du joueur et le tag du joueur portant son élément.
     * SORTIE   : L'appel de la méthode correspondant au tir du dit élément.
    \********************************************************/
    {
        switch (gameObject.tag)
        {
            case "Terre":
                Terre();
                break;
            case "Bois":
                Bois();
                break;
            case "Feu":
                Feu();
                break;
            default:
                Arcane();
                break;
        }
    }

    void Arcane()
    /********************************************************\
     * BUT      : Réaliser le motif d'arcane du joueur.
     * ENTREE   : Les points d'apparition des attaques du joueur et le prefab du tir d'arcane.
     * SORTIE   : L'apparition des tirs d'arcanes en suivant leurs motifs.
    \********************************************************/
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject Arcane = Instantiate(o_Arcane, MagicSpawns[i].position, MagicSpawns[i].rotation);
            Arcane.AddComponent<TrajectoireDroite>();
            switch (i)
            {
                case 1:
                    Arcane.GetComponent<TrajectoireDroite>().f_Angle = 80.0f;
                    break;
                case 2:
                    Arcane.GetComponent<TrajectoireDroite>().f_Angle = 100.0f;
                    break;
                default:
                    Arcane.GetComponent<TrajectoireDroite>().f_Angle = 90.0f;
                    break;
            }

            Arcane.GetComponent<TrajectoireDroite>().f_Vitesse = f_fireSpeed;
            Arcane.tag = "Arcane";
        }
    }

    void Feu()
    /********************************************************\
     * BUT      : Réaliser le motif de feu du joueur.
     * ENTREE   : Les points d'apparition des attaques du joueur et le prefab du tir de feu.
     * SORTIE   : L'apparition des tirs de feu en suivant leurs motifs.
    \********************************************************/
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject Feu = Instantiate(o_Feu, MagicSpawns[i].position, MagicSpawns[i].rotation);
            Feu.AddComponent<TrajectoireDroite>();
            switch (i)
            {
                case 1:
                    Feu.GetComponent<TrajectoireDroite>().f_Angle = 100.0f;
                    break;
                case 2:
                    Feu.GetComponent<TrajectoireDroite>().f_Angle = 80.0f;
                    break;
                case 3:
                    Feu.GetComponent<TrajectoireDroite>().f_Angle = 110.0f;
                    break;
                case 4:
                    Feu.GetComponent<TrajectoireDroite>().f_Angle = 70.0f;
                    break;
                default:
                    Feu.GetComponent<TrajectoireDroite>().f_Angle = 90.0f;
                    break;
            }

            Feu.GetComponent<TrajectoireDroite>().f_Vitesse = f_fireSpeed;
            Feu.tag = "Feu";
        }
    }

    void Bois()
    /********************************************************\
     * BUT      : Réaliser le motif de bois du joueur.
     * ENTREE   : Les points d'apparition des attaques du joueur et le prefab du tir de bois.
     * SORTIE   : L'apparition des tirs de bois en suivant leurs motifs.
    \********************************************************/
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject Bois = Instantiate(o_Bois, MagicSpawns[i].position, MagicSpawns[i].rotation);
            Bois.AddComponent<TrajectoireDroite>();
            switch (i)
            {
                case 3:
                    Bois.GetComponent<TrajectoireDroite>().f_Angle = 180.0f;
                    break;
                case 4:
                    Bois.GetComponent<TrajectoireDroite>().f_Angle = 0.0f;
                    break;
                default:
                    Bois.GetComponent<TrajectoireDroite>().f_Angle = 90.0f;
                    break;
            }

            Bois.GetComponent<TrajectoireDroite>().f_Vitesse = f_fireSpeed;
            Bois.tag = "Bois";
        }
    }

    void Terre()
    /********************************************************\
     * BUT      : Réaliser le motif de terre du joueur.
     * ENTREE   : Les points d'apparition des attaques du joueur et le prefab du tir de terre.
     * SORTIE   : L'apparition des tirs de terre en suivant leurs motifs.
    \********************************************************/
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject Terre = Instantiate(o_Terre, MagicSpawns[i].position, MagicSpawns[i].rotation);
            if (i == 0)
            {
                Terre.AddComponent<TrajectoireDroite>();
                Terre.GetComponent<TrajectoireDroite>().f_Angle = 90.0f;
            }
            else
            {
                Terre.AddComponent<TrajectoireChercheEnnemi>();
            }
            Terre.GetComponent<Trajectoire>().f_Vitesse = f_fireSpeed;

            Terre.tag = "Terre";
        }
    }

    void Bombe()
    /********************************************************\
     * BUT      : Effectuer l'action de bombe du joueur.
     * ENTREE   : Le tag, l'état d'invincibilité, le temps et la capacité d'effectuer la bombe du joueur.
     * SORTIE   : L'appel de la méthode de bombe relative à l'élément du joueur, et l'état d'invincibilité appliqué.
    \********************************************************/
    {
        if (c_CompteurJoueur.n_Bombe>0 && !b_Invincible && b_BombePossible && c_Minuteur.GetTemps()>=5.0f)
        {
            b_Invincible = true;

            c_CompteurJoueur.ChangeBombe(-1);
            
            GameObject[] Danmakus = FindGameObjectsInLayer(LayerMask.NameToLayer("Danmaku"));

            if (Danmakus!=null)
            {
                foreach (GameObject Danmaku in Danmakus)
                {
                    Destroy(Danmaku);
                }
            }
            switch (gameObject.tag)
            {
                case "Terre":
                    BombeTerre();
                    break;
                case "Bois":
                    BombeBois();
                    break;
                case "Feu":
                    BombeFeu();
                    break;
                default:
                    BombeArcane();
                    break;
            }

            d_MomentInvincible = c_Minuteur.GetTemps();
        }
        else
        {
            Debug.Log("Plus de bombe restante.");
        }
    }

    void BombeArcane()
    /********************************************************\
     * BUT      : Réaliser la bombe d'arcane et le retour dans le temps.
     * ENTREE   : Les ennemis à l'écran et le temps.
     * SORTIE   : Le temps réduit de 5, les ennemis retiré de l'écran et la bombe prévu pour dans 5 secondes.
    \********************************************************/
    {
        b_BombePossible = false;
        GameObject[] o_Ennemis = FindGameObjectsInLayer(LayerMask.NameToLayer("Ennemi"));
        if (o_Ennemis != null)
        {
            foreach (GameObject o_Ennemi in o_Ennemis)
            {
                o_Ennemi.GetComponent<Ennemi>().Vaincu();
            }
        }
        d_TempsRetour = c_Minuteur.GetTemps();
        c_Minuteur.SetTemps(d_TempsRetour - 5);
    }

    void ArcaneExplosion()
    /********************************************************\
     * BUT      : Réaliser l'explosion à rebours de la bombe d'arcane.
     * ENTREE   : Les ennemis et projectiles ennemis à l'écran.
     * SORTIE   : La destruction des projectiles ennemis et les dégâts infligés aux ennemis.
    \********************************************************/
    {
        GameObject[] o_Ennemis = FindGameObjectsInLayer(LayerMask.NameToLayer("Ennemi"));
        if (o_Ennemis!=null)
        {
            foreach (GameObject o_Ennemi in o_Ennemis)
            {
                o_Ennemi.GetComponent<Ennemi>().InfligeDegats(10);
            }
        }

        GameObject[] Danmakus = FindGameObjectsInLayer(LayerMask.NameToLayer("Danmaku"));
        if (Danmakus != null)
        {
            foreach (GameObject Danmaku in Danmakus)
            {
                Destroy(Danmaku);
            }
        }
    }

    void BombeFeu()
    /********************************************************\
     * BUT      : Réaliser la bombe de feu.
     * ENTREE   : Les ennemis à l'écran.
     * SORTIE   : Les dégâts infligés aux ennemis.
    \********************************************************/
    {
        GameObject[] o_Ennemis = FindGameObjectsInLayer(LayerMask.NameToLayer("Ennemi"));
        if (o_Ennemis != null)
        {
            foreach (GameObject o_Ennemi in o_Ennemis)
            {
                o_Ennemi.GetComponent<Ennemi>().InfligeDegats(10);
            }
        }

    }
    
    void BombeBois()
    /********************************************************\
     * BUT      : Réaliser la bombe de bois.
     * ENTREE   : Les ennemis à l'écran.
     * SORTIE   : Les dégâts infligés aux ennemis.
    \********************************************************/
    {
        GameObject[] o_Ennemis = FindGameObjectsInLayer(LayerMask.NameToLayer("Ennemi"));
        if (o_Ennemis != null)
        {
            foreach (GameObject o_Ennemi in o_Ennemis)
            {
                o_Ennemi.GetComponent<Ennemi>().InfligeDegats(10);
            }
        }

    }
    
    void BombeTerre()
    /********************************************************\
     * BUT      : Réaliser la bombe de terre.
     * ENTREE   : Les ennemis à l'écran.
     * SORTIE   : Les dégâts infligés aux ennemis.
    \********************************************************/
    {
        GameObject[] o_Ennemis = FindGameObjectsInLayer(LayerMask.NameToLayer("Ennemi"));
        if (o_Ennemis != null)
        {
            foreach (GameObject o_Ennemi in o_Ennemis)
            {
                o_Ennemi.GetComponent<Ennemi>().InfligeDegats(10);
            }
        }
    }

    GameObject[] FindGameObjectsInLayer(int layer)
    /********************************************************\
     * BUT      : Faire une liste d'objet à partir d'un layer.
     * ENTREE   : Le layer cible.
     * SORTIE   : Le tableau des objets.
    \********************************************************/
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }
}
