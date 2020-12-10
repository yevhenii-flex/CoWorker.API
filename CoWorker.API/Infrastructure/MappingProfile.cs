
using AutoMapper;
using CoWorker.API.ViewModels;
using CoWorker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorker.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingRecordViewModel, BookingRecord>();
            CreateMap<BookingRecord, BookingRecordViewModel>();

            CreateMap<RoomViewModel, Room>();
            CreateMap<Room, RoomViewModel>();

            CreateMap<SettingsRecord, SettingsRecordViewModel>();
            CreateMap<SettingsRecord, SettingsRecordViewModel>();

            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserViewModel, ApplicationUser>();
        }
    }
}
