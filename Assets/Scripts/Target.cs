using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    public ParticleSystem explosionParticle;

    private float minSpeed = 14f;
    private float maxSpeed = 18f;
    private float torque = 10f;
    private float xRange = 4.5f;
    private float ySpawnPos = -6;

    private GameManager gameManager;
    public int pointValue = 5;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(Vector3.up * RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float RandomForce()
    {
        float randomForce = Random.Range(minSpeed, maxSpeed);
        return randomForce;
    }

    private Vector3 RandomTorque()
    {
        Vector3 randomTorque = new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque));
        return randomTorque;
    }

    private Vector3 RandomSpawnPos()
    {
        float randomPositionX = Random.Range(-xRange, xRange);
        Vector3 randomSpawnPos = new Vector3(randomPositionX, ySpawnPos, transform.position.z);
        return randomSpawnPos;
    }

   /*private void OnMouseDown()
    {
        if(!gameManager.isGameover && !gameManager.isPaused)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);

            if (gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateLife(-1);
            }
        }
    }*/

    public void DestroyTarget()
    {
        if (!gameManager.isGameover && !gameManager.isPaused)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);

            if (gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateLife(-1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLife(-1);
        }
    }
}
