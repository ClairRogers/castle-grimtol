using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }



    public void AddExit(string direction, IRoom room)
    {
      Exits.Add(direction, room);
    }

    public void AddItem(Item item)
    {
      Items.Add(item);
    }

    public IRoom Travel(string direction)
    {
      if (Exits.ContainsKey(direction))
      {
        return Exits[direction];
      }
      System.Console.WriteLine($@"
There's nothing there!");
      return (IRoom)this;
    }

    public int Search(string search, string room)
    {
      if (room == "The Well")
      {
        if (search.ToLower() == "well")
        {
          return 1;
        }
        else if (search.ToLower() == "shed")
        {
          return 2;
        }
        else
        {
          return 3;
        }
      }
      else if (room == "The Treehouse")
      {
        if (search.ToLower() == "shelves")
        {
          return 1;
        }
        else if (search.ToLower() == "roof")
        {
          return 2;
        }
        else
        {
          return 3;
        }
      }
      else if (room == "The Fox Den")
      {
        if (search.ToLower() == "bones")
        {
          return 1;
        }
        else if (search.ToLower() == "den")
        {
          return 2;
        }
        else
        {
          return 3;
        }
      }
      else
      {
        return 3;
      }

    }




    public Room(string name, string desc)
    {
      Exits = new Dictionary<string, IRoom>();
      Items = new List<Item>();
      Name = name;
      Description = desc;

    }
  }
}