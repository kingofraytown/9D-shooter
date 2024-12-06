using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscImage : MonoBehaviour
{
    public Image image;
    public Sprite none;
    public Sprite cyan;
    public Sprite orange;
    public Sprite green;
    public Sprite purple;
    public Sprite yellow;
    public Sprite red;
    public Sprite grey;
    public Sprite blue;
    public Sprite pink;

    public Color visible;
    public Color invisible;

    public enum discType
    {
        none,
        cyan,
        orange,
        green,
        purple,
        yellow,
        red,
        grey,
        blue,
        pink
    }

    public discType currentType = discType.none;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDiscType(int i)
    {
        switch (i)
        {
            case 0:
                image.sprite = none;
                image.color = invisible;
                currentType = discType.none;
                break;
            case 1:
                image.sprite = cyan;
                image.color = visible;
                currentType = discType.cyan;
                break;
            case 2:
                image.sprite = orange;
                image.color = visible;
                currentType = discType.orange;
                break;
            case 3:
                image.sprite = green;
                image.color = visible;
                currentType = discType.green;
                break;
            case 4:
                image.sprite = purple;
                image.color = visible;
                currentType = discType.purple;
                break;
            case 5:
                image.sprite = yellow;
                image.color = visible;
                currentType = discType.yellow;
                break;
            case 6:
                image.sprite = red;
                image.color = visible;
                currentType = discType.red;
                break;
            case 7:
                image.sprite = grey;
                image.color = visible;
                currentType = discType.grey;
                break;
            case 8:
                image.sprite = blue;
                image.color = visible;
                currentType = discType.blue;
                break;
            case 9:
                image.sprite = pink;
                image.color = visible;
                currentType = discType.pink;
                break;

        }
    }
}
