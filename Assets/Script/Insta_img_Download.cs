using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insta_img_Download : MonoBehaviour
{

	public static Insta_img_Download instt;

	void Awake ()
	{
		instt = this;
	}

	//===============  1  ===============
	public void Download_Image_0 (string str)
	{
		StartCoroutine (isDownloading_0 (str));
	}

	public void Download_Image_1 (string str)
	{
		StartCoroutine (isDownloading_1 (str));
	}

	public void Download_Image_2 (string str)
	{
		StartCoroutine (isDownloading_2 (str));
	}

	public void Download_Image_3 (string str)
	{
		StartCoroutine (isDownloading_3 (str));
	}

	public void Download_Image_4 (string str)
	{
		StartCoroutine (isDownloading_4 (str));
	}

	IEnumerator isDownloading_0 (string url)
	{
		// Start a download of the given URL
		var www = new WWW (url);            
		// wait until the download is done
		yield return www;
		// Create a texture in DXT1 format
		Texture2D texture = new Texture2D (www.texture.width, www.texture.height, TextureFormat.DXT1, false);

		// assign the downloaded image to sprite
		www.LoadImageIntoTexture (texture);
		Rect rec = new Rect (0, 0, texture.width, texture.height);
		Sprite spriteToUse = Sprite.Create (texture, rec, new Vector2 (1f, 1f), 100);
		//FacebookManager.Instance.ProfilePicture.sprite = spriteToUse;


	}

	IEnumerator isDownloading_1 (string url)
	{
		// Start a download of the given URL
		var www = new WWW (url);            
		// wait until the download is done
		yield return www;
		// Create a texture in DXT1 format
		Texture2D texture = new Texture2D (www.texture.width, www.texture.height, TextureFormat.DXT1, false);

		// assign the downloaded image to sprite
		www.LoadImageIntoTexture (texture);
		Rect rec = new Rect (0, 0, texture.width, texture.height);
		Sprite spriteToUse = Sprite.Create (texture, rec, new Vector2 (1f, 1f), 100);
		GameManager.instt.PlayerImg [0].sprite = spriteToUse;


	}

	IEnumerator isDownloading_2 (string url)
	{
		// Start a download of the given URL
		var www = new WWW (url);            
		// wait until the download is done
		yield return www;
		// Create a texture in DXT1 format
		Texture2D texture = new Texture2D (www.texture.width, www.texture.height, TextureFormat.DXT1, false);

		// assign the downloaded image to sprite
		www.LoadImageIntoTexture (texture);
		Rect rec = new Rect (0, 0, texture.width, texture.height);
		Sprite spriteToUse = Sprite.Create (texture, rec, new Vector2 (1f, 1f), 100);
		GameManager.instt.PlayerImg [1].sprite = spriteToUse;
	}

	IEnumerator isDownloading_3 (string url)
	{
		// Start a download of the given URL
		var www = new WWW (url);            
		// wait until the download is done
		yield return www;
		// Create a texture in DXT1 format
		Texture2D texture = new Texture2D (www.texture.width, www.texture.height, TextureFormat.DXT1, false);

		// assign the downloaded image to sprite
		www.LoadImageIntoTexture (texture);
		Rect rec = new Rect (0, 0, texture.width, texture.height);
		Sprite spriteToUse = Sprite.Create (texture, rec, new Vector2 (1f, 1f), 100);
		GameManager.instt.PlayerImg [2].sprite = spriteToUse;
	}

	IEnumerator isDownloading_4 (string url)
	{
		// Start a download of the given URL
		var www = new WWW (url);            
		// wait until the download is done
		yield return www;
		// Create a texture in DXT1 format
		Texture2D texture = new Texture2D (www.texture.width, www.texture.height, TextureFormat.DXT1, false);

		// assign the downloaded image to sprite
		www.LoadImageIntoTexture (texture);
		Rect rec = new Rect (0, 0, texture.width, texture.height);
		Sprite spriteToUse = Sprite.Create (texture, rec, new Vector2 (1f, 1f), 100);
		GameManager.instt.PlayerImg [3].sprite = spriteToUse;
	}
	//==============================
}
