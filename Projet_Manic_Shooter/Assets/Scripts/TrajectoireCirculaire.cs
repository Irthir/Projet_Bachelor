using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoireCirculaire : Trajectoire
{
    public float f_rayon = 3.0f;
    public float f_Ox;// = v_Depart.x - f_rayon;
    public float f_Oy;// = v_Depart.y;


    // Update is called once per frame
    void Update()
    {
        double d_Temps = c_Minuteur.GetTemps();
        float f_Temps = (float)d_Temps - (float)d_Naissance; //Le temps actuel de l'objet.

        transform.position = MouvementCercle(f_rayon, f_Ox, f_Oy, f_vitesse, f_Temps);
    }

    Vector3 MouvementCercle(float f_Rayon, float f_Ox, float f_Oy, float f_Vitesse, float f_Temps)
    {
        Vector3 vecPosition;

        vecPosition.x = f_rayon * Mathf.Cos(f_Temps * f_vitesse) + f_Ox;
        vecPosition.y = f_rayon * Mathf.Sin(f_Temps * f_vitesse) + f_Oy;
        vecPosition.z = 0.0f;

        return vecPosition;
    }
}
