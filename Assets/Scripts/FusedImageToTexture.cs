﻿using UnityEngine;
using System;
using System.Collections;

public class FusedImageToTexture : MonoBehaviour {


	private int textureSize;
	Texture2D texture;
	// Use this for initialization
	public void Awake () {
		RoomFusion.GetInstance().Init();
	}
	public void Start () {
		
		texture = new Texture2D(RoomFusion.GetInstance().GetImageWidth(), RoomFusion.GetInstance().GetImageHeight(), TextureFormat.BGRA32, false);
		textureSize = RoomFusion.GetInstance ().GetImageSize ();
		GetComponent<Renderer>().material.mainTexture = texture;
		RoomFusion.GetInstance ().SetD3D11TexturePtr (texture.GetNativeTexturePtr ());
	}

	// Update is called once per frame
	void Update () {
		if (RoomFusion.GetInstance ().Update ()) {
			//LoadTexture (); // Deprecated: this is the slow way: copy from cpu memory
		}
	}
		
	public void LoadTexture(){
		// Load data into the texture and upload it to the GPU.
		texture.LoadRawTextureData(RoomFusion.GetInstance().GetCulledImagePtr(), textureSize);
		texture.Apply();
	}

	void OnApplicationQuit() {
		RoomFusion.GetInstance ().Destroy ();
	}
		

}
