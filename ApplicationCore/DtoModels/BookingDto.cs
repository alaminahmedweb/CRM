﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class BookingDto
    {
        public BookingDto()
        {
            BookingItems = new List<BookingItemDto>();
            BuildingDetails = new List<BuildingDetailsDto>();
            ContractDetails = new List<ContractDetailsDto>();
        }
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string? Name { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string AreaName { get; set; } = "";//FK
        public string EmployeeName { get; set; } = "";//FK
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; }
        public string CityName { get; set; }
        public string SubAreaName { get; set; }
        public string ContactName { get; set; }
        public string CategoryName { get; set; }
        public string ReferenceBy { get; set; }
        public List<BuildingDetailsDto> BuildingDetails { get; set; }
        public List<ContractDetailsDto> ContractDetails { get; set; } 


        //Followup Portion
        [DataType(DataType.Date)]
        public DateTime? CallingDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public string CustomerDoTheWorkingMonth { get; set; } = "";
        public string? Remarks { get; set; } = String.Empty;
        public string? PositiveOrNegative { get; set; } = String.Empty;
        public string? DiscussionDetailsNote { get; set; } = String.Empty;
        public string? MarketingNextPlan { get; set; } = String.Empty;
        public string ServiceName { get; set; } = "";
        
        [DataType(DataType.Date)]
        public DateTime FollowupCallDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public int CustomerId { get; set; } = 0;//FK

        [DataType(DataType.Date)]
        public DateTime? EntryDate { get; set; }=DateTime.Now;
        
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        public int TeamId { get; set; }

        public int ShiftId { get; set; }

        public int FollowupId { get; set; }

        public int BookingById { get; set; }

        public string Status { get; set; } = String.Empty;//Pending Or Confirm
        public string? BookingNote { get; set; } = String.Empty;
        public List<BookingItemDto> BookingItems { get; set; }


    }
}
