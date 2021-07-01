using UnityEngine;

public class ScreenHelper : MonoBehaviour
{
    public static float ScreenTop;
    public static float ScreenLeft;
    public static float ScreenRight;

    private void Awake()
    {
        Vector3 cameraPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        ScreenTop = cameraPosition.y;
        ScreenLeft = cameraPosition.x - Camera.main.transform.localScale.x + 1.5f;
        ScreenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x - .5f;
    }
}
