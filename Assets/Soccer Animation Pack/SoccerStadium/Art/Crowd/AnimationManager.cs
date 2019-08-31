using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {
	
	public enum PlayType{
		PLAYONCE_CIRCLE,
		PLAYONCE,
		PLAYLOOP,
		FRAME,
		PAUSE,
		STOP
	};	

	public static AnimationManager instance;
	private AnimationsUVLoader.AnimationDetails		animationDetails = null;	
	
	public PlayType		playType = PlayType.STOP;
	public  float 	playDelay = -1;
	public bool 	isReadyToPlay = false;
	
	
	private float 	time = 0;
	private int 	animationFrameIndex = 0;
	private int 	pauseAtFrame_num = -1;
	private int 	stopAtFrame_num = -1;


	public int no_of_frames;
	public float startX;
	public float startY;
	public float frame_width;
	public float frame_height;
	public float mainTextureWidth;
	public float mainTextureHeight;

	public AnimationsUVLoader.AnimationType		animationType = AnimationsUVLoader.AnimationType.NONE;
	// Use this for initialization
	void Awake()
	{
		instance = this;
		AnimationsUVLoader.GetInstance ().Init ();
	}

	void Start () 
	{
		if(isReadyToPlay)
			PlayAnimation(animationType, playType, playDelay);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(playType != PlayType.STOP && playType != PlayType.PAUSE)
		{
			if(playDelay != -1)
				time += Time.deltaTime;
			
			if(time > playDelay)
			{
				time = 0;
				
				GetComponent<Renderer>().material.mainTextureOffset = animationDetails.uv[animationFrameIndex];
				GetComponent<Renderer>().material.mainTextureScale = animationDetails.size;
				
				if(animationFrameIndex == pauseAtFrame_num)
					SetAnimPlayState(PlayType.PAUSE);
				
				if(animationFrameIndex == stopAtFrame_num)
					SetAnimPlayState(PlayType.STOP);
				
				animationFrameIndex++;
				
				CheckState();
			}
		}
	}
	
	public void PlayAnimation(AnimationsUVLoader.AnimationType animType, PlayType	type, float playdelay = 0.1f)
	{
		playDelay = playdelay;
		
		if(animationType != animType || isReadyToPlay)
		{
			SetAnimPlayState(type);
			animationDetails = AnimationsUVLoader.GetInstance().GetAnimationDetails(animType);
			animationType = animType;
			animationFrameIndex = 0;
			pauseAtFrame_num = -1;
			stopAtFrame_num = -1;
			
			GetComponent<Renderer>().material.mainTextureOffset = animationDetails.uv[animationFrameIndex];
			GetComponent<Renderer>().material.mainTextureScale = animationDetails.size;
		}
	}
	
	private void CheckState()
	{
		switch(playType)
		{
		case PlayType.PLAYONCE_CIRCLE:
			if(animationFrameIndex == animationDetails.uv.Count)
			{
				animationFrameIndex = 0;
				stopAtFrame_num = 0;
			}
			break;	
		case PlayType.PLAYONCE:
			if(animationFrameIndex == animationDetails.uv.Count)
				SetAnimPlayState(PlayType.STOP);
			break;
			
		case PlayType.PLAYLOOP:
			if(animationFrameIndex == animationDetails.uv.Count)
				animationFrameIndex = 0;
			break;
		}
	}
	
	public void PauseAnimation()
	{
		SetAnimPlayState(PlayType.PAUSE);
	}
	
	public void SetAnimPlayState(PlayType playState)
	{
		playType = playState;
		switch(playType)
		{
		case PlayType.STOP:
			animationFrameIndex = 0;
			break;
		}
	}
	
	public PlayType GetAnimPlayState()
	{
		return playType;
	}
		
	
	public void StopAnimation()
	{
		SetAnimPlayState(PlayType.STOP);
	}
	
	public void PauseAniamtionAtFrame(int frameNum)
	{
		pauseAtFrame_num = frameNum;
	}
	
	public AnimationsUVLoader.AnimationType GetAnimationType()
	{
		return animationType;
	}
	
	public int GetFrameCount(AnimationsUVLoader.AnimationType animType)
	{
		return AnimationsUVLoader.GetInstance().GetAnimationDetails(animType).uv.Count;
	}
}
