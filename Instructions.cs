
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SwinGameSDK;

/// <summary>
/// Controls displaying instruction page.
/// </summary>
/// <remarks>
/// Button named help.
/// </remarks>
static class Instructions
{

	private const int INST_LEFT = 450;
	/// <summary>
	/// The instruction structure is used to keep the name.
	/// </summary>
	private struct Instruct
	{
		public string Name;
	}


	private static List<Instruct> _Instruct = new List<Instruct>();

	/// <summary>
	/// Loads the instructions from the instructions text file.
	/// </summary>
	/// <remarks>
	/// The format is
	/// # of instructs
	/// NNN
	/// Where NNN is the name
	/// </remarks>
	private static void LoadInstruct()
	{
		string filename = null;
		filename = SwinGame.PathToResource("instructions.txt");

		StreamReader input = default(StreamReader);
		input = new StreamReader(filename);

		//Read in the # of instructs
		int numInstructs = 0;
		numInstructs = Convert.ToInt32(input.ReadLine());

		_Instruct.Clear();

		int i = 0;

		for (i = 1; i <= numInstructs; i++) {
			Instruct s = default(Instruct);
			string line = null;

			line = input.ReadLine();

			s.Name = line.Substring(0);
			_Instruct.Add(s);
		}
		input.Close();
	}

	/// <summary>
	/// Draws the help page to the screen.
	/// </summary>
	public static void DrawInstruct()
	{
		const int INST_HEADING = 40;
		const int INST_TOP = 80;
		const int INST_GAP = 30;

		const int WELC_HEADING = 500;
		const int WELC_LEFT = 350;

		if (_Instruct.Count == 0)
			LoadInstruct();

		SwinGame.DrawText("   Instructions   ", Color.White, GameResources.GameFont("Courier"), INST_LEFT, INST_HEADING);

		//Greating
		SwinGame.DrawText("   WELCOME!   ", Color.White, GameResources.GameFont("Courier"), WELC_LEFT, WELC_HEADING);
		SwinGame.DrawText("   Dear user, thank you for playing our product!   ", Color.White, GameResources.GameFont("Courier"), WELC_LEFT - 150, WELC_HEADING + INST_GAP);
		SwinGame.DrawText("   Hope you enjoy BattleShips!   ", Color.White, GameResources.GameFont("Courier"), WELC_LEFT - 70, WELC_HEADING + (INST_GAP * 2));

		//For all of the instructs
		int i = 0;
		for (i = 0; i <= _Instruct.Count - 1; i++) {
			Instruct s = default(Instruct);

			s = _Instruct[i];

			if (i < 8) {
				SwinGame.DrawText(s.Name, Color.White, GameResources.GameFont("Courier"), INST_LEFT, INST_TOP + i * INST_GAP);
			} else {
				SwinGame.DrawText(s.Name, Color.White, GameResources.GameFont("Courier"), INST_LEFT, INST_TOP + i * INST_GAP);
			}
		}
	}

	/// <summary>
	/// Handles the user input during the instruction screen.
	/// </summary>
	/// <remarks></remarks>
	public static void HandleInstructInput()
	{
		if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.vk_ESCAPE) || SwinGame.KeyTyped(KeyCode.vk_RETURN)) {
			GameController.EndCurrentState();
		}
	}

}


//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
