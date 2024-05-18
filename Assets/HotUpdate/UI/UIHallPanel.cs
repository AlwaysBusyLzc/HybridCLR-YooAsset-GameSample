using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHallPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var trainNearBtn = transform.Find("Trains/TrainNear").GetComponent<Button>();
        trainNearBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.LoadScene("Game_FlappyBird");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
