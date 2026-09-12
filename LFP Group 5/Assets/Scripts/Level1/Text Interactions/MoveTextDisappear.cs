using System.Collections;
using UnityEngine;

public class MoveTextDisappear : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(WaitToDisappear());
    }

    private IEnumerator WaitToDisappear()
    {
        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(false);
    }
}
