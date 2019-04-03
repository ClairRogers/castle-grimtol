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
      System.Console.WriteLine("There's nothing there!");
      return (IRoom)this;
    }


    public Room(string name, string desc)
    {
      Exits = new Dictionary<string, IRoom>();
      Items = new List<Item>();
      Name = name;
      Description = desc;

    }


    // public enum Direction
    // {
    //   north,
    //   south,
    //   east,
    //   west
    // }
  }
}