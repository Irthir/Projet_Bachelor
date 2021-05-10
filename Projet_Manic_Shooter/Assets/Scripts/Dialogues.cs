using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Dialogues : MonoBehaviour
/***************************************************************************\
 * BUT      : Gérer l'affichage des dialogues à l'écran.
 * ENTREE   : Le ScriptableObject qui stoque les informations des dialogues.
 * SORTIE   : L'affichage des dialogues passés sur input du joueur.
\***************************************************************************/
{
    public GameObject UI_Dialogue = null;
    public Text t_Text = null;
    public Text t_Personnage1 = null;
    public GameObject nom_Personnage1 = null;
    public Text t_Personnage2 = null;
    public GameObject nom_Personnage2 = null;
    public Image c_Personnage1 = null;
    public Image c_Personnage2 = null;
    public RectTransform rt_Personnage1 = null;
    public RectTransform rt_Personnage2 = null;

    public DialogueScriptableObject so_Dialogues;
    public PlayerController s_PlayerController = null;
    public Minuteur s_Minuteur = null;

    [SerializeField]
    protected float f_TempsParLettre = 0.02f;
    protected int n_Index = 0;

    protected bool b_Coroutine = false;

    protected IEnumerator AffichageTexte(string s_Chaine)
    {
        b_Coroutine = true;
        t_Text.text = "";
        foreach (char c_Lettre in s_Chaine)
        {
            t_Text.text += c_Lettre;
            yield return new WaitForSeconds(f_TempsParLettre);
        }
        b_Coroutine = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Debug.Log(CultureInfo.CurrentCulture); //retourne fr-FR ou en-EN;
    }

    protected virtual void Update()
    {
        if (Input.GetButtonDown("Fire1") && !b_Coroutine)
        {
            if (n_Index<so_Dialogues.t_Dialogues.Length-1)
            {
                n_Index++;
                //Debug.Log(Application.systemLanguage); //retourne French ou English;
                AffichageDialogue();
            }
            else
            {
                UI_Dialogue.SetActive(false);
                s_PlayerController.enabled = true;
                s_Minuteur.Play();
            }
        }
    }

    protected void AffichageDialogue()
    {
        t_Personnage1.text = so_Dialogues.t_Dialogues[n_Index].p_Personnage1.s_Nom;
        t_Personnage2.text = so_Dialogues.t_Dialogues[n_Index].p_Personnage2.s_Nom;

        c_Personnage1.sprite = so_Dialogues.t_Dialogues[n_Index].p_Personnage1.sp_Sprite;
        c_Personnage2.sprite = so_Dialogues.t_Dialogues[n_Index].p_Personnage2.sp_Sprite;

        if (so_Dialogues.t_Dialogues[n_Index].b_Personnage1Actif)
        {
            nom_Personnage1.SetActive(true);
            nom_Personnage2.SetActive(false);

            Color Color_p1 = c_Personnage1.color;
            Color_p1.a = 1f;
            c_Personnage1.color = Color_p1;

            Color Color_p2 = c_Personnage2.color;
            Color_p2.a = 0.8f;
            c_Personnage2.color = Color_p2;

            Vector3 Vector_p1 = rt_Personnage1.localPosition;
            Vector_p1.y = 450;
            rt_Personnage1.localPosition = Vector_p1;

            Vector3 Vector_p2 = rt_Personnage2.localPosition;
            Vector_p2.y = 400;
            rt_Personnage2.localPosition = Vector_p2;
        }
        else
        {
            nom_Personnage1.SetActive(false);
            nom_Personnage2.SetActive(true);

            Color Color_p1 = c_Personnage1.color;
            Color_p1.a = 0.8f;
            c_Personnage1.color = Color_p1;

            Color Color_p2 = c_Personnage2.color;
            Color_p2.a = 1f;
            c_Personnage2.color = Color_p2;

            Vector3 Vector_p1 = rt_Personnage1.localPosition;
            Vector_p1.y = 400;
            rt_Personnage1.localPosition = Vector_p1;

            Vector3 Vector_p2 = rt_Personnage2.localPosition;
            Vector_p2.y = 450;
            rt_Personnage2.localPosition = Vector_p2;
        }

        //Gestion de l'affichage du dialogue.
        if (CultureInfo.CurrentCulture.ToString().Contains("en") || Application.systemLanguage.ToString().Contains("english"))
        {
            StartCoroutine(AffichageTexte(so_Dialogues.t_Dialogues[n_Index].s_DialogueEN));
        }
        else
        {
            StartCoroutine(AffichageTexte(so_Dialogues.t_Dialogues[n_Index].s_DialogueFR));
        }
    }
}
