using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AffichageMinuteur : MonoBehaviour
/********************************************************\
 * BUT      : Régler l'affichage du minuteur sur l'interface.
 * ENTREE   : Le minuteur et le texte à modifier.
 * SORTIE   : Le texte modifiés.
\********************************************************/
{
    public Minuteur c_Minuteur = null;
    public Text t_Text = null;

    // Start is called before the first frame update
    void Start()
    //BUT : Initialiser les références.
    {
        if (c_Minuteur == null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }

        if (t_Text==null)
        {
            t_Text = gameObject.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    //BUT : Mettre à jour l'affichage du temps.
    {
        t_Text.text = Mathf.Round((float)c_Minuteur.GetTemps()).ToString();
    }
}
