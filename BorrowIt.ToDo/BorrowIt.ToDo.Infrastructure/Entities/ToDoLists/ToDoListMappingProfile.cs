using AutoMapper;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoLists
{
    public class ToDoListMappingProfile : Profile
    {
        public ToDoListMappingProfile()
        {
            CreateMap<ToDoList, ToDoListEntity>()
                .ForMember(x => x.Status, 
                    opt => opt.MapFrom(x => (int) x.Status));

            CreateMap<ToDoListEntity, ToDoList>()
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(x => (ToDoListStatus) x.Status))
                .ForMember(x => x.Tasks, opt => opt.Ignore());
        }
    }
}