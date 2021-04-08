using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteursJoueur : MonoBehaviour
{
    public int n_Bombe;
    public int n_Vie;
    public long ln_Score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScore(int n_Score)
    {
        ln_Score += n_Score;
    }

    public void ChangeVie(int Vie)
    {
        n_Vie+=Vie;
    }

    public void ChangeBombe(int Bombe)
    {
        n_Bombe+=Bombe;
    }

    public void SetScore(long Score)
    {
        ln_Score = Score;
    }

    public void SetVie(int Vie)
    {
        n_Vie = Vie;
    }

    public void SetBombe(int Bombe)
    {
        n_Bombe = Bombe;
    }
}
