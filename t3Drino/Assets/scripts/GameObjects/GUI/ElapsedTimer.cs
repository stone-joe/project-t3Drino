using UnityEngine;
using System.Collections;

public class ElapsedTimer : Timer {

	private TextMesh _txtMesh;
	private bool _isFrozen;

	#region Public Properties
	public bool IsFrozen
	{
		get
		{
			return _isFrozen;
		}
		
		set
		{
			_isFrozen = value;
		}
	}
	#endregion

	// Use this for initialization
	public override void Start ()
	{
		base.Start();

		SetTimer(0f, float.PositiveInfinity, 0f);
		BeginCountUp();

		_txtMesh = GetComponent<TextMesh>();
	}

	void FixedUpdate () 
	{
		base.UpdateTime();
		if (!_isFrozen)
			_txtMesh.text = base.TextCurrentTime;
	}

	public void Pause()
	{
		base.Pause();
	}

	public void Resume()
	{
		base.Resume();
	}

	// GUI settings
	public void SetText(string text)
	{
		_txtMesh.text = text;
	}

	public void FreezeTimeUpdating()
	{
		_isFrozen = true;
	}
	
	public void UnFreezeTimeUpdating()
	{
		_isFrozen = false;
	}

	public string GetCurrentText()
	{
		return _txtMesh.text;
	}
	
	public void SetFontSize(int size)
	{
		_txtMesh.fontSize = size;
	}
	
	public void SetFontStyleToBold()
	{
		_txtMesh.fontStyle = FontStyle.Bold;
	}
	
	public void AlignToCenter()
	{
		_txtMesh.alignment = TextAlignment.Center;
	}
	
	public void SetTextColor(Color color)
	{
		_txtMesh.color = color;
	}

}
