using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoireChercheEnnemi : Trajectoire
{
    public Compteur c_Compteur = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (c_Compteur == null)
        {
            c_Compteur = GameObject.Find("GameManager").GetComponent<Compteur>();
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (c_Compteur.n_TailleListe>0)
        {
            transform.position = Vector3.MoveTowards(transform.position, ChercheEnnemiProche(c_Compteur.l_Ennemis).position, f_Vitesse*Time.deltaTime);
        }
        else
        {
            transform.position += new Vector3(0,1,0) * f_Vitesse * Time.deltaTime;
        }
    }

    Transform ChercheEnnemiProche(List<Transform> t_Ennemis)
    {
        Transform t_Min = null;
        float f_MinDist = Mathf.Infinity;
        Vector3 v_Actuel = transform.position;
        foreach (Transform t in t_Ennemis)
        {
            float f_Dist = Vector3.Distance(t.position, v_Actuel);
            if (f_Dist < f_MinDist)
            {
                t_Min = t;
                f_MinDist = f_Dist;
            }
        }
        return t_Min;
    }
}
