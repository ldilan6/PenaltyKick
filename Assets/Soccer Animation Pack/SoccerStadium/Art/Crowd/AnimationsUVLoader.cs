using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class AnimationsUVLoader 
{
	private XmlDocument 			root;
	
	public enum AnimationType
	{
		PLAYER_IDLE_TOP = 0,
		NONE
	}
	
	public class AnimationDetails
	{
		public string name;
		public List<Vector2>	uv;
		public Vector2	size;
		
		public AnimationDetails()
		{}
		public AnimationDetails(string animationName, List<Vector2>	uvList, Vector2	sizeList)
		{
			name = animationName;
			uv = uvList;
			size = sizeList;
		}
	}
	
	private AnimationType	currentAnimation;
	private List<AnimationDetails>	animationDetails;
	private static AnimationsUVLoader	instance;
	// Use this for initialization
	public static AnimationsUVLoader GetInstance () 
	{
		if(instance == null)
			instance = new AnimationsUVLoader();
		
		return instance;
	}
	
	// Update is called once per frame
	public void Init () 
	{
		animationDetails = new List<AnimationDetails>();
		
//		root = new XmlDocument ();
//		TextAsset textAsset = (TextAsset)Resources.Load("xml/AnimationFrameDetails"); 
//		root.LoadXml(textAsset.text);
//		
//		XmlNodeList xlist = root.SelectNodes("Animations/Animation");
//		
//		foreach(XmlNode node in xlist)
//		{
			//string name = node.Attributes.GetNamedItem("name").Value;
			int frameCount = AnimationManager.instance.no_of_frames;
			
			float mainTexWidth = AnimationManager.instance.mainTextureWidth;
			float mainTexHeight = AnimationManager.instance.mainTextureHeight;
			
			float x = AnimationManager.instance.startX;
			float y = AnimationManager.instance.startY;
			float width = AnimationManager.instance.frame_width / mainTexWidth;
			float height = AnimationManager.instance.frame_height / mainTexHeight;
			
			List<Vector2>uvList = new List<Vector2>();
			Vector2 	size = new Vector2(width, height);
			

			for(int i = 0 ; i < frameCount; i++)
			{
				if(x >= 1)
				{
					x = 0;
					y += height;
				}
				uvList.Add(new Vector2(x, y));
				
				x += width;
			}
			
			animationDetails.Add(new AnimationDetails("", uvList, size));
//		}
	}
	
	public AnimationDetails GetAnimationDetails(AnimationsUVLoader.AnimationType animationId)
	{
		return animationDetails[(int)animationId];
	}
	
	public AnimationDetails GetAnimationDetailsByName(string animationName)
	{
		for(int i = 0 ; i < animationDetails.Count; i++)
		{
			if(animationDetails[i].name == animationName)
				return animationDetails[i];
		}
		
		return null;
	}
}
//4702892854