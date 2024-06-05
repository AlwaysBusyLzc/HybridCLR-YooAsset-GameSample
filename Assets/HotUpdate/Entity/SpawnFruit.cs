using System;
using System.Collections;
using UnityEngine;
using YooAsset;
using Random = UnityEngine.Random;

public class SpawnFruit: MonoBehaviour
{
    public int fruit_kind_count = 10; // 水果种类数
    public int spawn_max_index = 3;  // 只随机出现前3种水果

    public GameObject[] fruits;
    private int current_index;
    private int next_index;

    private GameObject current_fruit;


    IEnumerator Start()
    {
        var default_package = YooAssets.GetPackage("DefaultPackage");
        fruits = new GameObject[fruit_kind_count];
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

    private void Update()
    {
        if (current_fruit == null)
            return;

        Fruit fruit = current_fruit.GetComponent<Fruit>();
        if (fruit.state == Fruit.FruitState.Born)
        {
            // 跟随触摸移动位置
            
        }


    }
}
