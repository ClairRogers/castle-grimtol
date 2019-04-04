using System;
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

    public void Setup()
    {
      //create all rooms
      Room obelisk = new Room("The Obelisk", "You find yourself in a clearing. It is dark and stormy, and rain is falling. At the center of the clearing is a tall obelisk with strange, glowing carvings. To the north, you see the dark, yawning entrance to a cave. To the east, a lonely path that weaves into the trees. To the west, the trees appear to thin out. To the south, a stench makes your hair stand on end.");
      Room well = new Room("The Well", "You come to a field where you see an old, dilapidated structure. On one side is a (shed), so old that it is decaying where it stands. It is dark and musty inside. Next to it is a (well). The water is dark and smells stale.");
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
      Item yellow = new Item("Yellow Key", "It's a shiny yellow key with a topaz stone inlaid in the base.");
      Item purple = new Item("Purple Key", "It's a violet-colored key with with an amethyst inlaid in the base.");
      Item red = new Item("Red Key", "It's a rust-colored key with a ruby inlaid in the base.");

      //add items
      foxden.AddItem(purple);
      well.AddItem(yellow);
      treehouse.AddItem(red);

      CurrentRoom = obelisk;
      Running = true;



    }

    public void StartGame()
    {
      Setup();
      System.Console.Write("Welcome, player to the Quivering Forest. What is your name? ");
      string name = Console.ReadLine();
      Player player = new Player(name);
      System.Console.WriteLine($"{name}, the game is about to begin. Type (help) at any time for assistance. If you would like to leave the game, type (exit).");
      Console.Write($@"Let the games begin.... 
                      .
                      .
                      .
                      .
                      .
                      .
                      .
                      .
                      .");
      System.Console.WriteLine($@"
{CurrentRoom.Name}:
{CurrentRoom.Description}");
      while (Running)
      {
        Console.Write($@"
        What do you do?: ");
        string playerchoice = Console.ReadLine();
        GetUserInput(playerchoice);
      }
    }






    public void GetUserInput(string playerchoice)
    {
      string[] strArr = playerchoice.Split(" ");
      if (strArr.Length < 2)
      {
        string choice = strArr[0].ToLower();
        switch (choice)
        {
          case "help":
            Help();
            break;
          case "quit":
            Quit();
            break;
          case "restart":
            Reset();
            break;
          case "look":
            Look();
            break;
          case "inventory":
            Inventory();
            break;
          default:
            Console.WriteLine("Not a recognized command.");
            break;
        }
      }
      else
      {
        string action = strArr[0].ToLower();
        string choice = strArr[1].ToLower();
        switch (action)
        {
          case "go":
            Go(choice);
            break;
          case "use":
            //UseItem(choice);
            break;
          case "take":
            TakeItem(choice);
            break;
          case "search":
            Search(choice);
            break;
          default:
            System.Console.WriteLine("Not a recognized command.");
            break;
        }
      }
    }

    public void TakeItem(string itemName)
    {
      if (itemName.ToLower() == CurrentRoom.Items[0].Name)
      {
        CurrentPlayer.Inventory.Add(CurrentRoom.Items[0]);
        CurrentRoom.Items.Remove(CurrentRoom.Items[0]);
        System.Console.WriteLine("Added item to inventory!");
      }
      else
      {
        System.Console.WriteLine("Cannot add that item.");
      }
    }

    public void UseItem(Item itemName)
    {

    }

    public void Go(string direction)
    {
      CurrentRoom = (Room)CurrentRoom.Travel(direction);
      System.Console.WriteLine($@"
{CurrentRoom.Name}:
{CurrentRoom.Description}");
    }
    public void Search(string location)
    {
      string room = CurrentRoom.Name;
      if (CurrentRoom.Search(location, room) == 1 && CurrentRoom.Items.Count > 0)
      {
        System.Console.WriteLine($@"
You have uncovered a {CurrentRoom.Items[0].Name}! {CurrentRoom.Items[0].Description}");
      }
      else if (CurrentRoom.Search(location, room) == 2)
      {
        System.Console.WriteLine("Oh no! Wrong choice. You died!");
        Running = false;
      }
      else
      {
        System.Console.WriteLine("Nothing there.");
      }

    }






    public void Help()
    {
      Console.WriteLine("(help) to open helpdesk. (quit) to quit game. (look) to view your surroundings. (inventory) to view your inventory.(go (direction)) to go somewhere. (use (item)) to use item. (take (item)) to take item. (search (location)) to search a location.");
    }

    public void Inventory()
    {
      System.Console.WriteLine(CurrentPlayer.Inventory);
    }

    public void Look()
    {
      System.Console.WriteLine($@"
{CurrentRoom.Name}:
{CurrentRoom.Description}");
    }

    public void Quit()
    {
      System.Console.WriteLine("Goodbye!");
      Running = false;
    }

    public void Reset()
    {
      Running = false;
    }



  }
}