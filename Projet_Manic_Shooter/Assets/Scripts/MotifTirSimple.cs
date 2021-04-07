using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotifTirSimple : Motif
{
    public GameObject Tir;
    public int n_NombreTir = 1;
    public float f_AngleOffset;


    protected override void Spawn()
    {
        float f_AngleSeparation = -90.0f - ((f_AngleOffset * n_NombreTir)/2);
        for (int i = 0; i < n_NombreTir; i++)
        {
            GameObject Instance = Instantiate(Tir, transform.position, Quaternion.identity);
            Instance.AddComponent<TrajectoireDroite>();
            Instance.GetComponent<TrajectoireDroite>().f_Angle = f_AngleSeparation;
            Instance.GetComponent<TrajectoireDroite>().f_Vitesse = f_Vitesse;
            Instance.tag = gameObject.tag;
            
            f_AngleSeparation += f_AngleOffset;
        }
    }
}
