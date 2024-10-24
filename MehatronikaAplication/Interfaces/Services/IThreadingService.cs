using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MehatronikaAplication.Interfaces.Services
{
  public interface IThreadingService
  {
    void StopCarThread();
    void ResumeCarThread();
    void StopDriveThread();
    void ResumeDriveThread();
    void Start();
  }
}
