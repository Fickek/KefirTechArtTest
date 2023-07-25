using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _accelerationSpeed = 1.3f;
    [SerializeField] private float _decelerationSpeed = 2.0f;

    private float _velocity;
    private Rigidbody[] _rigidbodies;
    private Animator _animator;

    private List<string> _attackTriggers;
    private const string _clawAttackTrigger = "ClawAttackTrigger";
    private const string _specialAttackTrigger = "SpecialAttackTrigger";

    
    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        _attackTriggers = new List<string>();
        _attackTriggers.Add(_clawAttackTrigger);
        _attackTriggers.Add(_specialAttackTrigger);
    }

    private void Start()
    {

        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = true;
        }

    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            string attackTrigger = _attackTriggers[Random.Range(0, _attackTriggers.Count)];
            _animator.SetTrigger(attackTrigger);
        }

        if (Input.GetKey(KeyCode.W))
        {
            _velocity += Time.deltaTime * _accelerationSpeed;
            if (_velocity > 1f)
            {
                _velocity = 1f;
            }
        }
        else
        {
            _velocity -= Time.deltaTime * _decelerationSpeed;
            if (_velocity < 0f)
            {
                _velocity = 0f;
            }
        }

        _animator.SetFloat("MoveX", _velocity);


        if (Input.GetKeyDown(KeyCode.D))
        {
            EnablePhysic();
        }
    }

    private void EnablePhysic()
    {
        _animator.enabled = false;
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = false;
        }
    }

}
