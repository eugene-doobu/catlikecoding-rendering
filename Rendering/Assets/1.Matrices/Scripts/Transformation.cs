using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transformation : MonoBehaviour 
{	
    public abstract Matrix4x4 Matrix { get; }
    
    public Vector3 Apply (Vector3 point) => Matrix.MultiplyPoint(point);
}
