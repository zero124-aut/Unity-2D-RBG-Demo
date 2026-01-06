using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{

    public GameObject effectGo;
    public AudioClip pickClip;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instanace.candleNum++;
        Instantiate(effectGo, transform.position, Quaternion.identity);
        if (GameManager.Instanace.candleNum>=5)
        {
            GameManager.Instanace.SetContentIndex();
        }
        GameManager.Instanace.PlaySound(pickClip);
        Destroy(gameObject);
    }
}
