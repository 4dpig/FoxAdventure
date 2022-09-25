using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

public class HeartUIController : MonoBehaviour
{
    public static HeartUIController instance;

    public Sprite full_heart;
    public Sprite half_heart;
    public Sprite empty_heart;
    
    public GameObject heart_prefab;

    private int heartNumber;
    private int indexOfLeftMostNonFullHeart;
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

        // 设置下标，当最右边的非满心坐标设为heartNumber时，表示玩家为满心
        indexOfLeftMostNonFullHeart = heartNumber;
        indexOfRightmostNonEmptyHeart = heartNumber - 1;
        
        // 给数组分配内存
        hearts = new Heart[heartNumber];

        // 动态生成每一个heart ui物体
        for (int i = 0; i < heartNumber; i++)
        {
            // 从prefab生成一个实例，并将其parent设为heartGroup
            GameObject heartObject = Instantiate(heart_prefab, this.gameObject.transform);
            
            /*
             * 如果像下面这样先创建实例再添加parent物体，实例的scale缩放似乎会发生莫名其妙的改变，可能是Unity的bug
            GameObject heartObject = Instantiate(heart_prefab);
            heartObject.transform.parent = this.gameObject.transform;
            
             * 不过改变了之后再手动改回来也行，但还是一步到位最好，直接在创建实例的时候就指定parent物体
            heartObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            */
            
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

    public void IncreaseHealth(int healAmount)
    {
        // 从最左边的非满心开始，向后加
        int i = indexOfLeftMostNonFullHeart;

        while (healAmount > 0 && i < heartNumber)
        {
            Heart heart = hearts[i];

            if (healAmount >= 2)
            {
                healAmount -= ( 2 - heart.currentHealthPoint);
                heart.currentHealthPoint = 2;
                heart.heartImage.sprite = full_heart;
                i++;
            }
            else
            {
                heart.currentHealthPoint += healAmount;
                healAmount = 0;

                if (heart.currentHealthPoint == 2)
                {
                    heart.heartImage.sprite = full_heart;
                    i++;
                }
                else
                {
                    heart.heartImage.sprite = half_heart;
                }
            }
        }
        
        // 更新 最左边的非满心的下标 为i
        indexOfLeftMostNonFullHeart = i;
        
        // 更新 最右边的非空心的下标
        if (i == heartNumber || hearts[i].currentHealthPoint == 0)
        {
            indexOfRightmostNonEmptyHeart = i - 1;
        }
        else
        {
            indexOfRightmostNonEmptyHeart = i;
        }
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
                damage = 0;
                
                if (heart.currentHealthPoint > 0)
                {
                    heart.heartImage.sprite = half_heart;
                }
                else
                {
                    heart.heartImage.sprite = empty_heart;
                    i--;
                }
            }
        }
        
        // 更新 最右边的非空心的下标 为i
        indexOfRightmostNonEmptyHeart = i;
        
        // 更新 最左边的非满心的下标
        if (i < 0 || hearts[i].currentHealthPoint == 2)
        {
            indexOfLeftMostNonFullHeart = i + 1;
        }
        else
        {
            indexOfLeftMostNonFullHeart = i;
        }
    }

    public void Reset()
    {
        // 重置为满心
        for (int i = 0; i < heartNumber; i++)
        {
            hearts[i].heartImage.sprite = full_heart;
            hearts[i].currentHealthPoint = 2;
        }
        
        // 重置下标
        indexOfLeftMostNonFullHeart = heartNumber;
        indexOfRightmostNonEmptyHeart = heartNumber - 1;
    }
}
