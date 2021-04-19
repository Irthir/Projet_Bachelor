using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotifTirCible : Motif
/********************************************************\
 * BUT      : Réaliser des tirs ciblés.
 * ENTREE   : Le nombre de tir et l'angle entre ces derniers.
 * SORTIE   : Les tirs partant en ligne droite depuis leur point d'apparition.
\********************************************************/
{
    public GameObject Tir;
    public Transform t_Cible = null;
    private float f_Angle=0.0f;

    protected override void Start()
    {
        base.Start();
        if(t_Cible==null)
        {
            t_Cible = GameObject.Find("Joueur").GetComponent<Transform>();
        }
    }

    protected override void Spawn()
    {
        f_Angle = AngleCible();
        //Debug.Log(f_Angle);

        GameObject Instance = Instantiate(Tir, transform.position, Quaternion.identity);
        Instance.AddComponent<TrajectoireDroite>();
        Instance.GetComponent<TrajectoireDroite>().f_Angle = f_Angle;
        Instance.GetComponent<TrajectoireDroite>().f_Vitesse = f_Vitesse;
        Instance.tag = gameObject.transform.parent.tag;
    }

    float AngleCible()
    /********************************************************\
     * BUT      : Calculer l'angle 
     * ENTREE   : Le nombre de tir et l'angle entre ces derniers.
     * SORTIE   : Les tirs partant en ligne droite depuis leur point d'apparition.
    \********************************************************/
    {
        float f_x = t_Cible.position.x - transform.position.x;
        float f_y = t_Cible.position.y - transform.position.y;
        float f_Decalage = 90.0f;

        float Angle = f_Decalage - Mathf.Atan2(f_x, f_y) * Mathf.Rad2Deg;

        return Angle;
    }
}