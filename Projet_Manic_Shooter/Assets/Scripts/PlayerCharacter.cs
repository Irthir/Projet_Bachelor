using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public RectTransform rectTerrain = null;
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

        xMin = rect.xMin;
        xMax = rect.xMax;
        yMin = rect.yMin;
        yMax = rect.yMax;
    }
}


public class PlayerCharacter : MonoBehaviour
{
    public float f_tilt = 3.0f;

    public bool b_Invincible = false;
    public float f_TempsInvincible = 2.0f;
    private double d_MomentInvincible = 0.0f;

    public float f_Vitesse = 0.0f;
    public float f_VitesseTir = 0.0f;
    public float f_TauxTir = 0.0f;

    public Transform[] MagicSpawns;    

    private bool b_BombePossible = true;
    private double d_TempsRetour = 0.0f;

    //Références
    public Boundary boundary;
    public Rigidbody rb; //rb parce qu'il n'y a qu'un seul Rigidbody par objet et que le nom rigidbody est déjà pris dans la hiérarchie.

    public CompteursJoueur c_CompteurJoueur = null;
    public Compteur c_Compteur = null;
    public Minuteur c_Minuteur = null;

    public CollisionJoueur c_Collision = null;

    //Prefabs
    public GameObject o_Feu;
    public GameObject o_Arcane;
    public GameObject o_Bois;
    public GameObject o_Terre;

    public GameObject o_BombeFeu;
    public GameObject o_BombeArcane;
    public GameObject o_BombeBois;
    public GameObject o_BombeTerre;


    // Start is called before the first frame update
    void Start()
    /********************************************************\
     * BUT      : Mettre en place les variables dont le PlayerCharacter a besoin au lancement
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

        if (c_Compteur == null)
        {
            c_Compteur = GameObject.Find("GameManager").GetComponent<Compteur>();
        }

        if (c_Collision == null)
        {
            c_Collision = GameObject.Find("Collisionneur").GetComponent<CollisionJoueur>();
        }

        if (c_Minuteur == null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void Deplacement(float f_moveHorizontal, float f_moveVertical, float f_Delta)
    /********************************************************\
     * BUT      : Réaliser le déplacement du personnage.
     * ENTREE   : Le déplacement horizontal, vertical et le delta.
     * SORTIE   : Les déplacements du joueur.
    \********************************************************/
    {
        if (c_Minuteur.b_Actif)
        {
            //Réalisation du déplacement
            Vector3 v_movement = new Vector3(f_moveHorizontal, f_moveVertical, 0.0f);
            rb.velocity = v_movement * f_Vitesse * f_Delta;

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

    public void Tir()
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

            Arcane.GetComponent<TrajectoireDroite>().f_Vitesse = f_VitesseTir;
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

            Feu.GetComponent<TrajectoireDroite>().f_Vitesse = f_VitesseTir;
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

            Bois.GetComponent<TrajectoireDroite>().f_Vitesse = f_VitesseTir;
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
            Terre.GetComponent<Trajectoire>().f_Vitesse = f_VitesseTir;

            Terre.tag = "Terre";
        }
    }

