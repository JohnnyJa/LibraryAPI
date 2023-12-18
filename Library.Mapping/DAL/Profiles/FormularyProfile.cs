using AutoMapper;
using Library.BLL.Requests.Formulary;
using Library.BLL.Responses.Formulary;
using Library.DAL.Entities;

namespace Library.Mapping.DAL.Profiles;

public class FormularyProfile : Profile
{
    public FormularyProfile()
    {
        CreateMap<ReaderFormulary, FormularyResponse>();
        CreateMap<CreateFormularyRequest, ReaderFormulary>();
        CreateMap<UpdateFormularyRequest, ReaderFormulary>();

    }
}