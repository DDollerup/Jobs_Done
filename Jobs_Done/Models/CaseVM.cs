using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobs_Done.Models
{
    public class CaseVM
    {
        public Case Case { get; set; }
        public List<TaskVM> Tasks { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (TaskVM task in Tasks) total += task.Relation.Price;
                return total;
            }
        }
    }
}