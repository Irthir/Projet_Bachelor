using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoireDroite : Trajectoire
/********************************************************\
 * BUT      : Réaliser une trajectoire droite.
 * ENTREE   : Les informations de la classe mère trajectoire.
 * SORTIE   : Un mouvement en ligne droite en fonction du temps.
\********************************************************/
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
    protected override void Update()
    {
        base.Update();

        transform.position = MouvementDroite(DegToRad(f_Angle), v_Depart, f_Vitesse, f_Temps);
    }

    Vector3 MouvementDroite(float f_Angle,Vector3 v_Origine,float f_Vitesse, float f_Temps)
    //BUT : Calculer la position de l'objet sur une trajectoire droite à partir de son origine et sa vitesse.
    {
        Vector3 vecPosition;

        vecPosition.x = f_Temps * f_Vitesse * Mathf.Cos(f_Angle) + v_Origine.x;
        vecPosition.y = f_Temps * f_Vitesse * Mathf.Sin(f_Angle) + v_Origine.y;
        vecPosition.z = 0.0f;

        return vecPosition;
    }
}
