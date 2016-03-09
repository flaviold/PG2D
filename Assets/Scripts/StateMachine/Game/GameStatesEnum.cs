/********************************************************
 |  Creating a new State:                               |
 |      1 - Put the new state name in this enum.        |
 |      2 - Create a script using IState interface.     |
 |      3 - Insert the script at states list.           |
 |          3.1 - Insert at the same position that in   |
 |                the enum order.                       |
 ********************************************************/

public enum GameStatesEnum
{
	Menu = 0,
	PreArena,
	Arena,
	PosArena
}
