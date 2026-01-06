using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDlalog : MonoBehaviour
{

    private List<DialogInfo[]> dialogInfoList;
    private int contentIndex;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        dialogInfoList = new List<DialogInfo[]>()
        {
         
            new DialogInfo[]
            {
                new DialogInfo(){name="Luna",content="hello,我是Luna，你可以使用wsad,对我进行上下左右移动的控制，战斗中需要坚定点击按钮继续相应的行为"}
            },

          new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="好久不见了，小猫咪（*v*）,Luna~"},
                new DialogInfo(){name="Luna",content="好久不见，Nala，你还是这么有活力，哈哈"},
                new DialogInfo(){name="Nala",content="我的狗一直在叫，但是我这会忙不过来，你能帮我安抚一下它吗？"},
                new DialogInfo(){name="Luna",content="啊？"},
                new DialogInfo(){name="Nala",content="(>-<) (>-<) 摸摸它就行,摸摸说呦西呦西，真是个好孩子呐"},
                new DialogInfo(){name="Nala",content="别看它叫的这么凶，其实它就是想吸引别人注意"},
                new DialogInfo(){name="Luna",content="可是。。。。。。"},
                new DialogInfo(){name="Luna",content="我是猫女郎啊"},
                new DialogInfo(){name="Nala",content="安心啦，不会咬你的，去吧去吧~"},
            },

            new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="它还在叫呢"},
            },
              new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="感谢你，Luna，你还是那么可靠！"},
                new DialogInfo(){name="Nala",content="我想请你帮个忙好吗"},
                new DialogInfo(){name="Nala",content="说起来这事怪我。。。。。。"},
                new DialogInfo(){name="Nala",content="今天我睡过头了，出门比较匆忙"},
                new DialogInfo(){name="Nala",content="然后装蜡烛的口袋没封好"},
                new DialogInfo(){name="Nala",content="结果就。。。蜡烛基本上丢完了"},
                new DialogInfo(){name="Luna",content="你还是老样子，哈哈。。"},
                new DialogInfo(){name="Nala",content="所以，所以喽，你帮帮忙，帮我把蜡烛找回来"},
                new DialogInfo(){name="Nala",content="如果你帮我把五个蜡烛全部找回来，我就送你一把神器"},
                new DialogInfo(){name="Luna",content="神器？"},
                new DialogInfo(){name="Nala",content="我感觉很适合你，加油呐~"},

            },
               new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="你还没有帮我收集到所有蜡烛，宝~"},
            },
                new DialogInfo[]
            {

                new DialogInfo(){name="Nala",content="可靠啊，你竟然一个不差的全收集回来了"},
                new DialogInfo(){name="Luna",content="你知道多累吗？"},
                new DialogInfo(){name="Luna",content="你到处跑，真的很难收集"},
                new DialogInfo(){name="Nala",content="辛苦啦辛苦啦"},
                new DialogInfo(){name="Nala",content="这是给你的奖励"},
                new DialogInfo(){name="Nala",content="蓝纹火锤，传说中的神器"},
                new DialogInfo(){name="Luna",content="--获得蓝纹火锤--（遇到敌人可触发战斗）"},
                new DialogInfo(){name="Luna",content="哇，谢谢你，Thanks"},
                new DialogInfo(){name="Nala",content="嘿嘿，咱们的关系不用客气"},
                new DialogInfo(){name="Nala",content="正好，最近山里出行了一堆怪物，你也算为民除害，帮忙清理4只怪物"},
                new DialogInfo(){name="Luna",content="啊？"},
                new DialogInfo(){name="Luna",content="这才是你的真实目的吧？！"},
                new DialogInfo(){name="Nala",content="拜托啦，否则真的很不方便我卖东西"},
                new DialogInfo(){name="Luna",content="无语中。。。。。。"},
                new DialogInfo(){name="Nala",content="求你了。啵啵~"},
                new DialogInfo(){name="Luna",content="哎，行吧，谁让你大呢"},
                new DialogInfo(){name="Nala",content="嘻嘻，那辛苦你了，咋俩天下第一好"},
            },
                 new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="宝，你还没有清理干净呢，我这样不方便嘛"},
            },
                   new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="真棒，luna,周围的居民都会感谢你的，有机会来我家喝一杯"},
                  
                new DialogInfo(){name="Luna",content="我觉得可行，哈哈~"},
            },
                       new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="改天见喽"},
            },



        };
        GameManager.Instanace.dialogInfoIndex = 0;//第几段
        contentIndex = 1; //第几句话
    }


    //显示对话内容
    public void DisplayDialog()
    {
        if (GameManager.Instanace.dialogInfoIndex > 8)
        {
            return;
        }



        if (contentIndex >= dialogInfoList[GameManager.Instanace.dialogInfoIndex].Length)
        {
             if (GameManager.Instanace.dialogInfoIndex == 2 && !GameManager.Instanace.hasPetTheDog)
            {

            }
            else if (GameManager.Instanace.dialogInfoIndex == 4 && GameManager.Instanace.candleNum < 5)
            {

            }
            else if (GameManager.Instanace.dialogInfoIndex == 6 && GameManager.Instanace.killlNum < 4)
            {

            }
            
            else
            {
                GameManager.Instanace.dialogInfoIndex++;
            }
            if (GameManager.Instanace.dialogInfoIndex==6)
            {
                GameManager.Instanace.ShowMonster();
            }
            //当前这段对话结束了，可以控制人物移动了
            contentIndex = 0;
            UIManager.Instance.ShowDialog();
            GameManager.Instanace.canControlLuna = true;

        }
        else
        {
            DialogInfo dialogInfo = dialogInfoList[GameManager.Instanace.dialogInfoIndex][contentIndex];
            UIManager.Instance.ShowDialog(dialogInfo.content, dialogInfo.name);
            contentIndex++;
            animator.SetTrigger("Talk");
        }
    }
    public void SetContentIndex()
    {
        contentIndex = dialogInfoList[GameManager.Instanace.dialogInfoIndex].Length;
    }
}

/// <summary>
/// 类外结构体 对话信息；
/// </summary>
public struct DialogInfo
{
    public string name;
    public string content;
}
