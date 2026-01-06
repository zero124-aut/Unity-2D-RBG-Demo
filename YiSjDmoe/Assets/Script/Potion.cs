using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject effectGo;
    public AudioClip pickSuond;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        

      LunaController  lunaController =  collision.GetComponent<LunaController>();

        if (lunaController != null)
        {

            if (GameManager.Instanace.currentHealth< GameManager.Instanace.maxHealth)
            {
                GameManager.Instanace.AddOrDecreaseHP(40);
                Instantiate(effectGo, transform.position, Quaternion.identity);
                GameManager.Instanace.PlaySound(pickSuond);
                Destroy(gameObject);
            }

        }

        
    }

}
