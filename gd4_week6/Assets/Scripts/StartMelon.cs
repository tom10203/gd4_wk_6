using UnityEngine;

public class StartMelon : MonoBehaviour
{
    UIManager manager;
    Target target;
    private void Start()
    {
        target = GetComponent<Target>();    
        manager = FindFirstObjectByType<UIManager>();
    }

    private void Update()
    {
        if (target.isHit)
        {
            manager.StartGame();
        }
    }
}
