using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

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
        GetComponent<Rigidbody>().useGravity = false;
    }

    public void SetState(FruitState state)
    {
        state = state;
    }

    public void Fall()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OnCollisionEnter2D(Collision2D other)
    {
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
                Instantiate(target_fruit, transform.position, Quaternion.identity);
                DestroyImmediate(gameObject);
                DestroyImmediate(other_gameobject);

            }
        }
    }
}
