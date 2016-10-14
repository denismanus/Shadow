using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    public Transform target;
    public float cam = 1f;
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 point = new Vector3(target.position.x, target.position.y+1, -5.26f);
        transform.position = Vector3.Lerp(transform.position, point, Time.deltaTime*cam);

    }
}
