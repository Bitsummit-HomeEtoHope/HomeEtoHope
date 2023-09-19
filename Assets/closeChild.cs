using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeChild : MonoBehaviour
{
    private void OnDisable()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>(true);

        for (int i = 1; i < childTransforms.Length; i++)
        {
            childTransforms[i].gameObject.SetActive(false);
        }
    }
}
