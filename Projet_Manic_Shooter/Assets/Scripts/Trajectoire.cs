using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectoire : MonoBehaviour
{
    public Minuteur c_Minuteur = null;
    public float f_vitesse = 1f;
    protected double d_Naissance;
    protected Vector3 v_Depart;

    // Start is called before the first frame update
    protected virtual void Start()
    //On initialise le minuteur si il ne l'est déjà.
    {
        if (c_Minuteur==null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

        d_Naissance = c_Minuteur.GetTemps();

        v_Depart = transform.position;
    }
}
