using System;
using System.Collections.Generic;

namespace MovieProject.Models
{
    public class Top1MovieModel
    {
        public string ImageUri { get; set; }
        public string Name { get; set; }
        public Mpaa MPAA { get; set; }
        public ICollection<string> Directors { get; set; }
        public float Rating { get; set; }
        public ICollection<string> Genres { get; set; }
        public DateTime ReleaDate { get; set; }
        public string Slogan { get; set; }

        public string GenresString
        {
            get { return Concat(Genres); }
        }

        public string DirectorsString
        {
            get { return Concat(Directors); }
        }

        private string Concat(ICollection<string> items)
        {
            return items == null
                    ? string.Empty
                    : string.Join(", ", items);
        }
    }
}