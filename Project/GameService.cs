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

    public int Rain { get; set; }

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
      Rain = 0;



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
                      .
You come to consciousness slowly, and when you do, you smell the rain and cool air and feel droplets hitting your pelt. You open your eyes and stand. You have taken on the form of a cat--ears, whiskers and all. The forest you are in is stormy and dark, with rain falling heavily. You notice that, even as you watch, water is pooling on the ground rapidly, inching up your paws. You must go, quickly.
");
      System.Console.WriteLine($@"
{CurrentRoom.Name}:
{CurrentRoom.Description}");
      while (Running)
      {
        System.Console.WriteLine($@"
        Rain level: {Rain} inches");
        if (Rain >= 15)
        {
          System.Console.WriteLine($@"
        The water level is too high for your tiny frame, and you drown. Game over!");
          Running = false;
        }
        else
        {
          Console.Write($@"        What do you do?: ");
          string playerchoice = Console.ReadLine();
          Rain++;
          GetUserInput(playerchoice);
        }
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
          case "use":
            UseItem();
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


    public void UseItem()
    {
      if (CurrentPlayer.Inventory.Count > 0)
      {
        System.Console.WriteLine("Which item?");
        int i = 1;
        foreach (Item item in CurrentPlayer.Inventory)
        {
          System.Console.WriteLine($"({i}) {item.Name}: {item.Description}");
          i++;
        }
        string num = Console.ReadLine();
        if (CurrentRoom.Name == "The Gate")
        {
          int choice;
          if (Int32.TryParse(num, out choice) && choice <= CurrentPlayer.Inventory.Count)
          {

            if (CurrentPlayer.Inventory[choice - 1].Name == "Yellow Key")
            {
              System.Console.WriteLine($@"

        The gate unlocks... you step through.

 __     __            __          __ _         _ 
 \ \   / /            \ \        / /(_)       | |
  \ \_/ /___   _   _   \ \  /\  / /  _  _ __  | |
   \   // _ \ | | | |   \ \/  \/ /  | || '_ \ | |
    | || (_) || |_| |    \  /\  /   | || | | ||_|
    |_| \___/  \__,_|     \/  \/    |_||_| |_|(_)
                                                 
        Congratulatons, {CurrentPlayer.PlayerName}!
                 ");
              Running = false;
            }
            else
            {
              System.Console.WriteLine($@"
Item has no effect.");
            }
          }
          else
          {
            System.Console.WriteLine($@"
No such item in your inventory.");
          }
        }
        else
        {
          System.Console.WriteLine($@"
Cannot use that here.");
        }
      }
      else
      {
        System.Console.WriteLine($@"
You don't have any items to use.");
      }
    }



    public void Go(string direction)
    {
      if (CurrentRoom.Name == "The Caves" && direction == "west")
      {
        System.Console.WriteLine($@"
        You go to the west, and the sounds get louder. As you turn the corner, you see a horrendous monster--a giant, disfigured spider with multiple heads sprouting from its body: a goat, a dog, a snake. It spots you immediately, and before you can run, it attacks. You die before you even have a chance to fully realize your mistake.
        GAME OVER.");
        Running = false;
      }
      else
      {
        CurrentRoom = (Room)CurrentRoom.Travel(direction);
        System.Console.WriteLine($@"
{CurrentRoom.Name}:
{CurrentRoom.Description}");
      }
    }




    public void Search(string location)
    {
      string room = CurrentRoom.Name;
      if (CurrentRoom.Search(location, room) == 1 && CurrentRoom.Items.Count > 0)
      {
        System.Console.WriteLine($@"
You have spotted a {CurrentRoom.Items[0].Name}! {CurrentRoom.Items[0].Description}
          .---.
         / /\ |\________________
        | (  )| ________   _   _|
         \ \/ |/        |_| | |
          `---'             |_|
  ");
      }
      else if (CurrentRoom.Search(location, room) == 2)
      {
        if (CurrentRoom.Name == "The Well")
        {
          System.Console.WriteLine($@"
You creep your way into the shed. You begin rooting around, but when you push aside an overturned bucket, a rattlesnake is waiting for you! He strikes, and you cannot get away.
You have died!");
          Running = false;
        }
        else if (CurrentRoom.Name == "The Fox Den")
        {
          System.Console.WriteLine($@"
You approach the den, but the smell only grows stronger. You realize this is a mistake, but before you have the chance to turn around, a fox leaps forward and attacks you!
You have died!");
          Running = false;
        }
        else
        {
          System.Console.WriteLine($@"
You climb up the stair and onto the roof. Unfortunately, the roof is incredibly slick from the rain. You lose your footing, and plummet from the treehouse.
You have died!");
          Running = false;
        }
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
(help) to open helpdesk. 
(quit) to quit game. 
(restart) to reset the game. 
(look) to view your surroundings. 
(inventory) to view your inventory.
(go (direction)) to go somewhere. 
(search (location)) to search a location.
(take (item)) to take item. 
(use) to use an item.");
    }

    public void Inventory()
    {
      int i = 1;
      foreach (Item item in CurrentPlayer.Inventory)
      {
        System.Console.WriteLine($"({i}) {item.Name}: {item.Description}");
        i++;
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
      Console.Clear();
      StartGame();
    }



  }
}