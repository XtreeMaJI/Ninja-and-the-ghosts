using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchingRope : MonoBehaviour
{
    private const float VERTICAL_ORIENT_CORRECTION = 90f;

    public Transform playerTransform { get; set; }
    public Transform shurikenTransform { get; set; }

    void FixedUpdate()
    {
        Vector3 shurikenPos = shurikenTransform.position;
        Vector3 playerPos = playerTransform.position;
        Vector3 scale = transform.localScale;

        //Получаем расстояние между игроком и сюрикеном
        float playerToShurikenDist = (shurikenPos - playerPos).magnitude;
        //Устанавливаем длину верёвки, равную расстоянию между объектами (делим на 2 из-за цилиндра)
        transform.localScale = new Vector3(scale.x, playerToShurikenDist/2, scale.z);

        //Ищем точку между игроком и сюрикеном
        Vector3 ropeMidPoint = playerPos + (shurikenPos - playerPos) / 2;
        transform.position = ropeMidPoint;

        if ((shurikenPos.x - playerPos.x) == 0)
            return;

        //Находим тангенс угла между горизонталью и верёвкой
        float angleTan = (shurikenPos.y - playerPos.y)/(shurikenPos.x - playerPos.x);
        //Находим угол
        float angle = Mathf.Atan(angleTan) * Mathf.Rad2Deg;
        //Устанавливаем угол поворота вокруг оси z
        transform.localEulerAngles = new Vector3(0, 0, angle - VERTICAL_ORIENT_CORRECTION);

    }

}
