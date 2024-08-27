using AutoMapper;
using NoteApp.Abstractions;
using NoteApp.Models.DbSet;
using NoteApp.Models.Dtos;
using NoteApp.Models.Vms;

namespace NoteApp.Configurations.Mappings
{
    public class NoteMappingProfile : Profile
    {
        public NoteMappingProfile() { }
        public NoteMappingProfile(ITimeProvider timeProvider)
        {
            CreateMap<Note, NoteVm>();

            CreateMap<Note, DetailedNoteVm>();

            CreateMap<IEnumerable<Note>, NoteListVm>()
                .ForCtorParam(nameof(NoteListVm.Notes),
                source => source.MapFrom(s => s));

            CreateMap<EditNoteDto, Note>()
                .ForMember(dest => dest.LastModifiedDate,
                source => source.MapFrom(s => timeProvider.UtcNow));

            CreateMap<CreateNoteDto, Note>()
                .ForMember(dest => dest.Id,
                source => source.Ignore()) 
                .ForMember(dest => dest.Name,
                source => source.MapFrom(s => s.Name.Trim()))
                .ForMember(dest => dest.Description,
                source => source.MapFrom(s => s.Description))
                .ForMember(dest => dest.CreationDate,
                source => source.MapFrom(_ => timeProvider.UtcNow))
                .ForMember(dest => dest.Priority,
                source => source.MapFrom(s => s.Priority))
                .ForMember(dest => dest.IsCompleted,
                    source => source.MapFrom(_ => false));

        }
    }
}
