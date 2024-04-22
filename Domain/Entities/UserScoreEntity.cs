using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserScoreEntity : BaseEntity<int>
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
    }
}
