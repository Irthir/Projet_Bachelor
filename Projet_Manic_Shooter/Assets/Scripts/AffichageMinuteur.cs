using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AffichageMinuteur : MonoBehaviour
{
    public Minuteur c_Minuteur = null;
    public Text t_Text = null;

    // Start is called before the first frame update
    void Start()
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
    {
        t_Text.text = Mathf.Round((float)c_Minuteur.GetTemps()).ToString();
    }
}
