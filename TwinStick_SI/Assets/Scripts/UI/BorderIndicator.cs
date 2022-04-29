using UnityEngine;
using UnityEngine.UI;

public class BorderIndicator : MonoBehaviour
{
    [Header("Info")]
    public Transform player;
    public Transform target;

    [Header("Info")]
    public Image img;
    public float imgRotOffset = 90f;

    Vector3 targetDir;

    private void Update()
    {
        if(target is not null || player is not null)
        {
            PlaceOnScreen();
        }
    }

    protected void PlaceOnScreen()
    {
        Vector2 targetPos = KarpHelper.Camera.WorldToScreenPoint(target.position);
        Vector2 playerPos = KarpHelper.Camera.WorldToScreenPoint(player.position);
        targetDir = playerPos - targetPos;

        //Je pars du principe que le pivot est en [0.5/0.5]
        float minX = img.GetPixelAdjustedRect().width * 0.5f;
        float minY = img.GetPixelAdjustedRect().height * 0.5f;

        float maxX = Screen.width - minX;
        float maxY = Screen.height - minX;

        //if not facing the element
        /*
         * if (Vector3.Dot(targetDir, player.forward) < 0)
        {
            targetPos.x = targetPos.x < Screen.width * 0.5f ? maxX : minX;
        }
        */

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        img.transform.position = targetPos;

        FaceTarget(targetDir);
    }

    protected void FaceTarget(Vector3 targetDir)
    {
        //calcul l'angle pour faire face au joueur
        float rotZ = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        img.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + imgRotOffset);
    }
}
