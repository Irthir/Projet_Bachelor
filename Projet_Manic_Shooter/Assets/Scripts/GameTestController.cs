using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTestController : MonoBehaviour
//BUT : Passer rapidement à la vague suivante lors des tests.
{
    Minuteur c_Minuteur = null;

    // Start is called before the first frame update
    void Start()
    {
        if(c_Minuteur==null)
        {
            c_Minuteur = GameObject.Find("GameManager").GetComponent<Minuteur>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(Input.inputString)
        {
            case "1":
                c_Minuteur.d_Temps = 9.9f;
                break;
            case "2":
                c_Minuteur.d_Temps = 19.9f;
                break;
            case "3":
                c_Minuteur.d_Temps = 29.9f;
                break;
            case "4":
                c_Minuteur.d_Temps = 39.9f;
                break;
            case "5":
                c_Minuteur.d_Temps = 49.9f;
                break;
            case "6":
                c_Minuteur.d_Temps = 59.9f;
                break;
            case "7":
                c_Minuteur.d_Temps = 69.9f;
                break;
            case "8":
                c_Minuteur.d_Temps = 79.9f;
                break;
            case "9":
                c_Minuteur.d_Temps = 89.9f;
                break;
            default:
                break;
        }
    }
}
