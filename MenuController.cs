
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The menu controller handles the drawing and user interactions
/// from the menus in the game. These include the main menu, game
/// menu and the settings m,enu.
/// </summary>

static class MenuController
{

	/// <summary>
	/// The menu structure for the game.
	/// </summary>
	/// <remarks>
	/// These are the text captions for the menu items.
	/// </remarks>
	private static readonly string[][] _menuStructure = {
		new string[] {
			"PLAY",
			"SETUP",
			"SCORES",
			"INSTRUCTIONS", //Boon changes/Additions
			"QUIT"
		},
		new string[] {
			"RETURN",
			"SURRENDER",
			"RESTART", //Kevin WONG changes/Additions
			"QUIT",
			"SAVE"	//Boon changes/Additions
		},
		new string[] {
			"EASY",
			"MEDIUM",
			"HARD"
		},
		new string[] { //Aerisha Changes/Additions
			"GIVE UP",
			"QUIT"
		},
		new string[] { //Aerisha Changes/Additions
			"MAIN MENU",
			"EXIT"
		},
		//new string[] {
			//"PLAY",
			//"SETUP",
			//"SCORES",
			//"HELP",
			//"QUIT",
			//"RESUME"
		//}

	};
	private const int MENU_TOP = 575;
	private const int MENU_LEFT = 30;
	private const int MENU_GAP = 0;
	private const int BUTTON_WIDTH = 120;
	private const int BUTTON_HEIGHT = 30;
	private const int BUTTON_SEP = BUTTON_WIDTH + MENU_GAP;

	private const int TEXT_OFFSET = 0;
	private const int MAIN_MENU = 0;
	//private const int MAIN_MENU_2 = 3;
	private const int GAME_MENU = 1;
	private const int SETUP_MENU = 2;

	private const int COMMON_MENU = 3; //Aerisha Changes/Additions
	private const int SCORES_PAGE = 4; //Aerisha Changes/Additions

	private const int COMMON_MENU_GP_BUTTON = 0; //Aerisha Changes/Additions
	private const int COMMON_MENU_QUIT_BUTTON = 1; //Aerisha Changes/Additions

	private const int SCORES_PAGE_MAINMENU_BUTTON = 0; //Aerisha Changes/Additions
	private const int SCORES_PAGE_EXIT_BUTTON = 1; //Aerisha Changes/Additions

	private const int MAIN_MENU_PLAY_BUTTON = 0;
	private const int MAIN_MENU_SETUP_BUTTON = 1;
	private const int MAIN_MENU_TOP_SCORES_BUTTON = 2;
	private const int MAIN_MENU_INSTRUCT = 3;	//Boon changes/Addition
	private const int MAIN_MENU_QUIT_BUTTON = 4;
	//private const int MAIN_MENU_CONT_BUTTON = 5;

	private const int SETUP_MENU_EASY_BUTTON = 0;
	private const int SETUP_MENU_MEDIUM_BUTTON = 1;
	private const int SETUP_MENU_HARD_BUTTON = 2;

	private const int SETUP_MENU_EXIT_BUTTON = 3;

	private const int GAME_MENU_RETURN_BUTTON = 0;
	private const int GAME_MENU_SURRENDER_BUTTON = 1;
	private const int GAME_MENU_RESTART_BUTTON = 2;
	private const int GAME_MENU_QUIT_BUTTON = 3;
	private const int GAME_MENU_SAVE_BUTTON = 4;	//Boon changes/Addition

	private static readonly Color MENU_COLOR = SwinGame.RGBAColor(2, 167, 252, 255);

	private static readonly Color HIGHLIGHT_COLOR = SwinGame.RGBAColor(1, 57, 86, 255);

	private static string current_page = "Current Page"; 

	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleMainMenuInput()
	{	
		//if (GameController.CurrentStateHasPausedGame) {
			//HandleMenuInput (MAIN_MENU_2, 0, 0);
		//} else {
			HandleMenuInput (MAIN_MENU, 0, 0);
		//
	}

	/// <summary>
	/// Handles the processing of user input when the common menu is showing
	/// Aerisha Changes/Additions
	/// </summary>
	public static void HandleCommonMenuInput ()
	{
		HandleMenuInput (COMMON_MENU, 0, 0);
	}

	/// <summary>
	/// Handles the processing of user input when the SCORES PAGE menu is showing
	/// Aerisha Changes/Additions
	/// </summary>
	public static void HandleScoresPageInput ()
	{
		HandleMenuInput (SCORES_PAGE, 0, 0);
	}
	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleSetupMenuInput()
	{
		bool handled = false;
		handled = HandleMenuInput(SETUP_MENU, 1, 1);

		if (!handled) {
			HandleMenuInput(MAIN_MENU, 0, 0);
		}
	}

