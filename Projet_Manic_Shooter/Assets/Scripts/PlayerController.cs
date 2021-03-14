using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
    // Start is called before the first frame update
    /********************************************************\
     * BUT      : Mettre en place les limites de l'écran.
     * ENTREE   : Les limites de ce que voit la caméra.
     * SORTIE   : Les limites de l'écran stockées.
    \********************************************************/
    public void SetScreenBounds()
    {
        Vector3 v_ScreenMaxBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        Vector3 v_ScreenMinBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - Screen.width, Screen.height - Screen.height,0));
        xMax = v_ScreenMaxBoundaries.x;
        xMin = v_ScreenMinBoundaries.x;
        yMax = v_ScreenMaxBoundaries.y;
        yMin = v_ScreenMinBoundaries.y;
    }
}

public class PlayerController : MonoBehaviour
{
    //Header
    public float f_speed=10.0f;
    public float f_tilt=3.0f;
    public Boundary boundary;
    public Rigidbody rb; //rb parce qu'il n'y a qu'un seul Rigidbody par objet et que le nom rigidbody est déjà pris dans la hiérarchie.

    public GameObject shot;
    public Transform shotSpawn;
    
    private float fireRate = 0.1f;
    private float nextFire = 0.0f;

    //Functions

    // Start is called before the first frame update
    void Start()
    {
        boundary.SetScreenBounds();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetButton("Fire1")&& Time.time>nextFire)
       {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }

    //Fixed Update is called before each fixed physic step
    void FixedUpdate()
    {
        //Associations des inputs pour le déplacement
        float f_moveHorizontal = Input.GetAxis("Horizontal");
        float f_moveVertical = Input.GetAxis("Vertical");

        //Réalisation du déplacement
        Vector3 v_movement = new Vector3(f_moveHorizontal, f_moveVertical, 0.0f);
        rb.velocity = v_movement*f_speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );

        rb.rotation = Quaternion.Euler(0.0f, GetComponent<Rigidbody>().velocity.x * -f_tilt, 0.0f);
    }
}
