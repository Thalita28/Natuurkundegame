using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorUpdater : MonoBehaviour
{

    public Renderer Trusters;
    public Renderer Cockpit;
    public Renderer Body;
    // Start is called before the first frame update
    void Start()
    {
        SetColors();
    }

    private void SetColors()
    {
        string colorString;
        int i;
        for (i = 0; i < 3; i++)
        {
            colorString = "Something";
            if (i == 0) colorString = PlayerPrefs.GetString("TrusterColor", "RGBA(1.000, 0.000, 0.000, 1.000)");
            if (i == 1) colorString = PlayerPrefs.GetString("BodyColor", "RGBA(0.000, 1.000, 0.500, 1.000)");
            if (i == 2) colorString = PlayerPrefs.GetString("CockpitColor", "RGBA(0.000, 1.000, 1.000, 1.000)");

            //  Debug.Log(PlayerPrefs.GetString("TrusterColor"));
           // Debug.Log("In SetColors: " + colorString);

            int start = colorString.IndexOf("(");
            int length = colorString.IndexOf(")") - start - 2;
            string s = colorString.Substring(start + 1, length);

            string[] nums = s.Split(","[0]);

            float _red, _green, _blue, _a = 0;

            float.TryParse(nums[0], out _red);
            float.TryParse(nums[1], out _green);
            float.TryParse(nums[2], out _blue);
            float.TryParse(nums[3], out _a);

            //Debug.Log("Parsed =  (" + _red / 1000 + ", " + _green / 1000 + ", " + _blue / 1000 + ")");

            if (i == 0) Trusters.material.color = new Color(_red / 1000, _green / 1000, _blue / 1000);
            if (i == 1) Body.material.color = new Color(_red / 1000, _green / 1000, _blue / 1000);
            if (i == 2) Cockpit.material.color = new Color(_red / 1000, _green / 1000, _blue / 1000);

        }
    }

   
}
