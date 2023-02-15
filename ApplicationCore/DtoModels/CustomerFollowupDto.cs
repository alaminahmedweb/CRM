﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class CustomerFollowupDto
    {
        //Customer Portion
        public string? Name { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string AreaName { get; set; } = "";//FK
        public string EmployeeName { get; set; } = "";//FK
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; }
        public List<BuildingDetailsDto> BuildingDetails { get; set; }
        public List<ContractDetailsDto> ContractDetails { get; set; }


        //Followup Portion
        public DateTime? CallingDate { get; set; } = DateTime.Now;
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public int CustomerDoTheWorkingMonth { get; set; } = 0;
        public string? Remarks { get; set; } = String.Empty;
        public string? PositiveOrNegative { get; set; } = String.Empty;
        public string? DiscussionDetailsNote { get; set; } = String.Empty;
        public string? MarketingNextPlan { get; set; } = String.Empty;
        public DateTime FollowupCallDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = String.Empty;//Pending Or Confirm
        public int CustomerId { get; set; } = 0;//FK

    }
}