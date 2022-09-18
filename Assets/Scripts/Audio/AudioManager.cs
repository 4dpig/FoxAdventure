using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public enum SoundEffectName {
        Boss_Hit,
        Boss_Impact,
        Boss_Shot,
        Enemy_Explode,
        Level_Selected,
        Map_Movement,
        Pickup_Gem,
        Pickup_Health,
        Player_Death,
        Player_Hurt,
        Player_Jump,
        Warp_Jingle
    }
    
    [Serializable]
    public struct SoundEffectItem
    {
        public SoundEffectName audioName;
        public AudioSource audioSource;
    }
    
    // 存储游戏中用到的所有音效
    public SoundEffectItem[] soundEffectItems;
    private Dictionary<SoundEffectName, AudioSource> soundEffectDict;

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        soundEffectDict = new Dictionary<SoundEffectName, AudioSource>();
        for (int i = 0; i < soundEffectItems.Length; i++)
        {
            soundEffectDict.Add(soundEffectItems[i].audioName, soundEffectItems[i].audioSource);
        }
        Debug.Log(soundEffectDict);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundEffect(SoundEffectName audioName)
    {
        AudioSource audioSource = soundEffectDict[audioName];
        // 如果当前正在播放，先停止播放，然后再重新播放
        audioSource.Stop();
        audioSource.Play();
    }
}
