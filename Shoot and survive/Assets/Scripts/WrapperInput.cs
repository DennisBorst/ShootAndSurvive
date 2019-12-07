using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapperInput : MonoBehaviour
{
#if UNITY_XBOX
    instance = new StopHemErin();
#endif

}
