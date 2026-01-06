using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpArea : MonoBehaviour
{
    public Transform jumpPointA;
    public Transform jumpPointB;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LunaController lunaController = collision.GetComponent<LunaController>();
            collision.GetComponent<LunaController>().Jump(true);
         float disA =   Vector3.Distance(lunaController.transform.position, jumpPointA.position);
         float disB =   Vector3.Distance(lunaController.transform.position, jumpPointB.position);

            Transform targetTrans;
            if (disA>disB)
            {
                //距离A的距离大于B的距离，so从B跳到A
                targetTrans = jumpPointA;

            }
            else
            {
                targetTrans = jumpPointB;
            }
            collision.GetComponent<LunaController>().transform.DOMove(targetTrans.position, 0.5f).OnComplete(()=> { EndJump(lunaController); });
        }
    }

    private void EndJump(LunaController lunaController)
    {
        lunaController.Jump(false);
    }
}
