using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
	public Texture2D cursor;
	Image instructions;
	float fadeTime=1.0f;
	public video_manager videoman;
	public label_manager labelman;
		
    void Start()
    {
//		cursor = GameObject.Find("cursor").GetComponent<Image>();
		Vector2 hotSpot = new Vector2(cursor.width / 2f, cursor.height / 4f);
		Debug.Log(hotSpot);
		Cursor.SetCursor(cursor, hotSpot, CursorMode.ForceSoftware); 
		//fade in instructions
		instructions = GameObject.Find("instructions").GetComponent<Image>();
		instructions.canvasRenderer.SetAlpha( 0.0f );
		instructions.CrossFadeAlpha(1, fadeTime, false);
    }
    // Update is called once per frame
     void Update()
    {
		//fade out instructions on click
		if (Input.GetMouseButtonDown(0))
		{
            instructions.CrossFadeAlpha(0, fadeTime, false);
		}
		
		//Hotkeys
  		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Screen.fullScreen = false;
		}
		if (Input.GetMouseButton(0))
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			Screen.fullScreen = !Screen.fullScreen;
		}

		//Cursor Script
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
			if ((hit.collider.gameObject.tag == "art_object") && labelman.viewObject == false || hit.collider.gameObject.tag == "video_object" && videoman.activateVideo == false && labelman.viewObject == false)
			{
//				cursor.canvasRenderer.SetAlpha( 1f );
				Cursor.visible = true;
			}
			else
			{
				Cursor.visible = false;
//				cursor.canvasRenderer.SetAlpha( 0f );

			}
		} 
	}
}