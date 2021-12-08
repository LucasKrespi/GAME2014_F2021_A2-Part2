using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Awake");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");

        StartCoroutine(bataa());
    }

    public IEnumerator bataa()
    {
        Debug.Log("batatinha");

        yield return new WaitForSeconds(1);

        Debug.Log("123");
    }
}