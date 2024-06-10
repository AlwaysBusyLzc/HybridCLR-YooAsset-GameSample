using System;
using System.Collections;
using HotUpdate.EventDefine;
using UniFramework.Event;
using UnityEngine;
using UnityEngine.Animations;
using YooAsset;
using Random = UnityEngine.Random;

public class SpawnFruit: MonoBehaviour
{
    public int fruit_kind_count = 10; // 水果种类数
    public int spawn_max_index = 3;  // 只随机出现前3种水果

    public GameObject[] fruits;
    private int current_index;
    private int next_index;

    public GameObject current_fruit;

    Vector2 touchPosition;
    private EventGroup event_group;

    private void Awake()
    {
    }

    IEnumerator Start()
    {
        // 监听事件
        event_group = new EventGroup();
        event_group.AddListener<DaxiguaEventDefine.FruitDrop>(OnFruitDrop);

        var default_package = YooAssets.GetPackage("DefaultPackage");
        fruits = new GameObject[fruit_kind_count + 1];
        for (int i = 0; i <= fruit_kind_count; i++)
        {
            var handle = default_package.LoadAssetAsync<GameObject>($"fruit_{i}");
            yield return handle;
            var prefab = handle.AssetObject as GameObject;
            fruits[i] = prefab;
        }

        current_index = GetNextSpawnIndex();
        next_index = GetNextSpawnIndex();
        var first_fruit = Instantiate(fruits[current_index], transform.position, Quaternion.identity);
        first_fruit.GetComponent<Fruit>().Fall();

        // 1秒后产生下一个
        yield return new WaitForSeconds(1);
        Spawn();
    }

    private void Update()
    {
        if (current_fruit == null)
            return;

        Fruit fruit = current_fruit.GetComponent<Fruit>();
        if (fruit.state == Fruit.FruitState.Born)
        {
            float horizontal_move = GetHorizontalMove();
            current_fruit.transform.position = new Vector3(horizontal_move, current_fruit.transform.position.y, 0);
        }

    }

    void OnFruitDrop(IEventMessage eventMessage)
    {
        Fruit fruit = current_fruit.GetComponent<Fruit>();
        fruit.Fall();

        Invoke(nameof(Spawn), 1);
    }

    // 获取水平滑动距离
    private float GetHorizontalMove()
    {
        // 如果当前在编辑器中或者在standalone模式下
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {

            if (Input.GetMouseButton(0))
            {
                Debug.Log("鼠标拖动");
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return Mathf.Clamp(touchPosition.x, -2.9f, 2.9f);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("鼠标抬起");
                DaxiguaEventDefine.FruitDrop.SendEventMessage();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {

                Touch touch = Input.GetTouch(0); // 获取第一个触点

                if (touch.phase == TouchPhase.Moved)
                {
                    Debug.Log("触摸移动");
                    touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    return Mathf.Clamp(touchPosition.x, -2.9f, 2.9f);
                }
                // 如果是触摸结束或移动（这里我们主要关注结束以判断完整滑动）
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    Debug.Log("触摸结束");
                    DaxiguaEventDefine.FruitDrop.SendEventMessage();
                }
            }

        }

        return current_fruit.transform.position.x;
    }



    int GetNextSpawnIndex()
    {
        return Random.Range(0, spawn_max_index);
    }

    void Spawn()
    {
        current_index = next_index;
        current_fruit = Instantiate(fruits[current_index], transform.position, Quaternion.identity);

        next_index = GetNextSpawnIndex();
    }
}
