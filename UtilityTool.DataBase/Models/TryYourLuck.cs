using System;
using System.Collections.Generic;

namespace UtilityTool.DataBase.Models
{
    public partial class TryYourLuck
    {
        public int Id { get; set; }
        public string Numbers { get; set; }
        public int RemainingCount { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
