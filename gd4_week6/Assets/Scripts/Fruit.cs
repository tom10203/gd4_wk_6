using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    UIManager uiManager;
    Target target;
    public GameObject leftHalf;
    public GameObject rightHalf;
    public GameObject wholeFruit;
    SpawnManager spawnManager;
    bool interact = true;

    public ParticleSystem ps1;
    public ParticleSystem ps2;

    public bool isStart = false;
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        if (!isStart )
        {
            spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        }
        
        target = GetComponent<Target>();

    }

    private void Update()
    {
        if (interact)
        {
            if (target.isHit)
            {
                ps1.Play();
                ps2.Play();

                if (!isStart)
                {
                    spawnManager.currentFruit--;
                    uiManager.UpdateScore(1);
                }

                
                leftHalf.SetActive(true);
                rightHalf.SetActive(true);
                wholeFruit.SetActive(false);

                interact = false;
            }

            else if (target.isDestroyed)
            {
                spawnManager.currentFruit--;
                interact = false;
                if (!isStart)
                {
                    if (Time.time > spawnManager.reduceLivesTimer)
                    {
                        spawnManager.reduceLives = true;
                    }
                }
            }
        }
    }


}
