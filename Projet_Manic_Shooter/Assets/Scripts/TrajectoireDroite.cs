using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoireDroite : Trajectoire
{
    public float f_Angle = 90.0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //On réduit à un angle lisible.
        while (f_Angle>360.0f)
        {
            f_Angle -= 360.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        double d_Temps = c_Minuteur.GetTemps();
        float f_Temps = (float)d_Temps - (float)d_Naissance; //Le temps actuel de l'objet.

        transform.position = MouvementDroite(f_Angle, v_Depart, f_vitesse, f_Temps);
    }

    Vector3 MouvementDroite(float f_Angle,Vector3 v_Origine,float f_vitesse, float f_Temps)
    {
        Vector3 vecPosition;

        f_Angle *= Mathf.PI / 180;

        vecPosition.x = f_Temps * f_vitesse * Mathf.Cos(f_Angle) + v_Origine.x;
        vecPosition.y = f_Temps * f_vitesse * Mathf.Sin(f_Angle) + v_Origine.y;

        //Debug.Log("Origine X "+v_Origine.x+" Origine Y "+ v_Origine.y+" Cos(90) " + Mathf.Cos(f_Angle)+)

        vecPosition.z = 0.0f;

        return vecPosition;
    }
}
