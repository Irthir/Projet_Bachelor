using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minuteur : MonoBehaviour
{
    /*********************************************************************************************************************\
    *   BUT : Garder une trace du temps en jeu et le gérer toutes les vérifications par rapport au temps se feront ici.
    *   ENTREE : Le temps mis à jour grâce au DeltaTime d'Unity quand le Minuteur est en fonction.
    *   SORTIE : Une variable de Temps lisible pour les autres éléments en ayant besoin.
    \*********************************************************************************************************************/

    public double d_Temps=0.0;
    public bool b_Actif = false;
    public GameObject o_UIPause=null;

    // Start is called before the first frame update
    void Start()
    {
        if (o_UIPause==null)
        {
            o_UIPause = GameObject.Find("PanelPause");
        }
        Play();
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

    public void SetTemps(double Temps)
    {
        d_Temps = Temps;
    }

    public void Pause()
    {
        b_Actif = false;
        o_UIPause.SetActive(true);
    }

    public void Play()
    {
        b_Actif = true;
        o_UIPause.SetActive(false);
    }

    #region code pour le singleton
    //Groupe rendant le script unique.
    private static Minuteur instance = null;

    // Game Instance Singleton
    public static Minuteur Instance
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
