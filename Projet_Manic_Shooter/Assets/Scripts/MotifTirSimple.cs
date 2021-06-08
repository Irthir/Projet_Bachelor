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
    //BUT : Initialiser un ou plusieurs tirs suivant une trajectoire droite, séparés dans un arc de cercle par un angle prédéfini.
    {
        float f_AngleSeparation; //Calcul du décalage pour l'angle du premier tir, dans le sens anti-horaire.
        if (n_NombreTir > 1)
            f_AngleSeparation = -90.0f - ((f_AngleOffset * (n_NombreTir - 1)) / 2);
        else
            f_AngleSeparation = -90.0f - f_AngleOffset; //Un angle à -90° va vers le bas.

        for (int i = 0; i < n_NombreTir; i++)
        {
            GameObject Instance = Instantiate(Tir, transform.position, Quaternion.identity); //Instantiation du tir.
            Instance.AddComponent<TrajectoireDroite>(); //Ajout de la trajectoire droite.
            Instance.GetComponent<TrajectoireDroite>().f_Angle = f_AngleSeparation; //Décalage des tirs.
            Instance.GetComponent<TrajectoireDroite>().f_Vitesse = f_Vitesse; //Mise en place de la vitesse.
            Instance.tag = gameObject.transform.parent.parent.tag; //Gestion des éléments.
            
            f_AngleSeparation += f_AngleOffset;
        }
    }
}
