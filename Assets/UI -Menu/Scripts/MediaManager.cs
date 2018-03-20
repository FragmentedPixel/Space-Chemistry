using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaManager : MonoBehaviour
{
    private void OpenUrl(string urlToOpen)
    {
        Application.OpenURL(urlToOpen);
    }

    public void OpenFaceBook()
    {
        OpenUrl("https://www.facebook.com/DevSpaceChemistry/");
    }

    public void OpenInstagram()
    {
        OpenUrl("https://www.instagram.com/spacechemistry/");
    }

    public void OpenTwitter()
    {
        OpenUrl("https://twitter.com/space_chemistry");
    }

    public void OpenWebsite()
    {
        OpenUrl("http://spacechemistry.fragmentedpixel.com/");
    }

}
