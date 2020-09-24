using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video {
    public int id;
    public GameObject gameobject;
    public string url;
	
	public Video(int id, string gameobject, string url)
    {
        this.id = id;
        this.gameobject = GameObject.Find(gameobject);
        this.url = url;
    }
}

public class video_manager : MonoBehaviour
{
	public Video video;
    public List<Video> videos = new List<Video>();
	GameObject selected;
	VideoPlayer selectedvideo;
	string selectedvideo_url;
	public bool activateVideo = false;
	
	void Start()
	{
		//create the database
		videos = new List<Video>() {
            new Video(0, "abi_monitor-screen", "abi_methodicap-video.mp4"),
			new Video(1, "alfie_monitor-screen", "alfie_video.mp4")
		};
	}	
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "video_object")
                {
					selected=hit.collider.gameObject;
					activateVideo = !activateVideo;
					selectedvideo_url = videos.Find(x => x.gameobject == selected).url;
					selectedvideo = selected.AddComponent<UnityEngine.Video.VideoPlayer>();
					selectedvideo.url = System.IO.Path.Combine (Application.streamingAssetsPath, selectedvideo_url);
					selectedvideo.playOnAwake = false;
                }
			}
		}
		
		if (activateVideo)
		{
			selectedvideo.Play();
			
		}
		else
        {
			if(selectedvideo != null)
			{
				Destroy (selected.GetComponent<UnityEngine.Video.VideoPlayer>());
			}
		}
		
	}
}
