﻿using System.Collections.Generic;

namespace SymFiend.Models
{
    public class Conference
    {
        public int ConferenceId { get; set; }
        public string Name { get; set; }
        public List<string> EventThemes { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Event> Events { get; set; }
    }
}
