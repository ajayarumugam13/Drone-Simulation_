using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLaunch : MonoBehaviour
{
    public GameObject GO_MovingObject;
    public GameObject GO_From;
    public GameObject GO_To;
    public bool B_isMoving;
    public static MissileLaunch instance;
    private void Start()
    {
        instance = this;
    }
    public void moveMissile()
    {
        if(GO_To==null) return; 
        MoveObject(GO_MovingObject, GO_From, GO_To, 8);
    }
    public void MoveObject(GameObject movingObj, GameObject fromObj, GameObject toObj, float speed)
    {
        if(B_isMoving)return;if(GO_To==null)return;
        StartCoroutine(MoveCoroutine(movingObj, fromObj.transform.position, toObj.transform.position, speed));
    }
    // Start is called before the first frame update
    private IEnumerator MoveCoroutine(GameObject obj, Vector3 startPos, Vector3 endPos, float speed)
    {
        B_isMoving = true;
        // Enable object only when travelling
        obj.SetActive(true);
        obj.transform.position = startPos;

        while (Vector3.Distance(obj.transform.position, endPos) > 0.01f)
        {
            obj.transform.position = Vector3.MoveTowards(
                obj.transform.position,
                endPos,
                speed * Time.deltaTime
            );
            yield return null;
        }

        // Ensure final position
        obj.transform.position = endPos;

        // Disable after reaching destination
        obj.SetActive(false);
        B_isMoving = false;
        
    }
}
