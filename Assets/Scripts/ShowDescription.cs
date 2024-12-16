using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowDescription : MonoBehaviour
{
    public string url = "https://drive.google.com/file/d/1nTj-eee-Gi8TGH2GQml52v0zwNvMv6Ad/view";

    public void Show()
    {
        Application.OpenURL(url);
    }
}

