using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundry
{
    public float xMin, xMax, zMin, zMax;


}


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundry boundry;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);    
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movment = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movment * speed;

        GetComponent<Rigidbody>().position = new Vector3( 
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundry.xMin, boundry.xMax),
            0.0f, 
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundry.zMin, boundry.zMax)
        );
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f,0.0f,GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}
