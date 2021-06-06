using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trajectoire : MonoBehaviour
/********************************************************\
 * BUT      : Réaliser une classe mère pour toutes les trajectoires.
 * ENTREE   : La position et la date de naissance de l'objet.
 * SORTIE   : La mise à jour du temps de l'objet à appliquer sur la trajectoire enfant.
\********************************************************/
{
    public int n_XMax = 20;
    public int n_YMax = 20;

    public Minuteur c_Minuteur = null;
    public float f_Vitesse = 1f;
    protected double d_Naissance;
    protected Vector3 v_Depart;

    protected double d_Temps;
    protected float f_Delta;
    protected float f_Temps;

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

    protected virtual void Update()
    {
        if (c_Minuteur.GetTemps() != d_Temps)
            f_Delta = 1.0f;
        else
            f_Delta = 0.0f;

        d_Temps = c_Minuteur.GetTemps();
        f_Temps = (float)d_Temps - (float)d_Naissance; //Le temps actuel de l'objet.
        if (gameObject.transform.position.x > n_XMax || gameObject.transform.position.x < - n_XMax || gameObject.transform.position.y > n_YMax || gameObject.transform.position.y < -n_YMax)
        {
            Destroy(gameObject);
        }
    }

    protected float DegToRad(float f_Deg)
    {
        float fRad = f_Deg * Mathf.PI / 180;

        return fRad;
    }
}
