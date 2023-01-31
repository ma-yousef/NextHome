﻿using RankOne.Models;
using Umbraco.Core.Models;

namespace RankOne.Interfaces
{
    public interface IPageAnalysisService
    {
        PageAnalysis CreatePageAnalysis(IPublishedContent node, string focusKeyword = null);
        void Save(int id, PageAnalysis pageAnalysis);
    }
}