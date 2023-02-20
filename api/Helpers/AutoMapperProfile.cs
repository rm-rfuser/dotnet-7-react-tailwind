namespace WebApi.Helpers;

using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Article;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateRequest -> Article
        CreateMap<CreateRequest, Article>();

        // UpdateRequest -> Article
        CreateMap<UpdateRequest, Article>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    // ignore null role
                    if (x.DestinationMember.Name == "Type" && src.Type == null) return false;

                    return true;
                }
            ));
    }
}