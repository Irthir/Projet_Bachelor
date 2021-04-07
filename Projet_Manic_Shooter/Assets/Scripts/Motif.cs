using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motif : MonoBehaviour
{
    public Minuteur c_Minuteur = null;
    public float f_Vitesse = 1f;
    protected double d_Naissance;
    protected Vector3 v_Depart;
    
    public float f_Frequence;
    protected double d_Temps;
    protected float f_Temps;
    protected float f_LastShot;

    // Start is called before the first frame update
    protected virtual void Start()
    //On initialise le minuteur si il ne l'est déjà.
    {
        if (c_Minuteur == null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

        d_Naissance = c_Minuteur.GetTemps();

        v_Depart = transform.position;

        f_LastShot = f_Temps;
    }

    protected virtual void Update()
    {
        d_Temps = c_Minuteur.GetTemps();
        f_Temps = (float)d_Temps - (float)d_Naissance; //Le temps actuel de l'objet.

        if ((f_LastShot+f_Frequence) <= f_Temps)
        {
            f_LastShot = f_Temps;
            Spawn();
        }
    }

    protected virtual void Spawn()
    {
    }
}
