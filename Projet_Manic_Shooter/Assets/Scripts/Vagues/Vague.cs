using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vague : MonoBehaviour
{
    public Minuteur c_Minuteur = null;
    public VagueScriptableObject[] script_SousVagues;
    double d_Debut;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (c_Minuteur==null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }
        d_Debut = c_Minuteur.GetTemps();
    }
}
