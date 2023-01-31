﻿using System.Collections.Generic;

namespace RankOne.Models
{
    public class AnalysisInformation
    {
        public string Alias { get; set; }

        public List<string> Tokens { get; set; }

        public AnalysisInformation()
        {
            Tokens = new List<string>();
        }
    }
}