using AutoMapper;
using BlankApp1.DataBaseLay.Entitys;
using BlankApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Helpers
{
    public class CustomMapper
    {
        private static Mapper _instance;

        public static Mapper GetInstance()
        {
            if (_instance == null)
                InitializeInstance();
            return _instance;
        }

        private static void InitializeInstance()
        {
            _instance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserUI, User>().ReverseMap();
                cfg.CreateMap<TranzactionUI, Tranzaction>().ReverseMap();
                cfg.CreateMap<CategoryUI, Category>().ReverseMap();
                cfg.CreateMap<RegularTranzactionUI, RegularTranzaction>().ReverseMap();
                cfg.CreateMap<Tranzaction, TranzactionWithDateTimeStruct>().ReverseMap()
                .ForMember(dest => dest.Date, act => act.MapFrom(src => Convert.ToDateTime(src.Date)));
            }));
        }
    }
}
