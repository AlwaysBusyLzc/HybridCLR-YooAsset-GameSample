using System;
using System.Collections;
using System.Collections.Generic;
using HotUpdate.EventDefine;
using UniFramework.Event;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPipeline : MonoBehaviour
{
    public GameObject pipeline_prefab;

    public float init_x = -6.66f;           // 管道开始生成位置
    public float gap_x = 3.33f;             // 管道生成间隔
    public float range_y = 0.8f;                   // 管道随机y位置范围
    public int spawn_count = 5;             // 管道生成数量

    public bool spawn_on_start = false;     // 开始时是否生产管道

    private List<GameObject> pipeline_list;
    private EventGroup event_group;

    // Start is called before the first frame update
    void Start()
    {
        pipeline_list = new List<GameObject>();
        event_group = new EventGroup();
        event_group.AddListener<FlappyBirdEventDefine.BackgroundPosReset>(OnBackgroundPosReset);


        if (spawn_on_start)
        {
            Spawn();
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        event_group.RemoveAllListener();
    }

    void Spawn()
    {
        for (int i = 0; i < spawn_count; i++)
        {
            var go = Instantiate(pipeline_prefab, transform);
            float pos_y = Random.Range(-range_y, range_y);
            float pos_x = init_x + gap_x * i;
            go.transform.localPosition = new Vector3(pos_x, pos_y, 0);
            go.transform.localRotation = Quaternion.identity;
            pipeline_list.Add(go);
        }
    }

    void OnBackgroundPosReset(IEventMessage event_message)
    {
        FlappyBirdEventDefine.BackgroundPosReset event_msg = event_message as FlappyBirdEventDefine.BackgroundPosReset;
        if (event_msg.gameobject_name != gameObject.name)
            return;

        if (pipeline_list.Count == 0)
        {
            Spawn();
        }

        reset_y_pos();
    }

    void reset_y_pos()
    {
        foreach (var pipeline in pipeline_list)
        {
            pipeline.transform.localPosition = new Vector3(pipeline.transform.localPosition.x, Random.Range(-range_y, range_y), 0);
        }
    }
}
