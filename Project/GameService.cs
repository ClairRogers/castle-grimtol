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
      Room well = new Room("The Well", "You come to a field where you see an old, dilapidated structure. On one side is a (shed), so old that it is decaying where it stands. It is dark and musty inside. Next to it is a (well). The water is dark and smells stale. To the east, the Obelisk stands in the distance.");
      Room foxden = new Room("The Fox Den", "The closer you get, the more strong the stench becomes. You find a small clearing with that contains a fox (den), and right outside, a pile of discarded prey (bones). The smell is very strong, and you think the animal must be nearby. To the north, the Obelisk stands in the distance.");
      Room treehouse = new Room("The Treehouse", "The forest seems to get darker back here. Eventually, you happen upon a giant oak tree, and in the branches, an abandoned treehouse. You climb into the rickety structure. Inside there are some (shelves) on the back wall, and a ladder that leads to the (roof). To the west, the Obelisk stands in the distance.");
      Room caves = new Room("The Caves", "Inside the caves, it is quieter than the storms outside. There are several tunnels. In the ones to the west, eeries sounds echo... sounds that make your skin crawl. To the north, a tunnel leads deeper into the catacombs. To the south, you can hear the rain pattering outside.");
      Room gate = new Room("The Gate", "You emerge into a giant cavern, so tall you cannot see the ceiling. Here, you see a giant, ornate gate set into the stone walls. It is glowing slightly, but if you try to open it, it doesn't budge.");

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
      Item yellow = new Item("Yellow Key", "It's a shiny (yellow) key with a topaz stone inlaid in the base.");
      Item purple = new Item("Purple Key", "It's a pretty (purple) key with with an amethyst inlaid in the base.");
      Item red = new Item("Red Key", "It's a dark (red) key with a ruby inlaid in the base.");

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
      CurrentPlayer = new Player(name);
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
      if (CurrentRoom.Items.Count > 0 && (itemName + " key") == CurrentRoom.Items[0].Name.ToLower())
      {
        CurrentPlayer.AddItem(CurrentRoom.Items[0]);
        //CurrentPlayer.Inventory.Add();
        CurrentRoom.Items.Remove(CurrentRoom.Items[0]);
        System.Console.WriteLine($@"
Added item to inventory!");
      }
      else
      {
        System.Console.WriteLine($@"
Cannot add that item.");
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
        System.Console.WriteLine($@"
Oh no! Wrong choice. You died!");
        Running = false;
      }
      else
      {
        System.Console.WriteLine($@"
Nothing there.");
      }

    }






    public void Help()
    {
      Console.WriteLine($@"
(help) to open helpdesk. (quit) to quit game. (look) to view your surroundings. (inventory) to view your inventory.(go (direction)) to go somewhere. (use (item)) to use item. (take (item)) to take item. (search (location)) to search a location.");
    }

    public void Inventory()
    {
      foreach (Item item in CurrentPlayer.Inventory)
      {
        System.Console.WriteLine($"{item.Name}: {item.Description}");
      }
    }

    public void Look()
    {
      System.Console.WriteLine($@"
{CurrentRoom.Name}:
{CurrentRoom.Description}");
    }

    public void Quit()
    {
      System.Console.WriteLine($@"
Goodbye!");
      Running = false;
    }

    public void Reset()
    {

    }



  }
}