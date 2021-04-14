using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Niveau1 : MonoBehaviour
/********************************************************\
 * BUT      : Faire apparaitre et initialiser les ennemis durant le niveau.
 * ENTREE   : Les prefabs des ennemis, le temps et les points d'apparition et de déplacement.
 * SORTIE   : Les apparitions des ennemis dans le temps.
\********************************************************/
{
    private enum EVague { Vague1 = 10, Vague2 = 20, Vague3 = 30, Vague4 = 40, Vague5 = 50, Vague6 = 60, Vague7 = 70, Vague8 = 80, Vague9 = 90, Vague10 = 100 };

    Minuteur c_Minuteur = null;
    double d_Actuel = 0;
    double d_Precedent = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (c_Minuteur==null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        d_Actuel = c_Minuteur.GetTemps();

        /*switch (true)
        {
            case (d_Precedent < (double)EVague.Vague1):
            case (d_Actuel >= (double)EVague.Vague1):
                Debug.Log("Première vague !");
                break;
            default:
                break;
        }*/

        d_Precedent = d_Actuel;
    }


    #region code pour le singleton
    //Groupe rendant le script unique.
    private static Niveau1 instance = null;

    // Game Instance Singleton
    public static Niveau1 Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
}
