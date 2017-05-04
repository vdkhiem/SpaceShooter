using UnityEngine;

public class Helper
{
    public static void Add3DTextToGameObject(GameObject gameObject, string text)
    {
        var tm = gameObject.GetComponent<TextMesh>();
        if (tm != null)
        {
            tm.text = text;
        }
        else
        {
            tm = gameObject.AddComponent<TextMesh>();
            tm.text = text;
        }
    }
}