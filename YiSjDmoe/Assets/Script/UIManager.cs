using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public Image hpMaskImage;
    public Image mpMaskImage;
    private float originalSize;//血条原始宽度

    public GameObject battlePanelGo;

    public GameObject TalkPanlGo;

    public Sprite[] characterSprtes;

    public Image CharacterImage;

    public Text NameText;
    public Text ContentText;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        originalSize = hpMaskImage.rectTransform.rect.width;
       
    }
    /// <summary>
    /// 血条填充显示
    /// </summary>
    /// <param name="fillPercent">填充百分比</param>
    // Update is called once per frame
   public void setHPvalue(float fillPercent)
    {
        hpMaskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fillPercent*originalSize);
    }


    /// <summary>
    /// 蓝条填充显示
    /// </summary>
    /// <param name="fillPercent">填充百分比</param>
    // Update is called once per frame
    public void setMPvalue(float fillPercent)
    {
        mpMaskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fillPercent * originalSize);
    }

    public void ShowOrHide(bool show)
    {
        battlePanelGo.SetActive(show);
    }
    /// <summary>
    /// 显示对话内容（包含人物的切换，名字切换，对话内容的更换，）
    /// </summary>
    /// <param name="content"></param>
    /// <param name="name"></param>
    public void ShowDialog(string content =null,string name = null)
    {
        if (content == null)
        {
            TalkPanlGo.SetActive(false);
        }
        else
        {
            TalkPanlGo.SetActive(true);
            if (name!=null)
            {
                if (name == "Luna")
                {
                    CharacterImage.sprite = characterSprtes[0];
                }
                else
                {
                    CharacterImage.sprite = characterSprtes[1];
                }
                CharacterImage.SetNativeSize();
            }
            ContentText.text = content;
            NameText.text = name;
            
        }
    }
}

