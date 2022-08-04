using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAnimationHandler : MonoBehaviour
{
    public delegate void OnThrowAnimEnd();
    public OnThrowAnimEnd onThrowAnimEnd;

    public void ThrowShuriken()
    {
        onThrowAnimEnd();
    }
    

}