	/// <summary>
	/// Handle input in the game menu.
	/// </summary>
	/// <remarks>
	/// Player can return to the game, surrender, or quit entirely
	/// </remarks>
	public static void HandleGameMenuInput()
	{
		HandleMenuInput(GAME_MENU, 0, 0);
	}

	/// <summary>
	/// Handles input for the specified menu.
	/// </summary>
	/// <param name="menu">the identifier of the menu being processed</param>
	/// <param name="level">the vertical level of the menu</param>
	/// <param name="xOffset">the xoffset of the menu</param>
	/// <returns>false if a clicked missed the buttons. This can be used to check prior menus.</returns>
	private static bool HandleMenuInput(int menu, int level, int xOffset)
	{
		if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE)) {
			GameController.EndCurrentState();
			return true;
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			int i = 0;
			for (i = 0; i <= _menuStructure[menu].Length - 1; i++) {
				//IsMouseOver the i'th button of the menu
				if (IsMouseOverMenu(i, level, xOffset)) {
					PerformMenuAction(menu, i);
					return true;
				}
			}

			if (level > 0) {
				//none clicked - so end this sub menu
				GameController.EndCurrentState();
			}
		}

		return false;
	}

	/// <summary>
	/// Draws the main menu to the screen.
	/// </summary>
	public static void DrawMainMenu()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Main Menu", Color.White, GameFont("ArialLarge"), 50, 50)
		//if (GameController.CurrentStateHasPausedGame) {
			//DrawButtons (MAIN_MENU_2);
		//} else {
			DrawButtons (MAIN_MENU);
		//}
	}

	/// <summary>
	/// Draws the Game menu to the screen
	/// </summary>
	public static void DrawGameMenu()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Paused", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons(GAME_MENU);
	}

	/// <summary>
	/// Draws the Common menu to the screen
	/// Aerisha Changes/Additions
	/// </summary>
	public static void DrawCommonMenu ()
	{
		DrawButtons (COMMON_MENU);
	}

	/// <summary>
	/// Draws the Scores Page menu to the screen
	/// Aerisha Changes/Additions
	/// </summary>
	public static void DrawScoresPage ()
	{
		DrawButtons (SCORES_PAGE);
	}

	/// <summary>
	/// Draws the settings menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	public static void DrawSettings()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Settings", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons(MAIN_MENU);
		DrawButtons(SETUP_MENU, 1, 1);	//kevin wong : enlarge font size 
	}

	/// <summary>
	/// Draw the buttons associated with a top level menu.
	/// </summary>
	/// <param name="menu">the index of the menu to draw</param>
	private static void DrawButtons(int menu)
	{
		DrawButtons(menu, 0, 0);
	}

	/// <summary>
	/// Draws the menu at the indicated level.
	/// </summary>
	/// <param name="menu">the menu to draw</param>
	/// <param name="level">the level (height) of the menu</param>
	/// <param name="xOffset">the offset of the menu</param>
	/// <remarks>
	/// The menu text comes from the _menuStructure field. The level indicates the height
	/// of the menu, to enable sub menus. The xOffset repositions the menu horizontally
	/// to allow the submenus to be positioned correctly.
	/// </remarks>
	private static void DrawButtons(int menu, int level, int xOffset)
	{
		int btnTop = 0;

		btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int i = 0;
		for (i = 0; i <= _menuStructure[menu].Length - 1; i++) {
			int btnLeft = 0;
			btnLeft = MENU_LEFT + BUTTON_SEP * (i + xOffset);
			//SwinGame.FillRectangle(Color.White, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT)
			SwinGame.DrawTextLines(_menuStructure[menu][i], MENU_COLOR, Color.Black, GameResources.GameFont("Menu"), FontAlignment.AlignCenter, btnLeft + TEXT_OFFSET, btnTop + TEXT_OFFSET, BUTTON_WIDTH, BUTTON_HEIGHT);

			if (SwinGame.MouseDown(MouseButton.LeftButton) & IsMouseOverMenu(i, level, xOffset)) {
				SwinGame.DrawRectangle(HIGHLIGHT_COLOR, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			}
		}
	}

	/// <summary>
	/// Determined if the mouse is over one of the button in the main menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <returns>true if the mouse is over that button</returns>
	private static bool IsMouseOverButton(int button)
	{
		return IsMouseOverMenu(button, 0, 0);
	}

	/// <summary>
	/// Checks if the mouse is over one of the buttons in a menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <param name="level">the level of the menu</param>
	/// <param name="xOffset">the xOffset of the menu</param>
	/// <returns>true if the mouse is over the button</returns>
	private static bool IsMouseOverMenu(int button, int level, int xOffset)
	{
		int btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int btnLeft = MENU_LEFT + BUTTON_SEP * (button + xOffset);

		return UtilityFunctions.IsMouseInRectangle(btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
	}

	/// <summary>
	/// A button has been clicked, perform the associated action.
	/// </summary>
	/// <param name="menu">the menu that has been clicked</param>
	/// <param name="button">the index of the button that was clicked</param>
	private static void PerformMenuAction(int menu, int button)
	{
		switch (menu) {
			case MAIN_MENU:
				PerformMainMenuAction (button);
				break;
			case SETUP_MENU:
				PerformSetupMenuAction(button);
				break;
			case GAME_MENU:
				PerformGameMenuAction(button);
				break;
			case COMMON_MENU: 					//Aerisha Changes/Additions
				PerformCommonMenuAction (button);
			break;
			case SCORES_PAGE:
				PerformScoresPageMenuAction (button);
			break;
		}
	}

	/// <summary>
	/// The common menu was clicked, perform the button's action.
	/// Aerisha Changes/Additions
	/// </summary>
	/// <param name="button">the button pressed</param>
	public static void PerformCommonMenuAction (int button)
	{
		switch (button) {
		case COMMON_MENU_GP_BUTTON:
			GameController.AddNewState(GameState.ViewingGameMenu);
			if (GameController.CurrentState == GameState.ViewingGameMenu)
			{
				current_page = "Current Page";
			}
			break;
		case COMMON_MENU_QUIT_BUTTON:
			GameController.AddNewState(GameState.Quitting);
			break;
		}
	}

	/// <summary>
	/// The common menu was clicked, perform the button's action.
	/// Aerisha Changes/Additions
	/// </summary>
	/// <param name="button">the button pressed</param>
	public static void PerformScoresPageMenuAction (int button)
	{
		switch (button) {
		case SCORES_PAGE_MAINMENU_BUTTON:
			GameController.AddNewState(GameState.ViewingMainMenu);
			if (GameController.CurrentState == GameState.ViewingMainMenu)
			{
				current_page = "Current Page";
			}
			break;
		case SCORES_PAGE_EXIT_BUTTON:
			GameController.AddNewState(GameState.Quitting);
			break;
		}
	}

	/// <summary>
	/// The main menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	public static void PerformMainMenuAction(int button)
	{
		switch (button) {
			case MAIN_MENU_PLAY_BUTTON:
				GameController.StartGame();
				break;
			case MAIN_MENU_SETUP_BUTTON:
				GameController.AddNewState(GameState.AlteringSettings);
			if (GameController.CurrentState == GameState.AlteringSettings)
			{
				current_page = "Setting Page";
			}
				break;
			case MAIN_MENU_TOP_SCORES_BUTTON:
				GameController.AddNewState(GameState.ViewingHighScores);
				break;
			case MAIN_MENU_INSTRUCT:
				GameController.AddNewState(GameState.ViewingInstructMenu);
			if (GameController.CurrentState == GameState.ViewingInstructMenu)
			{
				current_page = "Current Page";
			}
				break;
			case MAIN_MENU_QUIT_BUTTON:
				GameController.EndCurrentState();
				break;

			//case MAIN_MENU_CONT_BUTTON:
				//GameController.SwitchState(GameState.Discovering);
			//break;
		}
	}

	public static string Set_Current_Page
	{
		get
		{
			return current_page;
		}
		set
		{
			current_page = value;
		}
	}

	public static string Get_Current_Page
	{
		get
		{
			return current_page;
		}
	}

	/// <summary>
	/// The setup menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformSetupMenuAction(int button)
	{
		switch (button) {
			case SETUP_MENU_EASY_BUTTON:
				GameController.SetDifficulty(AIOption.Hard);
				break;
			case SETUP_MENU_MEDIUM_BUTTON:
				GameController.SetDifficulty(AIOption.Hard);
				break;
			case SETUP_MENU_HARD_BUTTON:
				GameController.SetDifficulty(AIOption.Hard);
				break;
		}
		//Always end state - handles exit button as well
		GameController.EndCurrentState();
	}

	/// <summary>
	/// The game menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformGameMenuAction(int button)
	{
		switch (button) {
			case GAME_MENU_RETURN_BUTTON:
				GameController.EndCurrentState();
				break;
			case GAME_MENU_SURRENDER_BUTTON:
				GameController.EndCurrentState();
				//end game menu
				GameController.EndCurrentState();
				//end game
				break;
			case GAME_MENU_RESTART_BUTTON:
				GameController.StartGame();
				break;
			case GAME_MENU_QUIT_BUTTON:
				GameController.AddNewState(GameState.Quitting);
				break;
		case GAME_MENU_SAVE_BUTTON:	//Boon Changes/Additions
			GameController.AddNewState (GameState.ViewingMainMenu);
			break;
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
