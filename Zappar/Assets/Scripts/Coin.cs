using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager _gm;
    private float rotateValue =  0.5f;

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if (_gm.IsGameActive())
        {
            _gm.AddScore(1);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        transform.Rotate(0, rotateValue * Time.deltaTime, 0);
    }
}