using Microsoft.AspNetCore.Mvc;
using projetobiblioteca.HATEOAS.Contants;
using projetobiblioteca.HATEOAS.Filters;
using projetobiblioteca.HATEOAS.Utilis;

using projetobiblioteca.Model;

namespace projetobiblioteca.HATEOAS.Enricher
{
    public class AlunoEnricher : ContentResponseEnricher<Aluno>

    {
        protected override Task EnrichModel
            (Aluno content, IUrlHelper urlHelper)
        {
            var request = urlHelper
                .ActionContext.HttpContext.Request;

            var baseUrl = $"{request.Scheme}://" +
                $"{request.Host.ToUriComponent()}" +
                $"{request.PathBase.ToUriComponent()}" +
                $"/Aluno";

            content.Links.AddRange(
                GenerateLinks(
                    content.Id, baseUrl));

            return Task.CompletedTask;
        }
        private IEnumerable<HypermediaLink>GenerateLinks
            (long id,string baseUrl)
        {
            return new List<HypermediaLink>
            {
                new()
                {
                    Rel=RelationType.COLECTION,
                    Href=$"{baseUrl}",
                    Type=ResponseTypeFormat.DEFAULTGET,
                    Action=HttpActionVerb.GET
                },
                new()
                {
                    Rel=RelationType.SELF,
                    Href=$"{baseUrl}/{id}",
                    Type=ResponseTypeFormat.DEFAULTGET,
                    Action=HttpActionVerb.GET
                },
                new()
                {
                    Rel=RelationType.CREATE,
                    Href=$"{baseUrl}",
                    Type=ResponseTypeFormat.DEFAULTPOST,
                    Action=HttpActionVerb.POST
                    
                    
                },
                new()
                {
                    Rel=RelationType.UPDATE,
                    Href=$"{baseUrl}",
                    Type=ResponseTypeFormat.DEFAULTPUT,
                    Action=HttpActionVerb.PUT
                   
                },
                new()
                {
                    Rel=RelationType.PATCH,
                    Href=$"{baseUrl}/enable/{id}",
                    Type=ResponseTypeFormat.DEFAULTPATCH,
                    Action=HttpActionVerb.PATCH
                },
                new()
                {
                    Rel=RelationType.PATCH,
                    Href=$"{baseUrl}/disable/{id}",
                    Type=ResponseTypeFormat.DEFAULTPATCH,
                    Action=HttpActionVerb.PATCH
                }
            };
        }
    }
}
