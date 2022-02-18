using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    private float fov;
    private bool playerFound = false;
    [SerializeField] LayerMask lm;
    [SerializeField] LayerMask lm2;

    // Start is called before the first frame update
    void Start()
    {
        // print("sfjkhdsfkjhsd");
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        fov = 70f;
        mesh.RecalculateBounds();
    }

    // Update is called once per frame
    void Update()
    {
        int raycount = 25;
        float angle = startingAngle;
        float angleI = fov/raycount;
        float viewD = 4f;

        Vector3[] vertices = new Vector3[raycount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[raycount * 3];

        vertices[0] = origin;
        int vertexI = 1;
        int triangleI = 0;
        for(int i = 0; i <= raycount; i++){
            Vector3 vertex;
            RaycastHit2D rc = Physics2D.Raycast(origin, getVectorFromAngle(angle), viewD, lm);
            if(rc.collider == null){
                vertex = origin + getVectorFromAngle(angle) * viewD;
            }else{
                vertex = rc.point;
                if(rc.collider.gameObject.tag == "areaPlayer") {
                    playerFound = true;
                }
            }

            vertices[vertexI] = vertex;
            if(i > 0){
                triangles[triangleI + 0] = 0;
                triangles[triangleI + 1] = vertexI - 1;
                triangles[triangleI + 2] = vertexI;
                triangleI += 3; 
            }
            
            vertexI++;
            
            angle -= angleI;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private static Vector3 getVectorFromAngle(float angle) {
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir){
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(n < 0) n += 360;
        return n;
    }

    public void setOrigin(Vector3 o){
        this.origin = o;
    }

    public void SetAimDirection(Vector3 a){
        startingAngle = GetAngleFromVectorFloat(a) + fov/2;
    }

    public Vector3 getAimDirection(){
        return getVectorFromAngle(startingAngle);
    }

    public bool lookForPlayer() => playerFound;

    public void deactivate(){ playerFound = false;}
    public void activate(){ playerFound = true; }
}
