using Dream_Within_Dream.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream_Within_Dream.Populator
{
    public class DataPopulator
    {
        private readonly string[] dreamers = { "Cobb", "Arthur", "Ariadne", "Eames", "Yusuf", "Mal", "Saito", "Fischer" };
        private readonly string[] descriptions = {
            "Plant the idea", "Stabilize the level", "Create distraction", "Escape dream police",
            "Find the totem", "Steal the memory", "Freeze time", "Collapse the paradox"
        };

        static int dreamsCreated = 0;
        static int currentId = 1;
        static int totalDreams { get; set; } = 1000;
        

        public List<Dream> GenerateDreams(int maxDreams = 1000) {

            Console.WriteLine("Populating Dream....");
            //Thread.Sleep(2000);

            totalDreams = maxDreams;
            var rootDreams = new List<Dream>();
            while (dreamsCreated < totalDreams) {
                rootDreams.Add(CreateDreams(0));
            }
            Console.WriteLine("Dream Population finished!");
            return rootDreams;
        }

        private Dream CreateDreams(int level) {
            if (dreamsCreated >= totalDreams) return null;

            var random = new Random();

            var dream = new Dream
            {
                Id = currentId++,
                Level = level,
                Dreamer = dreamers[random.Next(dreamers.Length)],
                Description = descriptions[random.Next(descriptions.Length)],
                InnerDreams = new List<Dream>()
            };

            dreamsCreated++;

            int children = random.Next(0, 3);
            for (int i = 0; i < children && dreamsCreated < totalDreams; i++) {
                var inner = CreateDreams(level + 1);
                if (inner != null) { 
                    dream.InnerDreams.Add(inner);
                }
            }

            return dream;
        }

    }
}
