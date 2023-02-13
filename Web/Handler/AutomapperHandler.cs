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
            CreateMap<CustomerDto, AddFollowupVM>().ReverseMap();
            CreateMap<AddFollowupVM, Followup>().ReverseMap();
            CreateMap<AddFollowupVM, BookingVM>().ReverseMap();
            CreateMap<BookingDto, BookingVM>().ReverseMap();
            CreateMap<ServiceFeedbackDto, ServiceFeedbackVM>().ReverseMap();
        }
    }
}
