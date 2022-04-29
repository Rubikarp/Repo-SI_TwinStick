using UnityEngine;
using UnityEngine.UI;

public class BorderIndicator : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] Transform player;
    [SerializeField] Transform target;

    [Header("Info")]
    public Image img;
    public float imgRotOffset = 90f;

    Vector3 targetDir;

    protected void PlaceOnScreen()
    {
        targetDir = player.position - target.position;

        //Je pars du principe que le pivot est en [0.5/0.5]
        float minX = img.GetPixelAdjustedRect().width * 0.5f;
        float minY = img.GetPixelAdjustedRect().height * 0.5f;

        float maxX = Screen.width - minX;
        float maxY = Screen.height - minX;

        Vector2 pos = KarpHelper.Camera.WorldToScreenPoint(target.position);

        //if not facing the element
        if (Vector3.Dot(-targetDir, player.forward) < 0)
        {
            pos.x = pos.x < Screen.width * 0.5f ? maxX : minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;

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
