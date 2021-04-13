using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombeFeu : MonoBehaviour
/********************************************************\
 * BUT      : Générer une bombe de feu qui va croitre et infliger des dégâts aux ennemis.
 * ENTREE   : La taille maximum de la bombe de feu et ses ratios d'augmentation ainsi que sa vitesse.
 * SORTIE   : La bombe qui croit avec le temps avant de disparaitre.
\********************************************************/
{
    private const float RATIOLIGHT = 3.0f;
    private const float RATIOSCALE = 2.0f;
    private const float SCALEMAX = 70.0f;

    private double d_Actuel = 0.0f;

    public SphereCollider s_Collider = null;
    public Transform t_Transform = null;
    public Light l_Light = null;

    public float f_VitesseExpansion = 1.0f;

    public Minuteur c_Minuteur = null;

    // Start is called before the first frame update
    void Start()
    //BUT : Initialiser les références et le temps actuel de l'objet.
    {
        if (c_Minuteur==null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

        d_Actuel = c_Minuteur.GetTemps();
    }

    // Update is called once per frame
    void Update()
    //BUT : Mettre à jour la croissance de la bombe de feu.
    {
        if (d_Actuel > c_Minuteur.GetTemps()) //Pour sauver la feature qui est un bug.
        {
            Destroy(gameObject);
        }

        float f_Delta = (float)(c_Minuteur.GetTemps() - d_Actuel);
        d_Actuel = c_Minuteur.GetTemps();

        l_Light.range += f_Delta * RATIOLIGHT * f_VitesseExpansion;

        Vector3 v_Echelle = t_Transform.localScale;
        v_Echelle.x += f_Delta * RATIOSCALE * f_VitesseExpansion;
        v_Echelle.y = v_Echelle.x;
        t_Transform.localScale = v_Echelle;
        if (v_Echelle.x > SCALEMAX)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider cible)
    /********************************************************\
     * BUT      : Gérer la collision entre l'attaque de la bombe de feu et les danmakus et les ennemis.
     * ENTREE   : L'objet avec lequel la bombe entre en contact.
     * SORTIE   : Les dégâts infligés aux ennemis et les danmakus détruits.
    \********************************************************/
    {
        if (cible.gameObject.layer == LayerMask.NameToLayer("Danmaku"))
        {
            Destroy(cible.gameObject);
        }
        else if (cible.gameObject.layer == LayerMask.NameToLayer("Ennemi"))
        {
            cible.gameObject.GetComponent<Ennemi>().InfligeDegats(10);
        }
    }
}