using AutoMapper;
using BorrowIt.ToDo.Domain.Model.ToDoList;

namespace BorrowIt.ToDo.Infrastructure.ToDoSubTasks
{
    public class ToDoSubTaskMappingProfile : Profile
    {
        public ToDoSubTaskMappingProfile()
        {
            CreateMap<ToDoSubTaskEntity, ToDoSubTask>()
                .ForMember(x => x.Status, 
                    opt => opt.MapFrom(x => (ToDoListStatus) x.Status));

            CreateMap<ToDoSubTask, ToDoSubTaskEntity>()
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(x => (int) x.Status));
        }
    }
}