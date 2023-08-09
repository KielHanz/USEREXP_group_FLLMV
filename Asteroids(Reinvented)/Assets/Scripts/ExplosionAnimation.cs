using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{

    private Vector3 size;

    private void Start()
    {
        size = transform.localScale;
        transform.localScale = new Vector3(0.1f,0.1f,0.1f);
    }

    private void Update()
    {
        if (transform.localScale.x < 1)
        {
            transform.localScale += (new Vector3(0.1f, 0.1f, 0.1f) * (Time.deltaTime * 3));
        }
    }
}
