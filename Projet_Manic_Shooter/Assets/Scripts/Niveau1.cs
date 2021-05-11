using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Niveau1 : MonoBehaviour
/********************************************************\
 * BUT      : Faire apparaitre et initialiser les ennemis durant le niveau.
 * ENTREE   : Les prefabs des ennemis, le temps et les points d'apparition et de d�placement.
 * SORTIE   : Les apparitions des ennemis dans le temps.
\********************************************************/
{
    private enum EVague { Vague0 = 0, Vague1 = 10, Vague2 = 20, Vague3 = 30, Vague4 = 40, Vague5 = 50, Vague6 = 60, Vague7 = 70, Vague8 = 80, Vague9 = 90, Vague10 = 100 };

    Minuteur c_Minuteur = null;
    double d_Actuel = 0;
    double d_Precedent = 0;
    [SerializeField]
    Vague[] c_Vagues;


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
    /********************************************************\
     * BUT      : G�rer le syst�me de vagues.
     * ENTREE   : Les secondes enti�res des vagues, le temps actuel et le temps pr�c�dent.
     * SORTIE   : Le lancement des vagues � chaque seconde identifi�e dans les vagues.
    \********************************************************/
    {
        //Id�e d'origine, v�rifier si (d_Precedent<EVague.Vague<=d_Actuel) qui n'a donc lieu qu'une fois dans la vie de l'application.
        d_Actuel = c_Minuteur.GetTemps();

        int n_Actuel = (int)d_Actuel;
        int n_Precedent = (int)d_Precedent;
        switch (n_Actuel + n_Precedent)
        {
            case ((int)EVague.Vague1 + (int)EVague.Vague1 - 1):
                //Debug.Log("Premi�re vague !");
                c_Vagues[0].enabled = true;
                break;
            case ((int)EVague.Vague2 + (int)EVague.Vague2 - 1):
                //Debug.Log("Deuxi�me vague !");
                c_Vagues[1].enabled = true;
                break;
            case ((int)EVague.Vague3 + (int)EVague.Vague3 - 1):
                //Debug.Log("Troisi�me vague !");
                c_Vagues[2].enabled = true;
                break;
            case ((int)EVague.Vague4 + (int)EVague.Vague4 - 1):
                //Debug.Log("Quatri�me vague !");
                c_Vagues[3].enabled = true;
                break;
            case ((int)EVague.Vague5 + (int)EVague.Vague5 - 1):
                Debug.Log("Cinqui�me vague !");
                //c_Vagues[4].enabled = true;
                break;
            case ((int)EVague.Vague6 + (int)EVague.Vague6 - 1):
                Debug.Log("Sixi�me vague !");
                //c_Vagues[5].enabled = true;
                break;
            case ((int)EVague.Vague7 + (int)EVague.Vague7 - 1):
                Debug.Log("Septi�me vague !");
                //c_Vagues[6].enabled = true;
                break;
            case ((int)EVague.Vague8 + (int)EVague.Vague8 - 1):
                Debug.Log("Huiti�me vague !");
                //c_Vagues[7].enabled = true;
                break;
            case ((int)EVague.Vague9 + (int)EVague.Vague9 - 1):
                Debug.Log("Neuvi�me vague !");
                //c_Vagues[8].enabled = true;
                break;
            case ((int)EVague.Vague10 + (int)EVague.Vague10 - 1):
                Debug.Log("Dixi�me vague !");
                //c_Vagues[9].enabled = true;
                break;
            default:
                break;
        }

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
