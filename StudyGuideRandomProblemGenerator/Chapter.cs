using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuideRandomProblemGenerator
{
    class Chapter
    {
        public Chapter() { }

        public Chapter(int chapterNumber)
        {
            this.ChapterNumber = chapterNumber;
        }

        public int ChapterNumber { get; set; }
        public int Problems { get; set; }
    }
}
