using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridInputCtrl : MonoBehaviour
{
    private Animator _animator;
    private  Rigidbody2D _rigidbody2D;
    public float fly_up_speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("is_fly");

            _rigidbody2D.velocity = Vector2.up * fly_up_speed;
        }
    }
}
