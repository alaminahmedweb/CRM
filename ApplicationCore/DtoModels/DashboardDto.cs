using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class DashboardDto
    {
        public DashboardDto()
        {
            TodayContactWiseCustomerList = new List<CustomerCountDto>();
        }
        public string TotalCustomer { get; set; } = "";
        public string TodayNewCustomer { get; set; } = "";
        public string TodayTotalFollowupQty { get; set; } = "";
        public string TodayFollowupDoneQty { get; set; } = "";
        public string TodayRemainingFollowupQty { get; set; } = "";
        public string ThisMonthFeedbackPendingQty { get; set; } = "";
        public string ThisMonthBookingQty { get; set; } = "";
        public string TodayBookingQty { get; set; } = "";
        public string TodayBookingAmount { get; set; } = "";
        public string ThisMonthBookingAmount { get; set; } = "";
        public string ThisMonthComplainPendingQty { get; set; } = "";
        public List<CustomerCountDto> TodayContactWiseCustomerList { get;set; }
    }
}
