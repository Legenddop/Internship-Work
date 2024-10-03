using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class NewBehaviourScript : MonoBehaviour
{
    public GameObject SpawnableFurniture;
    public ARRaycastManager rayCastManager;
    public ARPlaneManager planeManager;
    private List<ARRaycastHit> raycastHits =  new List<ARRaycastHit>();

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                bool collision = rayCastManager.Raycast(Input.GetTouch(0).position, raycastHits , TrackableType.PlaneWithinPolygon);
            
            if(collision && isButtonPressed() == false)
            {
                GameObject _object = Instantiate(SpawnableFurniture);
                _object.transform.position = raycastHits[0].pose.position;
                _object.transform.rotation = raycastHits[0].pose.rotation;
            }

            foreach(var planes in planeManager.trackables)
            {
                planes.gameObject.SetActive(false);
            }
            planeManager.enabled = false;
            }
        }
    }

    public bool isButtonPressed()
    {
        if(EventSystem.current.currentSelectedGameObject?.GetComponent<TouchPhase>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SwitchFurniture(GameObject furniture)
    {
        SpawnableFurniture = furniture;
    }
}
