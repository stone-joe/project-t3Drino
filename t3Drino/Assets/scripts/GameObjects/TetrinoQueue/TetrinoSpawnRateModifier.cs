using UnityEngine;
using System.Collections;

public class TetrinoSpawnRateModifier : MonoBehaviour {

	private float _decreaseAmount;
	private float _intervalPerDecrease;
	private bool _shouldApplyModifier;

	#region Public Properties
	/// <summary>
	/// Gets or sets the decrease amount (in seconds).
	/// </summary>
	/// <value>The decrease factor (in seconds).</value>
	public float DecreaseAmount
	{
		get 
		{
			return _decreaseAmount;
		}

		set
		{
			_decreaseAmount = value;
		}
	}

	/// <summary>
	/// Gets or sets the interval per decrease (in seconds).
	/// </summary>
	/// <value>The interval per decrease (in seconds).</value>
	public float IntervalPerDecrease
	{
		get
		{
			return _intervalPerDecrease;
		}

		set
		{
			_intervalPerDecrease = value;
		}
	}

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="TetrinoSpawnRateModifier"/> should apply modifier.
	/// </summary>
	/// <value><c>true</c> if should apply modifier; otherwise, <c>false</c>.</value>
	public bool ShouldApplyModifier
	{
		get 
		{
			return _shouldApplyModifier;
		}

		set 
		{
			_shouldApplyModifier = value;
		}
	}
	#endregion

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		_decreaseAmount = 0.20f;
		_intervalPerDecrease = 30f;
		_shouldApplyModifier = false;
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
	
	}

	/// <summary>
	/// Sets the decrease anount.
	/// </summary>
	/// <param name="decreaseAmount">Decrease amount.</param>
	public void SetDecreaseAmount(float decreaseAmount)
	{
		_decreaseAmount = decreaseAmount;
	}

	/// <summary>
	/// Sets the interval per decrease.
	/// </summary>
	/// <param name="intervalPerDecrease">Interval per decrease.</param>
	public void SetIntervalPerDecrease(float intervalPerDecrease)
	{
		_intervalPerDecrease = intervalPerDecrease;
	}
	
}
