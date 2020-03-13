using System;

public class PlayerTurret
{
	// The field variable for the player
	private int[] _location; // location on x,y plane ([x,y])
	private bool _isAlive; // boolean for checking if the player is dead

	// Properties
	public int[] Location // property for _location
    { get { return _location; } }

	public bool IsAlive // property for _isAlive
	{ get { return _isAlive; } set { _isAlive = value; } }

	public PlayerTurret(int xStart, int yStart)
	{
		_location = (xStart,yStart); // Starting location for the player
		_isAlive = true; // Player is alive by default

		throw NotImplementedException; // Not Finished
	}

	public void ShootMissile()
    {
		//TODO: implement shooting
	}

}
