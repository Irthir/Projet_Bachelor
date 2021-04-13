using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombeBois : MonoBehaviour
/********************************************************\
 * BUT      : Gérer les collisions de la bombe de bois.
 * ENTREE   : Ses collisions.
 * SORTIE   : Les dégâts et les destructions.
\********************************************************/
{
    void OnTriggerEnter(Collider cible)
    /********************************************************\
     * BUT      : Gérer la collision entre l'attaque de la bombe de bois et les danmakus et les ennemis.
     * ENTREE   : L'objet avec lequel la bombe entre en contact.
     * SORTIE   : Les dégâts infligés aux ennemis et les danmakus détruits.
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
