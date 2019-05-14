using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropColor
{
    public DropType type;
    public bool muddy;

    public DropColor(DropType type, bool muddy=false)
    {
        this.muddy = muddy;
        this.type  = type;
    }

    public Color color
    {
        get
        {
            Color c = ColorManager.GetColor(type);
            if (muddy) c = c*0.25f +  Color.gray*0.33f;
            c.a = 1f;
            return c;
        }
    }

}

public enum DropType{
    yellow, 
    yellowOrange,
    orange,
    orangeRed,
    red,
    redPurple,
    purple,
    purpleBlue,
    blue,
    blueGreen,
    green, 
    greenYellow 
    };

public class ColorManager : MonoBehaviour {
    public static ColorManager instance;


    public static List<Color> colors = new List<Color>{
        new Color32(244, 228, 0, 255),//yellow
        new Color32(253, 198, 13, 255),//yellowOrange
        new Color32(241, 124, 28, 255),//orange
        new Color32(234, 98, 31, 255),//orangeRed
        new Color32(227, 34, 33, 255),//red
        new Color32(196, 4, 124, 255),//redPurple
        new Color32(110, 57, 138, 255),//purple
        new Color32(68, 78, 153, 255),//purpleBlue
        new Color32(42, 113, 176, 255),//blue
        new Color32(8, 150, 187, 255),//blueGreen
        new Color32(2, 142, 91, 255),//green
        new Color32(140, 187, 38, 255)//greenYellow
    };




    // Use this for initialization
    void Awake () 
    {
        instance = this;   
    }

	// Update is called once per frame
    public static Color GetColor(DropType color)
    {
 return colors[(int)color];
     
	}

    public static DropColor AddColors(DropColor color1, DropColor color2)
    {
        int colorIndex1 = (int)color1.type;
        int colorIndex2 = (int)color2.type;

        int diff = colorIndex2 - colorIndex1;
        if (diff>6) diff-=12;
        if (diff<-6) diff+=12;
        int newColorIndex = (colorIndex1 + diff/2);
        newColorIndex = (12 + newColorIndex) %12;
//        bool muddy = (Mathf.Abs(diff) == 6);
//        if (color1.muddy||color2.muddy) muddy = true;

        return  new DropColor((DropType)newColorIndex, false);
    }

    public static bool Compare (DropType color1, DropType color2)
    {
        int colorIndex1 = (int)color1;
        int colorIndex2 = (int)color2;

        int diff = colorIndex2 - colorIndex1;

        return (Mathf.Abs(diff)==0);
    }
}
