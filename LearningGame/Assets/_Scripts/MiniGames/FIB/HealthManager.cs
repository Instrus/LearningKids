using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public enum Type
    {
        Player,
        NPC
    }

    public Type healthType;

    [SerializeField] public int maxHealth = 3;// how many hearts / health the player will have at the start of games (potentially subject to change through save system)
    [SerializeField] public int health; // current user health

    [SerializeField] private GameObject heartPrefab; // instantaite heart
    public List<GameObject> hearts = new List<GameObject>(); // to manipulate each heart

    [SerializeField] GameObject body;
    [SerializeField] private List<SpriteRenderer> bodyComponents; // all components of body will flash red on hit
    [SerializeField] private AudioClip hitSound;

    private void Start()
    {
        if (healthType == Type.NPC) {
            body = GameObject.Find("NPC");
            bodyComponents.Add(body.transform.GetChild(0).GetComponent<SpriteRenderer>());
            bodyComponents.Add(body.transform.GetChild(1).GetComponent<SpriteRenderer>());
            bodyComponents.Add(body.transform.GetChild(2).GetComponent<SpriteRenderer>());
        }
        else
        {
            body = GameObject.Find("Player");
            bodyComponents.Add(body.transform.GetChild(0).GetComponent<SpriteRenderer>());
            bodyComponents.Add(body.transform.GetChild(1).GetComponent<SpriteRenderer>());
            bodyComponents.Add(body.transform.GetChild(2).GetComponent<SpriteRenderer>());
        }
        

    }

    public void InstantiateHearts(int count)
    {
        maxHealth = count;

        // instantiate hearts 
        for (int i = 0; i < count; i++)
        {
            hearts.Add(Instantiate(heartPrefab, transform));
            health += 1;
        }
    }

    public void TakeDamage()
    {
        if (health < 1)
            return;

        // visual/auditory feedback
        StartCoroutine( FlashRed() );
        if (hitSound != null)
            AudioManager.instance.PlayClip(hitSound);

        //hearts[health - 1].GetComponent<Heart>().EmptyHeart();
        hearts[health - 1].GetComponent<Heart>().DestroyHeart();
        
        //Destroy(hearts[health - 1]);

        health -= 1;

        // depending on who loses, send over arguments to PopupWindow?

        // if player loses
        if (healthType == Type.Player && health < 1)
        {
            FIB_E FIB = transform.parent.GetComponent<FIB_E>();
            FIB.PopupWindow();
        } 
        // NPC loses
        else if (healthType == Type.NPC && health < 1)
        {
            FIB_E FIB = transform.parent.GetComponent<FIB_E>();
            FIB.PopupWindow();
        }


    }

    // called by EndGame in FIB_E
    public void ClearHeartsList()
    {
        foreach (GameObject heart in hearts)
            Destroy(heart);
        hearts.Clear();

        bodyComponents.Clear();

        health = 0;
    }

    public IEnumerator FlashRed()
    {
        foreach(SpriteRenderer component in bodyComponents)
            component.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        foreach(SpriteRenderer component in bodyComponents)
            component.color = Color.white;
    }

    // potential restore health function
}
