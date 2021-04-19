using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/VagueScriptableObject", order=1)]
public class VagueScriptableObject : ScriptableObject
{
    public GameObject prefabsEnnemis;

    public int nb_Ennemis;

    [Tooltip("Les ennemis apparaitront � interval r�gulier dans la premi�re moiti� du temps de la vague.")]
    public float f_TempsVague;

    public string Tag;
}
