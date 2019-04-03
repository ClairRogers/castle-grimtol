using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Running { get; set; }

    private void Initialize()
    {
      //create all rooms
      Room obelisk = new Room("The Obelisk", "desc");
      Room well = new Room("The Well", "desc");
      Room foxden = new Room("The Fox Den", "desc");
      Room treehouse = new Room("The Treehouse", "desc");
      Room caves = new Room("The Caves", "desc");
      Room gate = new Room("The Gate", "desc");

      //add exits
      obelisk.AddExit("south", foxden);
      obelisk.AddExit("north", caves);
      obelisk.AddExit("west", well);
      obelisk.AddExit("east", treehouse);
      foxden.AddExit("north", obelisk);
      well.AddExit("east", obelisk);
      treehouse.AddExit("west", obelisk);
      caves.AddExit("south", obelisk);
      caves.AddExit("north", gate);
      gate.AddExit("south", caves);

      //create items
      Item yellow = new Item("Yellow Key", "desc");
      Item purple = new Item("Purple Key", "desc");
      Item red = new Item("Red Key", "desc");

      //add items
      foxden.AddItem(purple);
      well.AddItem(yellow);
      treehouse.AddItem(red);

      CurrentRoom = obelisk;
      Running = true;


    }

    public void Run()
    {
      Initialize();
    }






    public void GetUserInput()
    {

    }

    public void Go(string direction)
    {

    }

    public void Help()
    {

    }

    public void Inventory()
    {

    }

    public void Look()
    {

    }

    public void Quit()
    {

    }

    public void Reset()
    {

    }

    public void Setup()
    {

    }

    public void StartGame()
    {

    }

    public void TakeItem(string itemName)
    {

    }

    public void UseItem(string itemName)
    {

    }
  }
}