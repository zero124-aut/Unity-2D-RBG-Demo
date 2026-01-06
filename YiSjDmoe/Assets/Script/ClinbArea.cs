using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClinbArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<LunaController>().Climb(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<LunaController>().Climb(false);
        }
    }
}
