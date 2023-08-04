using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices;

    public int xSize, ySize;

    private void Awake () 
    {
        Generate();
    }
    
    private void Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        var uv       = new Vector2[vertices.Length];
        var tangents = new Vector4[vertices.Length];
        // 탄젠트의 4번째 값은 항상 -1 또는 1이며, 이는 탄젠트 공간 차원의 방향을 앞뒤로 제어하는데 사용합니다.
        // 이를 통해 노멀맵의 미러링이 용이해집니다.
        var tangent  = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= ySize; y++) 
        {
            for (int x = 0; x <= xSize; x++, i++) 
            {
                vertices[i] = new Vector3(x, y);
                uv[i]       = new Vector2((float)x / xSize, (float)y / ySize);
                tangents[i] = tangent;
            }
        }
        mesh.vertices = vertices;
        mesh.uv       = uv;
        mesh.tangents = tangents;

        var triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti]     = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                mesh.triangles    = triangles;
		        mesh.RecalculateNormals();
            }
        }
    }
    
    private void OnDrawGizmos () 
    {
        if (vertices == null) return;

        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++) 
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
