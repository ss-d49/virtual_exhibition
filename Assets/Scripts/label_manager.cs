using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class Artwork {
    public int id;
    public GameObject gameobject;
    public string title;
    public string artist;
	
	public Artwork(int id, string gameobject, string title, string artist)
    {
        this.id = id;
        this.gameobject = GameObject.Find(gameobject);
        this.title = title;
        this.artist = artist;
    }
}

public class label_manager : MonoBehaviour
{
	public Artwork artwork;
    public List<Artwork> artworks = new List<Artwork>();
	float distanceFromCamera = 0.5f;
	Quaternion originalRot;
	GameObject selected;
	public bool viewObject = false;
	bool isCreated;
	Text artwork_title;
	Text artwork_artist;
	
	void Start()
	{
		//create the database
		artworks = new List<Artwork>() {
            new Artwork(0, "abi_methodicap", "The Methodicap", "Abigail Stallard"),
			new Artwork(1, "abi_methodicap-box", "The Methodicap Box", "Abigail Stallard"),
			new Artwork(2, "abi_methodicap-business-card", "The Methodicap Business Card", "Abigail Stallard"),
			new Artwork(3, "abi_methodicap-leaflet", "The Methodicap Leaflet", "Abigail Stallard"),
			new Artwork(4, "abi_immedimitt", "The Immedimitts", "Abigail Stallard")
		};
			originalRot = Quaternion.Euler(-89.98f, 0f, 0f);
			artwork_title = GameObject.Find("artwork_title").GetComponent<Text>();
			artwork_artist = GameObject.Find("artwork_artist").GetComponent<Text>();
	}
 	void Update()
	{		 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "art_object")
                {
					selected=hit.collider.gameObject;
					viewObject = !viewObject;
                }
			}
		} 
   		if (viewObject)
		{
			
 			artwork_title.text = artworks.Find(x => x.gameobject == selected).title;
			artwork_artist.text = artworks.Find(x => x.gameobject == selected).artist;
			Vector3 item_view = Camera.main.transform.position + Camera.main.transform.forward * distanceFromCamera; 
			if (!isCreated)
			{
				GameObject clone = Instantiate (selected, item_view, originalRot);
				clone.name = "clone";  // Set the instance name.
				isCreated = true;
			}
			GameObject.Find("clone").transform.Rotate(Input.GetAxis("Mouse Y"),0,Input.GetAxis("Mouse X"));
			GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
			
		}
		else
		{
 			Destroy(GameObject.Find("clone"));
			isCreated = false;
			artwork_title.text = null;
			artwork_artist.text = null;
			GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
		}	  
	}
}