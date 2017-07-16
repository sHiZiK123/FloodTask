using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_task {
    enum States {
        Water,
        Wall,
        Air
    }

    class Cell {
        public States CellState;
    }
}
