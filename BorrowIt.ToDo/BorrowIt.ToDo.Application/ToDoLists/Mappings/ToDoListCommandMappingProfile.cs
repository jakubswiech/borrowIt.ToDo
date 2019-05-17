using AutoMapper;
using BorrowIt.ToDo.Application.ToDoLists.Commands;
using BorrowIt.ToDo.Application.ToDoLists.DTOs;
using BorrowIt.ToDo.Domain.Model.ToDoList;
using BorrowIt.ToDo.Domain.Model.ToDoList.DataStructures;

namespace BorrowIt.ToDo.Application.ToDoLists.Mappings
{
    public class ToDoListCommandMappingProfile : Profile
    {
        public ToDoListCommandMappingProfile()
        {
            CreateMap<CreateToDoListCommand, ListDataStructure>();
            CreateMap<CreateToDoTaskCommand, TaskDataStructure>();
            CreateMap<CreateToDoSubTaskCommand, SubTaskDataStructure>();

            CreateMap<ToDoSubTask, ToDoSubTaskDto>()
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(x => (int) x.Status));
            CreateMap<ToDoTask, ToDoTaskDto>()
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(x => (int) x.Status))
                .ForMember(x => x.SubTasks, opt => opt.MapFrom(x => x.SubTasks));
            CreateMap<ToDoList, ToDoListDto>()
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(x => (int) x.Status))
                .ForMember(x => x.Tasks, opt => opt.MapFrom(x => x.Tasks));


        }
    }
}