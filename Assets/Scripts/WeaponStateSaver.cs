using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStateSaver : MonoBehaviour
{
    private Vector3 savedLocalPosition;
    private Quaternion savedLocalRotation;
    private Vector3 savedLocalScale;

    private void Awake()
    {
        savedLocalPosition = transform.localPosition;
        savedLocalRotation = transform.localRotation;
        savedLocalScale = transform.localScale;
    }

    public void SaveState()
    {
        savedLocalPosition = transform.localPosition;
        savedLocalRotation = transform.localRotation;
        savedLocalScale = transform.localScale;
    }

    public void RestoreState()
    {
        transform.localPosition = savedLocalPosition;
        transform.localRotation = savedLocalRotation;
        transform.localScale = savedLocalScale;
    }
}