    public void Bombe()
    /********************************************************\
     * BUT      : Effectuer l'action de bombe du joueur.
     * ENTREE   : Le tag, l'état d'invincibilité, le temps et la capacité d'effectuer la bombe du joueur.
     * SORTIE   : L'appel de la méthode de bombe relative à l'élément du joueur, et l'état d'invincibilité appliqué.
    \********************************************************/
    {
        if (c_CompteurJoueur.f_Bombe > 0 && !b_Invincible && b_BombePossible && c_Minuteur.GetTemps() >= 5.0f)
        {
            b_Invincible = true;

            c_CompteurJoueur.ChangeBombe(-1);

            NettoieEcran();

            switch (gameObject.tag)
            {
                case "Terre":
                    StartCoroutine("BombeTerre");
                    break;
                case "Bois":
                    StartCoroutine("BombeBois");
                    break;
                case "Feu":
                    StartCoroutine("BombeFeu");
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
        Explosion();

        NettoieEcran();
    }

    IEnumerator BombeFeu()
    /********************************************************\
     * BUT      : Réaliser la bombe de feu.
     * ENTREE   : Le nombre de bombes à réaliser.
     * SORTIE   : La bombes de feu qui apparaissent en séquence.
    \********************************************************/
    {
        int nb_Bombe = 3;
        while (nb_Bombe > 0)
        {
            Instantiate(o_BombeFeu, transform.position, Quaternion.identity);
            nb_Bombe--;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator BombeBois()
    /********************************************************\
     * BUT      : Réaliser la bombe de bois.
     * ENTREE   : Les ennemis à l'écran.
     * SORTIE   : Les dégâts infligés aux ennemis.
    \********************************************************/
    {
        int nb_Bombe = 5;
        while (nb_Bombe > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject BombeBois = null;
                switch (i)
                {
                    case 0:
                        BombeBois = Instantiate(o_BombeBois, MagicSpawns[1].position, Quaternion.identity);
                        BombeBois.AddComponent<TrajectoireDroite>();
                        break;
                    case 1:
                        BombeBois = Instantiate(o_BombeBois, MagicSpawns[2].position, Quaternion.identity);
                        BombeBois.AddComponent<TrajectoireDroite>();
                        break;
                    case 2:
                        BombeBois = Instantiate(o_BombeBois, MagicSpawns[5].position, MagicSpawns[5].rotation);
                        BombeBois.AddComponent<TrajectoireDroite>();
                        BombeBois.GetComponent<TrajectoireDroite>().f_Angle = 0.0f;
                        break;
                    case 3:
                        BombeBois = Instantiate(o_BombeBois, MagicSpawns[6].position, MagicSpawns[6].rotation);
                        BombeBois.AddComponent<TrajectoireDroite>();
                        BombeBois.GetComponent<TrajectoireDroite>().f_Angle = 180.0f;
                        break;
                    case 4:
                        BombeBois = Instantiate(o_BombeBois, MagicSpawns[7].position, MagicSpawns[7].rotation);
                        BombeBois.AddComponent<TrajectoireDroite>();
                        BombeBois.GetComponent<TrajectoireDroite>().f_Angle = 270.0f;
                        break;
                    default:
                        break;
                }
                if (BombeBois != null)
                    BombeBois.GetComponent<TrajectoireDroite>().f_Vitesse = f_VitesseTir;
            }

            nb_Bombe--;
            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator BombeTerre()
    /********************************************************\
     * BUT      : Réaliser la bombe de terre.
     * ENTREE   : La mise en place du bouclier du joueur.
     * SORTIE   : Le regain de points de bombes si le bouclier n'est pas tombé.
    \********************************************************/
    {
        c_Collision.b_BombeTerre = true;
        yield return new WaitForSeconds(5.0f);
        if (c_Collision.b_BombeTerre)
        {
            c_CompteurJoueur.ChangeBombe(0.5f);
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

    public void NettoieEcran(bool b_GainScore = true)
    /********************************************************\
     * BUT      : Nettoyer l'écran des attaques ennemies.
     * ENTREE   : Les attaques ennemies.
     * SORTIE   : Les attaques ennemies supprimées.
    \********************************************************/
    {
        GameObject[] Danmakus = FindGameObjectsInLayer(LayerMask.NameToLayer("Danmaku"));

        if (Danmakus != null)
        {
            foreach (GameObject Danmaku in Danmakus)
            {
                Destroy(Danmaku);
                if (b_GainScore)
                {
                    c_CompteurJoueur.ChangeScore(1);
                }
            }
        }
    }

    public void Explosion()
    /********************************************************\
     * BUT      : Infliger des dégâts à tous les ennemis à l'écran.
     * ENTREE   : Les ennemis à l'écran.
     * SORTIE   : Les dégât infligés aux ennemis.
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

    #region code pour le singleton
    //Groupe rendant le script unique.
    private static PlayerCharacter instance = null;

    // Game Instance Singleton
    public static PlayerCharacter Instance
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
