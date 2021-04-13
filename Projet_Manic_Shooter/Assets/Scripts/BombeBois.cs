using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombeBois : MonoBehaviour
/********************************************************\
 * BUT      : G�rer les collisions de la bombe de bois.
 * ENTREE   : Ses collisions.
 * SORTIE   : Les d�g�ts et les destructions.
\********************************************************/
{
    void OnTriggerEnter(Collider cible)
    /********************************************************\
     * BUT      : G�rer la collision entre l'attaque de la bombe de bois et les danmakus et les ennemis.
     * ENTREE   : L'objet avec lequel la bombe entre en contact.
     * SORTIE   : Les d�g�ts inflig�s aux ennemis et les danmakus d�truits.
    \********************************************************/
    {
        if (cible.gameObject.layer == LayerMask.NameToLayer("Danmaku"))
        {
            Destroy(cible.gameObject);
        }
        else if (cible.gameObject.layer == LayerMask.NameToLayer("Ennemi"))
        {
            cible.gameObject.GetComponent<Ennemi>().InfligeDegats(10);
        }
    }
}
