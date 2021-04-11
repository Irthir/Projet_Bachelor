using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AffichageÉlément : MonoBehaviour
{
    public GameObject o_Joueur = null;
    public Text t_Text = null;

    // Start is called before the first frame update
    void Start()
    {
        if (o_Joueur==null)
        {
            o_Joueur = GameObject.Find("Joueur");
        }

        if (t_Text)
        {
            t_Text = gameObject.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        t_Text.text = "Élément : " + o_Joueur.tag;
    }
}
