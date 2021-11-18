using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionImages : MonoBehaviour
{
    public GameObject instructions01, instructions02;
    // Start is called before the first frame update
    void Start()
    {
        instructions02.SetActive(false);
        StartCoroutine(EraseInstructions01());
        StartCoroutine(EraseInstructions02());
    }

    private IEnumerator EraseInstructions01()
    {
        yield return new WaitForSeconds(3);

        instructions01.SetActive(false);
        instructions02.SetActive(true);

    }

    private IEnumerator EraseInstructions02()
    {
        yield return new WaitForSeconds(6);

        instructions02.SetActive(false);

    }
}
