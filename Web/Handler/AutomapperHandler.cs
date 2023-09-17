using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using AutoMapper;
using Web.ViewModels;

namespace Web.Handler
{
    public class AutomapperHandler : Profile
    {
        public AutomapperHandler()
        {
            CreateMap<CustomerFollowupVM, Customer>().ReverseMap();
            CreateMap<CustomerFollowupVM, Followup>().ReverseMap();
            CreateMap<CustomerDto, DailyFollowupListVM>();
            CreateMap<CustomerFollowupDto, AddFollowupVM>().ReverseMap();
            CreateMap<AddFollowupVM, AddFollowupDto>().ReverseMap();
            CreateMap<CustomerFollowupDto, Followup>().ReverseMap();
            CreateMap<AddFollowupVM, BookingVM>().ReverseMap();
            CreateMap<BookingDto, BookingVM>().ReverseMap();
            CreateMap<ServiceFeedbackDto, ServiceFeedbackVM>().ReverseMap();
            CreateMap<BuildingDetailsDto, BuildingDetailsVM>().ReverseMap();
            CreateMap<ContractDetailsDto, ContractDetailsVM>().ReverseMap();
            CreateMap<ServiceFeedbackDto, ComplainRegisterVM>().ReverseMap();
            CreateMap<ComplainFeedbackDto, ComplainFeedbackVM>().ReverseMap();
            CreateMap<CustomerFollowupMasterDto, CustomerFollowupMasterVM>().ReverseMap();
            CreateMap<AddFollowupDto, Followup>().ReverseMap();
            CreateMap<CustomerFollowupDto, BookingVM>().ReverseMap();
            CreateMap<ComplainFeedbackDto, ComplainFeedback>().ReverseMap();
            CreateMap<CustomerDto, CustomerVM>().ReverseMap();
            CreateMap<CustomerVM, Customer>().ReverseMap();
        }
    }
}
