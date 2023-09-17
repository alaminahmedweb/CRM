using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class DashboardDto
    {
        public string TodayTotalFollowupQty { get; set; } = "";
        public string TodayFollowupDoneQty { get; set; } = "";
        public string TodayRemainingFollowupQty { get; set; } = "";
        public string ThisMonthFeedbackPendingQty { get; set; } = "";
        public string ThisMonthBookingQty { get; set; } = "";
        public string ThisMonthBookingAmount { get; set; } = "";
        public string ThisMonthComplainPendingQty { get; set; } = "";
        public List<CustomerCountDto> ThisMonthContactWiseCustomerList { get;set; }=new List<CustomerCountDto>();
    }
}
