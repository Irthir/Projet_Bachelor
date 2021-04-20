using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoireChercheCible : Trajectoire
/********************************************************\
 * BUT      : Réaliser une trajectoire pour aller vers la cible.
 * ENTREE   : La classe mère trajectoire et la cible.
 * SORTIE   : Les déplacements vers la cible.
\********************************************************/
{
    public Compteur c_Compteur = null;
    public Vector3 v_Cible;

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

        transform.position = Vector3.MoveTowards(transform.position, v_Cible, f_Vitesse * f_Delta * Time.deltaTime);
    }
}
