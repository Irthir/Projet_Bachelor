using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueScriptableObject", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    [Serializable]
    public struct st_Personnage
    {
        public Sprite sp_Sprite;
        public string s_Nom;
    }

    [Serializable]
    public struct st_Dialogue
    {
        public st_Personnage p_Personnage1;
        public st_Personnage p_Personnage2;
        public string s_DialogueFR;
        public string s_DialogueEN;
        public bool b_Personnage1Actif;
    }

    public st_Dialogue[] t_Dialogues;
}
