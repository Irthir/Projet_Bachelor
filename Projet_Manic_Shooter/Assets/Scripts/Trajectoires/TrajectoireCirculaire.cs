using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoireCirculaire : Trajectoire
/********************************************************\
 * BUT      : Réaliser une trajectoire circulaire.
 * ENTREE   : Les informations de la classe mère trajectoire.
 * SORTIE   : Un mouvement circulaire en fonction du temps.
\********************************************************/
{
    public float f_rayon = 3.0f;
    public Vector3 v_Centre;
    public Transform f_Centre=null;
    public float f_AngleDecalage;


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (f_Centre != null)
            v_Centre = f_Centre.position;

        transform.position = MouvementCercle(f_rayon, v_Centre, f_Vitesse, f_Temps, DegToRad(f_AngleDecalage));
    }

    Vector3 MouvementCercle(float f_Rayon, Vector3 v_Centre, float f_Vitesse, float f_Temps, float f_Angle)
    {
        Vector3 vecPosition;

        vecPosition.x = f_rayon * Mathf.Cos(f_Temps * f_Vitesse + f_Angle) + v_Centre.x;
        vecPosition.y = f_rayon * Mathf.Sin(f_Temps * f_Vitesse + f_Angle) + v_Centre.y;
        vecPosition.z = 0.0f;

        return vecPosition;
    }
}
