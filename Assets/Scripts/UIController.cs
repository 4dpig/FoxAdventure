using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart
{
    public Image heartImage;
    public int currentHealthPoint;

    public Heart(Image _heartImage, int _currentHealthPoint)
    {
        heartImage = _heartImage;
        currentHealthPoint = _currentHealthPoint;
    }
}

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Sprite full_heart;
    public Sprite half_heart;
    public Sprite empty_heart;
    
    public GameObject heart_prefab;

    private int heartNumber;
    private int indexOfRightmostNonEmptyHeart;
    private Heart[] hearts;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 从healthController处获取heart数
        heartNumber = PlayerHealthController.instance.heartNumber;

        // 给数组分配内存
        hearts = new Heart[heartNumber];
        indexOfRightmostNonEmptyHeart = heartNumber - 1;
        
        // 动态生成每一个heart ui物体
        for (int i = 0; i < heartNumber; i++)
        {
            // 从prefab生成一个实例，并将其添加为heartPanel的子物体
            GameObject heartObject = Instantiate(heart_prefab);
            heartObject.transform.parent = this.gameObject.transform;
            
            /*
             * 给每一个heart分配内存（因为Heart是引用类型，所以创建Heart数组后，其中每一个heart都默认是空引用）
             * 一颗满心 = 2个health point
             * 保存对每个heart object的image组件的引用，方便之后更换sprite图像
            */
            hearts[i] = new Heart(heartObject.GetComponent<Image>(), 2);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth(int damage)
    {
        // 从最右边的非空心开始，向前减
        int i = indexOfRightmostNonEmptyHeart;
        while (damage > 0 && i >= 0)
        {
            Heart heart = hearts[i];
            
            if (damage >= 2)
            {
                damage -= heart.currentHealthPoint;
                heart.currentHealthPoint = 0;
                heart.heartImage.sprite = empty_heart;
                i--;
            }
            else
            {
                heart.currentHealthPoint -= damage;
                if (heart.currentHealthPoint > 0)
                {
                    heart.heartImage.sprite = half_heart;
                }
                else
                {
                    heart.heartImage.sprite = empty_heart;
                    i--;
                }

                damage = 0;
            }
        }
        
        // 更新 最右边的非空心的下标 为i
        indexOfRightmostNonEmptyHeart = i;
    }
}
