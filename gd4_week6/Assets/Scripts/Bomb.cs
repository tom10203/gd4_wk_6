using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Target target;
    UIManager uiManager;
    bool interact = true;

    public ParticleSystem explosion;

    public GameObject bombMesh;
    public GameObject bombWick;
    public ParticleSystem smoke;
    public ParticleSystem sparks;
    void Start()
    {
        target = GetComponent<Target>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target.isHit && interact)
        {
            explosion.Play();
            print($"bomb hit");
            uiManager.UpdateLives(-1);
            interact = false;

            bombMesh.SetActive(false);
            bombWick.SetActive(false);
            smoke.Stop();
            sparks.Stop();
            sparks.Clear();

        }
    }
}
