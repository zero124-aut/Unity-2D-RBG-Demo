using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //轴向控制
    public bool vertical;
    public float speed = 5;
    private Rigidbody2D rb;
    //方向控制
    private int direction = 1;
    //方向改变的时间间隔
    public float changeTime = 5;
    //计时器
    private float timer;
    //动画控制器
    private Animator animator;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instanace.entserBattle)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer<0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instanace.entserBattle)
        {
            return;
        }
        Vector3 pos = rb.position;
        if (vertical)
        {
            animator.SetFloat("lookx", 0);
            animator.SetFloat("looky", direction);
            pos.y = pos.y + speed * direction * Time.fixedDeltaTime;
        }
        else
        {
            animator.SetFloat("looky", 0);
            animator.SetFloat("lookx", direction);
            pos.x = pos.x + speed * direction * Time.fixedDeltaTime;
        }
        rb.MovePosition(pos);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instanace.EnterOrExitBattle();
            GameManager.Instanace.SetMonster(gameObject);
            UIManager.Instance.ShowOrHide(true);
        }
    }

}
