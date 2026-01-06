using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager Instanace;
    public int maxHealth ; //最大生命值
    public float currentHealth; //当前生命值
    public int lunaMP=100;//最大蓝量
    public float lunaCurrentMp; //当前蓝量


    public int MonsterHP;

    public int dialogInfoIndex;
    public bool canControlLuna;

    public bool hasPetTheDog;
    public int candleNum;
    public int killlNum;

    public GameObject Monster;
    public GameObject battleGo;//战斗场景游戏物体
    public NpcDlalog npc;

    public bool entserBattle;

    public GameObject battleMonsterGo;


    public AudioSource audioSource;

    public AudioClip noramlClip;
    public AudioClip battleClip;
    // Start is called before the first frame update
    private void Awake()
    {
        Instanace = this;

        currentHealth = 100f;
        lunaCurrentMp = 100f;
        maxHealth = 100;
        lunaMP = 100;

        MonsterHP = 50;
    }
    private void Update()
    {
        if (!entserBattle)
        {
            if (currentHealth<=100)
            {
                AddOrDecreaseMP(Time.deltaTime);
            }
            if (lunaCurrentMp<=100)
            {
                AddOrDecreaseHP(Time.deltaTime);
            }
        }
    }
    //触碰血瓶回血
    /*  public void ChangeHeath(int amout)
      {

          //Mathf.Clamp 限制 currentHealth加上amout 之后 ,csurrentHealth 的范围是 0-maxHealth(5).的意思
          currentHealth = Mathf.Clamp(currentHealth + amout, 0, maxHealth);
      }*/
    public void EnterOrExitBattle(bool enter = true,int addKillNum =0)
    {
        UIManager.Instance.ShowOrHide(enter);
        battleGo.SetActive(enter);
        if (!enter)
        {
            killlNum += addKillNum;
            if (addKillNum>0)
            {
                DestoryMonster();
            }
            MonsterHP = 50;
            PlayMusic(noramlClip);
            if (currentHealth<=0)
            {
                currentHealth = 100;
                lunaCurrentMp = 0;
                battleMonsterGo.transform.position += new Vector3(0, 2, 0);
            }
        }
        else
        {
            PlayMusic(battleClip);
        }
        if (GameManager.Instanace.killlNum >= 5)
        {
            GameManager.Instanace.SetContentIndex();
        }

        entserBattle = enter;
    }
    public void DestoryMonster()
    {
        Destroy(battleMonsterGo);
    }
    public void SetMonster(GameObject go)
    {
        battleMonsterGo = go;
    }
    /// <summary>
    /// luna血量改变方法
    /// </summary>
    public void AddOrDecreaseHP(float value)
    {
        currentHealth += value;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        UIManager.Instance.setHPvalue(currentHealth / maxHealth);
    }
    /// <summary>
    /// LUNA蓝量改变
    /// </summary>
    /// <param name="value"></param>
    public void AddOrDecreaseMP(float value)
    {
        lunaCurrentMp += value;
        if (lunaCurrentMp >= lunaMP)
        {
            lunaCurrentMp = lunaMP;
        }
         if (lunaCurrentMp <= 0)
        {
            lunaCurrentMp = 0;
        }
        UIManager.Instance.setMPvalue(lunaCurrentMp /lunaMP);
    }




    public bool CanUsePlayerMP(int value)
    {
        return lunaCurrentMp >= value;
    }


    //敌人血量改变
    public int  AddOrDecreaseMonsterHP(int value)
    {
        MonsterHP += value;

        return MonsterHP;
    }

    //显示敌人
    public void ShowMonster()
    {
        if (!Monster.activeSelf)
        {
            Monster.SetActive(true);
        }
    }
    //任务完成索引
   public void SetContentIndex()
    {
      npc.SetContentIndex();
    }


    public void PlayMusic(AudioClip audioClip)
    {
        if (audioSource.clip!=audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    //播放音效
    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
