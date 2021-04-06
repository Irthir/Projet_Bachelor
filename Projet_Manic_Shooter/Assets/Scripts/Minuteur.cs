using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minuteur : MonoBehaviour
{
    /*********************************************************************************************************************\
    *   BUT : Garder une trace du temps en jeu et le g�rer toutes les v�rifications par rapport au temps se feront ici.
    *   ENTREE : Le temps mis � jour gr�ce au DeltaTime d'Unity quand le Minuteur est en fonction.
    *   SORTIE : Une variable de Temps lisible pour les autres �l�ments en ayant besoin.
    \*********************************************************************************************************************/

    private double d_Temps=0.0;
    private bool b_Actif = false;

    // Start is called before the first frame update
    void Start()
    {
        b_Actif = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (b_Actif)
        {
            d_Temps += Time.deltaTime;
        }
    }

    public double GetTemps()
    {
        return d_Temps;
    }

    public void Pause()
    {
        b_Actif = false;
    }

    public void Play()
    {
        b_Actif = true;
    }
}
