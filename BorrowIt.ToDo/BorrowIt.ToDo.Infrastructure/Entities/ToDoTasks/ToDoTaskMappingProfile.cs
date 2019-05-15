using AutoMapper;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Infrastructure.Entities.ToDoTasks
{
    public class ToDoTaskMappingProfile : Profile
    {
        public ToDoTaskMappingProfile()
        {
            CreateMap<ToDoTaskEntity, ToDoTask>()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => (int) x.Status))
                .ForMember(x => x.SubTasks, opt => opt.Ignore());

            CreateMap<ToDoTask, ToDoTaskEntity>()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => (ToDoListStatus) x.Status));
        }
    }
}