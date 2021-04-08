using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
    // Start is called before the first frame update
    /********************************************************\
     * BUT      : Mettre en place les limites de l'écran.
     * ENTREE   : Les limites de ce que voit la caméra.
     * SORTIE   : Les limites de l'écran stockées.
    \********************************************************/
    public void SetScreenBounds()
    {
        Vector3 v_ScreenMaxBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        Vector3 v_ScreenMinBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - Screen.width, Screen.height - Screen.height,0));
        xMax = v_ScreenMaxBoundaries.x;
        xMin = v_ScreenMinBoundaries.x;
        yMax = v_ScreenMaxBoundaries.y;
        yMin = v_ScreenMinBoundaries.y;
    }
}

public class PlayerController : MonoBehaviour
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

    public CompteursJoueur c_Compteur = null;

    public Minuteur c_Minuteur = null;

    //Functions

    // Start is called before the first frame update
    void Start()
    {
        boundary.SetScreenBounds();
        rb = GetComponent<Rigidbody>();

        if (c_Compteur == null)
        {
            c_Compteur = GameObject.Find("GameManager").GetComponent<CompteursJoueur>();
        }

        if (c_Minuteur == null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

        f_speed = f_Vitesse;
        f_fireRate = f_TauxTir;
        f_fireSpeed = f_VitesseTir;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1")&& Time.time>nextFire)
        {
            nextFire = Time.time + f_fireRate;
            Tir();
        }

        if (Input.GetButtonDown("Suivant"))
        {
            switch (gameObject.tag)
            {
                case "Feu" :
                    gameObject.tag = "Bois";
                    break;
                case "Bois" :
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
                case "Bois" :
                    gameObject.tag = "Feu";
                    break;
                case "Terre" :
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
            f_fireSpeed =  f_VitesseTir + f_AugmentationVitesseTir;
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

        if (b_Invincible)
        {
            if (d_MomentInvincible+f_TempsInvincible<=c_Minuteur.GetTemps())
            {
                b_Invincible = false;
                Debug.Log("Plus Invincible");
            }
        }
    }

    //Fixed Update is called before each fixed physic step
    void FixedUpdate()
    {
        //Associations des inputs pour le déplacement
        float f_moveHorizontal = Input.GetAxis("Horizontal");
        float f_moveVertical = Input.GetAxis("Vertical");

        //Réalisation du déplacement
        Vector3 v_movement = new Vector3(f_moveHorizontal, f_moveVertical, 0.0f);
        rb.velocity = v_movement*f_speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );

        rb.rotation = Quaternion.Euler(0.0f, GetComponent<Rigidbody>().velocity.x * -f_tilt, 0.0f);
    }

    void Tir()
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
    {
        if (c_Compteur.n_Bombe>0 && !b_Invincible)
        {
            b_Invincible = true;
            d_MomentInvincible = c_Minuteur.GetTemps();
            Debug.Log("Invincible");

            c_Compteur.ChangeBombe(-1);
            Debug.Log("Bombe");
            
            GameObject[] Danmakus = FindGameObjectsInLayer(LayerMask.NameToLayer("Danmaku"));
            
            foreach (GameObject Danmaku in Danmakus)
            {
                Destroy(Danmaku);
            }
        }
        else
        {
            Debug.Log("Plus de bombe restante.");
        }
    }

    GameObject[] FindGameObjectsInLayer(int layer)
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
