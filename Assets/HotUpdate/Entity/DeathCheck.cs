using System;
using System.Collections;
using System.Collections.Generic;
using HotUpdate.EventDefine;
using UniFramework.Log;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // GameManager.Instance.LoadScene("Hall");

            FlappyBirdEventDefine.BirdDead.SendEventMessage();
            Debug.Log("game over!!!");

            Time.timeScale = 0;
            // 弹出结束提示面板

        }
    }
}
