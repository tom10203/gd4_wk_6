using UnityEngine;

public class Combos : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Material material;
    float alphaValue = 2;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        alphaValue -= Time.deltaTime;
        if (alphaValue <= 1)
        {
            spriteRenderer.material.SetFloat("_AlphaScaler", alphaValue);
        }
        if (alphaValue <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
        
    }


}
