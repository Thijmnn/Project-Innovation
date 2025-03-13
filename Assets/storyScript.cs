using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class storyScript : MonoBehaviour
{
    [SerializeField] List<Sprite> im = new List<Sprite>();
    [SerializeField] Image image;
    [SerializeField] UI_Script script;
    public int curPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }
    public void nextPage()
    {
        if(curPage < im.Count-1)
        curPage++;
        else
        {
            script.closeComic();
        }
        image.sprite = im[curPage];
    }
}
