using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class FireBallBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject childBall;
    public bool canExplode;
    public bool canMultiply;
    public int numberOfSpawns;
    public int expForce;
    public float spawnXZRandRange;
    public float spawnYRandRange;

    private void OnCollisionEnter(Collision collision)
    {
        if (!canExplode)
            return;

        if (collision.collider.CompareTag("FireBall"))
            return;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        GameObject explosion;
        explosionPrefab.GetComponent<ExplosionPhysicsForce>().explosionForce = expForce;
        explosion = Instantiate(explosionPrefab, pos, rot);
        

        if (canMultiply)
        {
            for (int i = 0; i < numberOfSpawns; i++) {
                Vector3 position = new Vector3(Random.Range(-spawnXZRandRange, spawnXZRandRange), Random.Range(-0.3f, spawnYRandRange), Random.Range(-spawnXZRandRange, spawnXZRandRange));
                GameObject tuhottava = Instantiate(childBall, pos + position, rot);

                Destroy(tuhottava, 8f);
            }
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
