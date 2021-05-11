using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotifTirSimple : Motif
/********************************************************\
 * BUT      : Réaliser des tirs simples.
 * ENTREE   : Le nombre de tir et l'angle entre ces derniers.
 * SORTIE   : Les tirs partant en ligne droite depuis leur point d'apparition.
\********************************************************/
{
    public GameObject Tir;
    public int n_NombreTir = 1;
    public float f_AngleOffset;


    protected override void Spawn()
    {
        float f_AngleSeparation = -90.0f;
        if (n_NombreTir > 1)
            f_AngleSeparation = -90.0f - ((f_AngleOffset * (n_NombreTir - 1)) / 2);
        else
            f_AngleSeparation = -90.0f - f_AngleOffset;

        for (int i = 0; i < n_NombreTir; i++)
        {
            GameObject Instance = Instantiate(Tir, transform.position, Quaternion.identity);
            Instance.AddComponent<TrajectoireDroite>();
            Instance.GetComponent<TrajectoireDroite>().f_Angle = f_AngleSeparation;
            Instance.GetComponent<TrajectoireDroite>().f_Vitesse = f_Vitesse;
            Instance.tag = gameObject.transform.parent.parent.tag; ;
            
            f_AngleSeparation += f_AngleOffset;
        }
    }
}
