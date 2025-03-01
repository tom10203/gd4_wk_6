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

    [SerializeField] AudioSource bombExplosion;
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
            if (explosion != null)
            {
                explosion.Play();
            }
            
            bombExplosion.Play();
            print($"bomb hit");
            uiManager.UpdateLives(-1);
            interact = false;

            bombMesh.SetActive(false);
            bombWick.SetActive(false);
            if (smoke != null)
            {
                smoke.Stop();
            }
            if (sparks != null)
            {
                sparks.Stop();
                sparks.Clear();
            }
            
            uiManager.GameOver();

        }
    }
}
