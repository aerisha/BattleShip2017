using System;
using SwinGameSDK;
using NUnit.Framework;

namespace BattleShips
{
	[TestFixture]
	public class unit_test
	{
		[Test]
		public void Testing_Setup()
		{
			Assert.AreEqual ("Current Page", MenuController.Get_Current_Page);

			MenuController.PerformMainMenuAction (1);
			Assert.AreEqual ("Setting Page", MenuController.Get_Current_Page);
		}

		[Test]
		public void Testing_Instruction()
		{
			Assert.AreEqual ("Current Page", MenuController.Get_Current_Page);

			MenuController.PerformMainMenuAction (3);
			Assert.AreEqual ("Current Page", MenuController.Get_Current_Page);
		}
	}
}

