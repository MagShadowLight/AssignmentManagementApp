using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.Core.Interfaces
{
    public interface IAppLogger
    {
        void Log(string message);
        void Warn(string message);
        void Error(string message);
    }
}
