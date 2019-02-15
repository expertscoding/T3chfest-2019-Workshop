using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ECApi.Data
{
    public class Film
    {
        public Film()
        {

        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Director { get; set; }

        public short? Year { get; set; }

        public string ImdbLink { get; set; }

        public float ImdbScore { get; set; }

        private string keywords;
        public List<string> Keywords
        {
            get { return keywords?.Split('|').ToList(); }
            set
            {
                if (value != null && value.Count > 0)
                {
                    keywords = value.Aggregate("", (o, s) => string.Concat(o, "|", s)).Substring(1);
                }
                else
                {
                    keywords = "";
                }
            }
        }
    }
}