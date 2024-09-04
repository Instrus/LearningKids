using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for checks/x feedback during gameplay

public class Marks : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    Color color;
    
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        StartCoroutine(Evaporate());
        StartCoroutine(Kill());
    }

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);
    }

    private IEnumerator Evaporate()
    {
        while (color.a > 0)
        {
            color.a -= 0.1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    private IEnumerator Kill()
    {

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

}
