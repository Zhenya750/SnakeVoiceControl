using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeVoiceControl
{
    public interface ISnakeController
    {
        bool CanGetDirection();
        Direction GetDirection();
        void AddEvent(object sender, EventArgs e);
    }
}
