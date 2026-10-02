using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutor : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorPage;
    public void Open()
    {
        tutorPage.SetActive(true);
    }
    public void Close()
    {
        tutorPage.SetActive(false);
    }
}
