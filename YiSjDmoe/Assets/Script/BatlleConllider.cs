using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BatlleConllider : MonoBehaviour
{

    public Animator LunaAt;//luna战斗的动画控制器
    public Transform lunaTransform;//luna的位置获取
    public Transform monsterTransform;//敌人的位置获取
    private Vector3 monsterInitPos;// vector3数据类型，储存敌人初始位置
    private Vector3 lunaInitPos; //vector3数据类型，储存Luna初始位置

    public SpriteRenderer monsterSr; // 敌人图片挂载的组件
    public SpriteRenderer lunaSr;// 玩家挂载的组件
    public GameObject skillEffectGo; //攻击特效预制体
    public GameObject recoverHpEffectGo;//治疗特效预制体

    public AudioClip attackSound;
    public AudioClip LunaAttackSuond;

    public AudioClip mosterAttackSunod;

    public AudioClip skillSound;
    public AudioClip recoverSound;

    public AudioClip hitSuond;
    public AudioClip dieSuond;
    public AudioClip monsterDieSound;
    // Start is called before the first frame update
    private void Awake()
    {
        monsterInitPos = monsterTransform.localPosition;// vector3数据类型，储存敌人初始位置
        lunaInitPos = lunaTransform.localPosition;//vector3数据类型，储存Luna初始位置
    }
    
    public void OnEnable()//每次进入战斗场景的初始设置
    {
        monsterSr.DOFade(1, 0.1f);
        lunaSr.DOFade(1, 0.1f);

        lunaTransform.localPosition = lunaInitPos;

        monsterTransform.localPosition = monsterInitPos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Luna攻击方法
    /// </summary>
    public void LunaAttack()
    {
        //调用携程PerformAttackLogic方法
        StartCoroutine("PerformAttackLogic");
    }

    IEnumerator PerformAttackLogic()
    {
        UIManager.Instance.ShowOrHide(false); //ui单例模式，ui脚本里面调用ShowOrHide()方法
        LunaAt.SetBool("MoveState", true); //设置主角动画控制器状态机，“MoveState”的值为 true(让人物进入运动状态)
        LunaAt.SetFloat("MoveValue", -1); //设置主角动画控制器状态机，“MoveValue”的值为 -1(让人物向前运动状态)
        //让 lunaTransform 的位置 移动到这个 monsterInitPos + new Vector3(1.5F, 0, 0), 0.5f) 位置 用0.5秒的时间
        lunaTransform.DOLocalMove(monsterInitPos + new Vector3(1.5F, 0, 0), 0.5f).OnComplete(
            () => 
                {
                    GameManager.Instanace.PlaySound(attackSound);
                    GameManager.Instanace.PlaySound(LunaAttackSuond);
                    LunaAt.SetBool("MoveState", false);//设置主角动画控制器状态机，“MoveState”的值为false(现在主角移动到了敌人面前，现在停止移动了)
                    LunaAt.SetFloat("MoveValue", 0);
                    LunaAt.CrossFade("Attack",0);//让主角动画状态机播放 “Attack”动画，在第0 个图层

                    //敌人图片组件 ，透明度设置30%，时间0.2秒，然后调用 JudgeMonsterHp 方法(敌人血量显示与判定)
                    monsterSr.DOFade(0.3f, 0.2f).OnComplete(()=> { JudgeMonsterHp(-20); }) ;
                }
            );
        yield return new WaitForSeconds(1.167f);//等待移动（0.5）攻击（0.667）完成后
        LunaAt.SetBool("MoveState", true);//人物恢复移动状态
        LunaAt.SetFloat("MoveValue", 1);//设置向后移动状态方向
        //主角位置（lunaTransform）移动到 初始位置（lunaInitPos）     //让人物恢复静止待机状态
        lunaTransform.DOLocalMove(lunaInitPos, 0.5f).OnComplete(() => { LunaAt.SetBool("MoveState", false);  });
       //0.5秒后调用敌人攻击携程方法
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MonsterAttack());
    }


    //敌人攻击行为方法
    IEnumerator MonsterAttack()
    {
        //敌人移动到主角面前
        monsterTransform.DOLocalMove(lunaInitPos + new Vector3(-1.5f, 0, 0), 0.5f).OnComplete(
            () => {
                GameManager.Instanace.PlaySound(mosterAttackSunod);
                LunaAt.CrossFade("Hit", 0);
                GameManager.Instanace.PlaySound(hitSuond);
                lunaSr.DOFade(0.3f, 0.2f).OnComplete(()=> { lunaSr.DOFade(1, 0.2f);  });
                JudgeLunaHp(-20);
                    }
            );

        yield return new WaitForSeconds(0.7f);
        //敌人位置复原
        monsterTransform.DOLocalMove(monsterInitPos, 0.5f).OnComplete(()=> {UIManager.Instance.ShowOrHide(true); });
    }
    /// <summary>
    /// Luna防御方法
    /// </summary>
    public void LunaDefend()
    {
        StartCoroutine(PerformDefendLogic());
    }
    //防御携程方法
    IEnumerator PerformDefendLogic()
    {
        UIManager.Instance.ShowOrHide(false); //ui单例模式，ui脚本里面调用ShowOrHide()方法
        LunaAt.SetBool("Defend",true);
        monsterTransform.DOLocalMove(lunaInitPos + new Vector3(-1.5f, 0, 0), 0.5f).OnComplete(
          () => {
              GameManager.Instanace.PlaySound(mosterAttackSunod);
              lunaSr.DOFade(0.3f, 0.2f).OnComplete(() => { lunaSr.DOFade(1, 0.2f); });
          lunaTransform.DOLocalMove(lunaInitPos + new Vector3(1f, 0, 0), 0.2f).OnComplete(() => { lunaTransform.DOLocalMove(lunaInitPos, 0.2f); });
              JudgeLunaHp(-10);
          }
          );

        yield return new WaitForSeconds(0.9f);
        //敌人位置复原
        monsterTransform.DOLocalMove(monsterInitPos, 0.5f).OnComplete(
            () => { 
                UIManager.Instance.ShowOrHide(true);
                LunaAt.SetBool("Defend", false);
                    }
            );
    }


    /// <summary>
    /// luna使用技能
    /// </summary>
    /// <param name="value"></param>
    public void LunaSkill()
    {
        if (!GameManager.Instanace.CanUsePlayerMP(30))
        {
            return;
        }
        StartCoroutine(PerformSkillLogic());
    }
    IEnumerator PerformSkillLogic()
    {
        UIManager.Instance.ShowOrHide(false);
        LunaAt.CrossFade("Skill", 0);
        GameManager.Instanace.AddOrDecreaseMP(-30);
        yield return new WaitForSeconds(0.35f);
     GameObject go=   Instantiate(skillEffectGo, monsterTransform);
        go.transform.localPosition = Vector3.zero;
        GameManager.Instanace.PlaySound(LunaAttackSuond);
        GameManager.Instanace.PlaySound(skillSound);

        yield return new WaitForSeconds(0.4f);
        monsterSr.DOFade(0.3f, 0.2f).OnComplete(
            () =>
            {
                JudgeMonsterHp(-30);
                monsterSr.DOFade(1f, 0.2f);
            }
            );
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MonsterAttack());
    }

    //回血技能
    public void LunaRecoverHp()
    {
        if (!GameManager.Instanace.CanUsePlayerMP(50))
        {
            return;
        }
        StartCoroutine(PerformRecoverHPLogic());
      
    }
    IEnumerator PerformRecoverHPLogic()
    {
        UIManager.Instance.ShowOrHide(false);
        LunaAt.CrossFade("RecoverHP",0);
        GameManager.Instanace.AddOrDecreaseMP(-50);
        GameManager.Instanace.PlaySound(LunaAttackSuond);
        GameManager.Instanace.PlaySound(recoverSound);
        // yield return new WaitForSeconds(0.1f);
        GameObject to = Instantiate(recoverHpEffectGo,lunaTransform);
        to.transform.localPosition = Vector3.zero;
      
        GameManager.Instanace.AddOrDecreaseHP(40);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MonsterAttack());
    }
    //敌人血量显示与判定
    private void JudgeMonsterHp(int value)
    {
        //敌人图片 透明度 设置为100%，时间0.2秒(这里就是还原透明度的作用)
      
        if (GameManager.Instanace.AddOrDecreaseMonsterHP(value)<=0)
        {
            GameManager.Instanace.PlaySound(monsterDieSound);
            monsterSr.DOFade(0, 0.4f).OnComplete(
                () =>
                {
                    GameManager.Instanace.EnterOrExitBattle(false,1);
                    UIManager.Instance.ShowOrHide(false);

                }
                    );
        }
        else
        {
            monsterSr.DOFade(1, 0.2f);
        }
    }
    //玩家血量改变与判定
    private void JudgeLunaHp(int value)
    {
        GameManager.Instanace.AddOrDecreaseHP(value);
        if (GameManager.Instanace.currentHealth<=0)
        {
            LunaAt.CrossFade("Die",0);
            GameManager.Instanace.PlaySound(dieSuond);
            lunaSr.DOFade(0, 0.8f).OnComplete(
                ()=>
                {
                    GameManager.Instanace.EnterOrExitBattle(false);
                    UIManager.Instance.ShowOrHide(false);

                }
                    );
        }
    }

    //逃跑方法
    public  void LunaEscape()
    {
        UIManager.Instance.ShowOrHide(false);
        LunaAt.SetBool("MoveState", true);//人物恢复移动状态
        LunaAt.SetFloat("MoveValue", 1);
        lunaTransform.DOLocalMove(lunaInitPos + new Vector3(5, 0, 0), 0.5f).OnComplete(
            () =>
            {
                LunaAt.SetBool("MoveState", false);//人物恢复移动状态
                LunaAt.SetFloat("MoveValue", 0);
                GameManager.Instanace.EnterOrExitBattle(false);

            }
            );
    }
}
