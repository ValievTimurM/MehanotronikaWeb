using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MehatronikaAplication.ThTimer
{
  public class ThreadTimer
  {
    private static System.Timers.Timer _timer;
    private static DateTime _startTime;

    public ThreadTimer()
    {
      _timer = new System.Timers.Timer(1000);

    }

    public void StartTimer(Thread th1, Thread th2)
    {
      _startTime = DateTime.Now;
      Console.WriteLine("Starttimer" + _startTime);
      _timer.Start();

      th1.Start();
      th2.Start();
    }
  }
}
