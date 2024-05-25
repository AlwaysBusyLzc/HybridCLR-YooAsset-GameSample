using HotUpdate.EventDefine;
using UniFramework.Event;
using UnityEngine;

public class RepeatBackgroundHorizontalcs : MonoBehaviour
{
    public float speed;
    private float bg_width;     // 背景精灵图片宽度
    private Vector3 reset_pos;

    private void Start()
    {
        var sp = GetComponent<SpriteRenderer>();
        bg_width = sp.sprite.bounds.size.x;
        reset_pos = new Vector3(bg_width, 0, 0);
    }

    private void Update() {
       transform.position += Vector3.right * (speed * Time.deltaTime);
       if (transform.position.x < -bg_width)
       {
           transform.position = reset_pos;
           FlappyBirdEventDefine.BackgroundPosReset.SendEventMessage(gameObject.name);
       }
    }
    

}