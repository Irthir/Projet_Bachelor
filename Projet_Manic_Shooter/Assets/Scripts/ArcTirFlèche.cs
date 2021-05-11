using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcTirFlèche : MonoBehaviour
{
    [System.Serializable]
    public struct PointsDePassage
    {
        public GameObject[] go_PointsDePassage;
    }
    public PointsDePassage[] st_PointsDePassage;
    
    public GameObject[] go_Flèches;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < go_Flèches.Length; i++)
        {
            GameObject Fleche = Instantiate(go_Flèches[i], st_PointsDePassage[0].go_PointsDePassage[i].transform.position, Quaternion.identity);
            Chemin c_Chemin = Fleche.GetComponent<Chemin>();

            c_Chemin.o_PointDePassage = new GameObject[st_PointsDePassage.Length-1];
            c_Chemin.f_Delai = new float[st_PointsDePassage.Length-1];

            for (int j = 1; j < st_PointsDePassage.Length; j++)
            {
                c_Chemin.o_PointDePassage[j-1] = st_PointsDePassage[j].go_PointsDePassage[i];
                c_Chemin.f_Delai[j-1] = 0.0f;
            }
        }

        Destroy(gameObject);
    }
}
