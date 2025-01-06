using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class DraftCustomerDto
    {
        public int Id { get; set; } = 0;

        public string ClientName { get; set; } = String.Empty;
        public string MobileNo { get; set; } = String.Empty;
        public string Remarks { get; set; } = String.Empty;
        public DateTime NextFollowupDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public bool IsFollowupDone { get; set; } = false;
        public string ContactName { get; set; }//FK


    }
}
