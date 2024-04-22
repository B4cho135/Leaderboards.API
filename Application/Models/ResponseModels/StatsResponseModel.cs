using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ResponseModels
{
    public class StatsResponseModel
    {
        public int AverageDailyScore { get; set; }
        public int AverageMonthlyScore { get; set; }
        public int MaxMonthlyScore { get; set; }
        public int MaxDailyScore { get; set; }
        public int MaxWeeklyScore { get; set; }
    }
}
