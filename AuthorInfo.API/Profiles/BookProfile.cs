using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorInfo.API.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Entities.Book, Models.BookDto>();
            CreateMap<Models.BookForCreationDto, Entities.Book>();
            CreateMap<Models.BookForUpdateDto, Entities.Book>().ReverseMap();
            //CreateMap<Entities.Book, Models.BookForUpdateDto>();
        }
    }
}
