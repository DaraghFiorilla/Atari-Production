using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private ParticleSystem explosionParticles;
    private MeshRenderer meshRenderer;
    private float timer;
    private bool started;

    private void Awake()
    {
        started = false;
        explosionParticles= GetComponent<ParticleSystem>();
        meshRenderer = GetComponent<MeshRenderer>();
        timer = explosionParticles.main.duration;
        Debug.Log(explosionParticles.main.duration);
        explosionParticles.Pause();
    }

    private void Update()
    {
        if (started)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("bomb hit ground");
            meshRenderer.enabled = false;
            explosionParticles.Play();
            started = true;
        }
    }
}
