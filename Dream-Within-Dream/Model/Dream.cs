using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream_Within_Dream.Model
{
    public class Dream
    {
        public int Id { get; set; }
        public string? Dreamer { get; set; }
        public string? Description { get; set; }
        public int Level { get; set; }
        public List<Dream>? InnerDreams { get; set; }

        public override string ToString()
        {
            return $"Id : {Id} , Dreamer: {Dreamer} ,Description: {Description} ,Level: {Level} ,Subdreams: {InnerDreams?.Count}";
        }
    }
}
