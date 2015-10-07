using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	private float _startTime;
	private float _currentTime;
	private float _endTime;
	private bool _isStarted;
	private bool _countUp;
	private bool _countDown;
	private string _txtCurrentTime;

	#region Public Properties

	public float StartTime
	{
		get
		{
			return _startTime;
		}
		set
		{
			if (value != _startTime)
			{
				_startTime = value;
			}
		}
	}

	public float CurrentTime
	{
		get
		{
			return _currentTime;
		}
		set
		{
			if (value != _currentTime)
			{
				_currentTime = value;
			}
		}
	}

	public float EndTime
	{
		get
		{
			return _endTime;
		}
		set
		{
			if (value != _endTime)
			{
				_endTime = value;
			}
		}
	}

	public string TextCurrentTime
	{
		get
		{
			return _txtCurrentTime;
		}
		set
		{
			if (value != _txtCurrentTime)
			{
				_txtCurrentTime = value;
			}
		}
	}

	#endregion

	// Use this for initialization
	public virtual void Start ()
	{
		_currentTime = 0f;
		_endTime = 0f;
		_isStarted = false;
		_txtCurrentTime = "";
	}
	
	// Update is called once per frame
	public virtual void UpdateTime ()
	{
		if (_isStarted)
		{
			if (_countUp)
			{
				if (_currentTime <= _endTime)
				{
					_currentTime += Time.deltaTime;
					_txtCurrentTime = CurrentTimetoReadable();
				}
			}
			else if (_countDown)
			{
				if (_currentTime >= _endTime)
				{
					_currentTime -= Time.deltaTime;
					_txtCurrentTime = CurrentTimetoReadable();
				}
			}
		}
	}

	public void SetTimer(float startTime, float endTime, float currentTime)
	{
		_startTime = startTime;
		_endTime = endTime;
		_currentTime = currentTime;
	}

	public void BeginCountUp()
	{
		_isStarted = true;
		_countUp = true;
	}

	public void BeginCountDown()
	{
		_isStarted = true;
		_countDown = true;
	}

	public void Stop()
	{
		_isStarted = false;
		_countUp = false;
		_countDown = false;
	}

	private string CurrentTimetoReadable()
	{
		int minutes = Mathf.FloorToInt(_currentTime / 60F);
		int seconds = Mathf.FloorToInt(_currentTime - minutes * 60);
		string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds); // For format: 0:00
		//string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds); // For format: 00:00

		return niceTime;
	}
}
