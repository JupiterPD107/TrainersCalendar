using System;
using System.Collections.Generic;

namespace Jupiter01.Models
{
    public partial class Trainer
    {
        public int TrainerId { get; set; }
        public string Name { get; set; } = null!;
        public int AdminId { get; set; }
        public string Designation { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
