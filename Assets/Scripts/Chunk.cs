using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Chunk : MonoBehaviour
{
        public Transform Begin;
        public Transform End;
        public Transform EndWithSpace;
        public AnimationCurve ChanceFromDistance;
}
