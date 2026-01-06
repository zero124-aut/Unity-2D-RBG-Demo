using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaController : MonoBehaviour
{
    
    Rigidbody2D rb;

  public   float seep ;
   
    private Animator animator;
    private Vector2 lookDirection = new Vector2(0,0);
    private float moveScale;
    public Vector2 move;

    private float lastHorizontal;
    private float lastVertical;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
       // currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        lastHorizontal = 0;
        lastVertical = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instanace.entserBattle)
        {
            return;
        }
        if (!GameManager.Instanace.canControlLuna)
        {
            return;
        }

         float h =   Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
         move = new Vector2(h, v);
       

        if (!Mathf.Approximately(move.x,0)||!Mathf.Approximately(move.y, 0))
        {
            lookDirection.Set(move.x, move.y);
            //.Normalize()作用一句话：把方向保留、把长度变成 1。
            lookDirection.Normalize();
            lastHorizontal = h;
            lastVertical = v;
        }
        moveScale = move.magnitude;
        if (move.magnitude>0)
        {

       

            if (Input.GetKey(KeyCode.LeftShift))
            {
            moveScale = 1;
                seep = 1;
            }
            else
            {
            moveScale = 2;
                seep = 2.5f;
            }

        }


           animator.SetFloat("Horizontal", lastHorizontal);
           animator.SetFloat("Vertical", lastVertical);
        // move.magnitude 代表 vector2 move 向量的 模长 （向量的模长 高中数学知识）
        animator.SetFloat("MoveValue", moveScale);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Talk();
        }
        
      

    }
    private void FixedUpdate()
    {
        if (GameManager.Instanace.entserBattle)
        {
            return;
        }
        rb.velocity = new Vector2(move.x,move.y)*seep;
    }


    

    //攀爬方法
    public void Climb(bool start)
    {
        animator.SetBool("Climb", start);
    }
    public void Jump(bool start)
    {
        animator.SetBool("Jump", start);
        rb.simulated = !start;
    }
   public void Talk()
    {
        Collider2D collider = Physics2D.OverlapCircle(rb.position, 0.5f,LayerMask.GetMask("NPC"));
        if (collider != null)
        {
            if (collider != null && collider.name == "Nala")
            {
                GameManager.Instanace.canControlLuna = false;
                collider.GetComponent<NpcDlalog>().DisplayDialog();
            }
            else if (collider.name == "Dog"&&!GameManager.Instanace.hasPetTheDog&&GameManager.Instanace.dialogInfoIndex==2)
            {
                PetTheDog();
                GameManager.Instanace.canControlLuna = false;
                collider.GetComponent<Dog>().BeHapp();
                

            }
        }
    }
    public  void PetTheDog()
    {
        animator.CrossFade("PteTheDog", 0);
        //transform.position = new Vector3(-1.29f,-8.35f,0);

    }
}
