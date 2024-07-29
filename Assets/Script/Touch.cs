using System.Collections;
using System.Collections.Generic;
using Mirror.Examples.Basic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Touch : MonoBehaviour
{
    // public GameObject player;
    // void OnMouseDown(){
    //     Player playerScript = FindObjectOfType<Player>();
    //     if(playerScript != null){
    //         playerScript.CmdMoveCard(gameObject);
    //     }
    // }
    
    // public void ClickAt(Vector2 pos, bool pressed){
    //     Input.simulateMouseWithTouches = true;
    //     PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
    //     pointerEventData.position = pos;
    //     Debug.Log(pos);

    // }
    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Vector3 clickPosition = new Vector3(Input.mousePosition.x,Input.mousePosition.x,0);
    //         ClickAt(clickPosition, true);
    //         player.transform.position = clickPosition;
    //     }

    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         Vector3 clickPosition = Input.mousePosition;
    //         ClickAt(clickPosition, false);
    //     }
    // }
}
