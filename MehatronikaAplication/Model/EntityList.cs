using MehatronikaAplication.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaAplication.Model
{
  public class EntityList
  {
    private static EntityList instance;

    public Stack<CarDriverViewModel> Models { get; set; } = new Stack<CarDriverViewModel>();
    public Stack<CarDriverViewModel> DbModels { get; set; } = new Stack<CarDriverViewModel>();

    private static object syncRoot = new Object();

    private EntityList() { }

    public static EntityList getInstance()
    {
      if (instance == null)
      {
        lock (syncRoot)
        {
          if (instance == null)
            instance = new EntityList();
        }
      }
      return instance;
    }
  }
}
