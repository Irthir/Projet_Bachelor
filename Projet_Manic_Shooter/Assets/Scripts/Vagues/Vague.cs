using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vague : MonoBehaviour
/********************************************************\
 * BUT      : Être la classe mère des vagues.
 * ENTREE   : Le temps actuel et les scriptable objects des sous-vagues.
 * SORTIE   : La vie de la vague initialisée.
\********************************************************/
{
    [SerializeField] private int NumeroVague;

    public Minuteur c_Minuteur = null;
    public VagueScriptableObject[] script_SousVagues;
    protected double d_Debut=0.0f;
    protected double d_Actuel =0.0f;
    protected double d_Precedent = 0.0f;
    protected float f_Fin = 0.0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (c_Minuteur==null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }
        d_Debut = c_Minuteur.GetTemps();
        d_Actuel = d_Debut;
        d_Precedent = d_Actuel;
        if (script_SousVagues[0]!=null)
            f_Fin = script_SousVagues[0].f_TempsVague+(float)d_Actuel;

        for (int i = 0; i < script_SousVagues.Length; i++)
        {
            if (script_SousVagues[i].f_TempsVague > f_Fin)
            {
                f_Fin = script_SousVagues[i].f_TempsVague;
            }
        }
    }

    protected virtual void Update()
    {
        d_Actuel = c_Minuteur.GetTemps();
        if (d_Actuel<d_Debut || d_Actuel>(f_Fin + 6))
        {
            this.enabled = false;
        }
    }
}
