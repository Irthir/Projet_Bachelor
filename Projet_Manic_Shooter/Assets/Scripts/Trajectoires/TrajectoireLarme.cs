using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoireLarme : Trajectoire
/********************************************************\
* BUT      : Réaliser une trajectoire larme.
* ENTREE   : Les informations de la classe mère trajectoire.
* SORTIE   : Un mouvement en forme de larme en fonction du temps.
\********************************************************/
{
    public Vector3 v_Origine;
    public float f_Alpha = 1.0f;
    public float f_PuissanceN = 1.0f;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        v_Origine = transform.position;

        transform.position = MouvementLarme(f_Vitesse, f_Temps);
    }

    Vector3 MouvementLarme(float f_Vitesse, float f_Temps)
    {
        Vector3 vecPosition=new Vector3();

        float a = f_Alpha;
        float t = f_Temps * f_Vitesse;

        vecPosition.x = a*Mathf.Cos(t)+v_Origine.x;
        vecPosition.y = a*Mathf.Sin(t)* Mathf.Pow((Mathf.Sin(t/2)), f_PuissanceN) + v_Origine.y;

        return vecPosition;
    }
}
