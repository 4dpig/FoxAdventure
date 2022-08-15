using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEffect : MonoBehaviour
{
    public float moveUpSpeed;
    public float moveDownSpeed;
    private bool isFinished;
    private bool isPlaying;

    public Transform player;
    public SpriteRenderer sr;
    public Camera camera;

    private float topY, bottomY;
    private bool reachTop, reachBottom;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (!reachTop)
            {
                this.transform.Translate(0f, moveUpSpeed * Time.deltaTime, 0f);
                if (this.transform.position.y >= topY)
                {
                    reachTop = true;
                }
            }
            else if (!reachBottom)
            {
                this.transform.Translate(0f, -moveDownSpeed * Time.deltaTime, 0f);
                if (this.transform.position.y <= bottomY)
                {
                    reachBottom = true;
                }
            }
            else
            {
                isFinished = true;
                isPlaying = false;
            }
        }
    }

    public void StartDeathEffect(bool dieOutsideMap)
    {
        // 位置移动到Player
        this.transform.position = player.transform.position;
        /*
         * 计算死亡效果的Y坐标的移动范围
         * 顶部Y坐标 = 屏幕顶部Y坐标 + player高度的一半
         * 底部Y坐标 = 屏幕底部Y坐标 - player高度的一半
         */
        if (dieOutsideMap)
        {
            topY = camera.transform.position.y - camera.orthographicSize + sr.bounds.size.y;
        }
        else
        {
            topY = player.transform.position.y + sr.bounds.size.y;
        }
        bottomY = camera.transform.position.y - camera.orthographicSize - sr.bounds.size.y / 2;

        isFinished = false;
        isPlaying = true;
        reachTop = reachBottom = false;
        
        
    }

    public bool IsFinished()
    {
        return isFinished;
    }
}
