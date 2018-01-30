using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject childBall;
    public bool canExplode;
    public bool canMultiply;

    private void OnCollisionEnter(Collision collision)
    {
        if (!canExplode)
            return;
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(explosionPrefab, pos, rot);

        if (canMultiply)
        {
            Instantiate(childBall, pos, rot);
        }
        Destroy(gameObject);
    }



    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }
}
