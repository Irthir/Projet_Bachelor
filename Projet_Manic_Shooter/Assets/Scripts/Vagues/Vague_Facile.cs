using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vague_Facile : Vague
/********************************************************\
 * BUT      : Gérer l'appel des sous-vagues durant la vie de la vague.
 * ENTREE   : Les sous-vagues et le temps.
 * SORTIE   : Les ennemis invoqués et initialisés en jeu.
\********************************************************/
{
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        double d_Relatif_Actuel = d_Actuel - d_Debut;
        double d_Relatif_Precedent = d_Precedent - d_Debut;

        for (int i = 0; i < script_SousVagues.Length; i++)
        {
            for (int j = 0; j < script_SousVagues[i].nb_Ennemis; j++)
            {
                float f_TempsCible = ((script_SousVagues[i].f_TempsVague / 2) / script_SousVagues[i].nb_Ennemis) * (j + 1);
                if ((float)d_Relatif_Precedent<f_TempsCible && (float)d_Relatif_Actuel>=f_TempsCible)
                {
                    GameObject Trajectoire = Instantiate(script_SousVagues[i].prefabTrajectoire, script_SousVagues[i].prefabPointApparition.transform.position,Quaternion.identity);
                    GameObject Ennemi = Instantiate(script_SousVagues[i].prefabEnnemi, Trajectoire.transform);
                    Ennemi.tag = script_SousVagues[i].Tag;
                    GameObject Motif = Instantiate(script_SousVagues[i].prefabMotif, Ennemi.transform);
                }
            }
        }
        d_Precedent = d_Actuel;
    }
}
