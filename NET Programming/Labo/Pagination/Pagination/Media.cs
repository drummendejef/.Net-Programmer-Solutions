using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination
{
    public class Media
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public int PrevID { get; set; }
        public int NextID { get; set; }

        public Media() { }

        public override string ToString()
        {
            return string.Format("ID: {0}, Category: {1}, Title: {2}", ID, Category, Title);
        }
    }
}
