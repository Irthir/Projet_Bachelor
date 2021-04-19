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
    private enum EVague { Vague0 = 0, Vague1 = 10, Vague2 = 20, Vague3 = 30, Vague4 = 40, Vague5 = 50, Vague6 = 60, Vague7 = 70, Vague8 = 80, Vague9 = 90, Vague10 = 100 };

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
    /********************************************************\
     * BUT      : Gérer le système de vagues.
     * ENTREE   : Les secondes entières des vagues, le temps actuel et le temps précédent.
     * SORTIE   : Le lancement des vagues à chaque seconde identifiée dans les vagues.
    \********************************************************/
    {
        //Idée d'origine, vérifier si (d_Precedent<EVague.Vague<=d_Actuel) qui n'a donc lieu qu'une fois dans la vie de l'application.
        d_Actuel = c_Minuteur.GetTemps();

        int n_Actuel = (int)d_Actuel;
        int n_Precedent = (int)d_Precedent;
        switch (n_Actuel + n_Precedent)
        {
            case ((int)EVague.Vague1 + (int)EVague.Vague1 - 1):
                Debug.Log("Première vague !");
                break;
            case ((int)EVague.Vague2 + (int)EVague.Vague2 - 1):
                Debug.Log("Deuxième vague !");
                break;
            case ((int)EVague.Vague3 + (int)EVague.Vague3 - 1):
                Debug.Log("Troisième vague !");
                break;
            case ((int)EVague.Vague4 + (int)EVague.Vague4 - 1):
                Debug.Log("Quatrième vague !");
                break;
            case ((int)EVague.Vague5 + (int)EVague.Vague5 - 1):
                Debug.Log("Cinquième vague !");
                break;
            case ((int)EVague.Vague6 + (int)EVague.Vague6 - 1):
                Debug.Log("Sixième vague !");
                break;
            case ((int)EVague.Vague7 + (int)EVague.Vague7 - 1):
                Debug.Log("Septième vague !");
                break;
            case ((int)EVague.Vague8 + (int)EVague.Vague8 - 1):
                Debug.Log("Huitième vague !");
                break;
            case ((int)EVague.Vague9 + (int)EVague.Vague9 - 1):
                Debug.Log("Neuvième vague !");
                break;
            case ((int)EVague.Vague10 + (int)EVague.Vague10 - 1):
                Debug.Log("Dixième vague !");
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
