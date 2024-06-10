using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using YooAsset;
using Random = UnityEngine.Random;

public class Fruit : MonoBehaviour
{
    public enum FruitState
    {
        Born = 1,
        Drag = 2,
        Fall = 3,
    }


    public int fruit_type;
    public FruitState state = FruitState.Born;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetState(FruitState state)
    {
        state = state;
    }

    public void Fall()
    {
        state = FruitState.Fall;
        gameObject.AddComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == FruitState.Born)
            return;


        if (state == FruitState.Fall)
        {
            if (transform.position.y > 3f)
            {
                Debug.Log("游戏结束，你输了");
                Time.timeScale = 0;

                // 弹出结算结束面板
            }
        }
    }

    IEnumerator OnCollisionEnter2D(Collision2D other)
    {
        if (state == FruitState.Born)
            yield break;

        GameObject other_gameobject = other.gameObject;
        if (other_gameobject.CompareTag("Fruit"))
        {
            Fruit other_fruit = other_gameobject.GetComponent<Fruit>();
            if (other_fruit.fruit_type == fruit_type)
            {
                int target_fruit_type = Math.Clamp(fruit_type + 1, 0, 10);

                var res_package = YooAssets.GetPackage("DefaultPackage");
                var asset_handle = res_package.LoadAssetAsync<GameObject>($"fruit_{target_fruit_type}");
                yield return asset_handle;
                GameObject target_fruit = asset_handle.AssetObject as GameObject;
                var new_fruit = Instantiate(target_fruit, transform.position, Quaternion.identity);
                new_fruit.GetComponent<Fruit>().Fall();
                // // 产生一个爆炸力
                // new_fruit.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10, 10), 0f));

                DestroyImmediate(gameObject);
                DestroyImmediate(other_gameobject);


                if (target_fruit_type == 10)
                {
                    Debug.Log("游戏结束,你赢了");
                    Time.timeScale = 0f;
                    // 弹出结束面板
                }

            }
        }
    }
}
