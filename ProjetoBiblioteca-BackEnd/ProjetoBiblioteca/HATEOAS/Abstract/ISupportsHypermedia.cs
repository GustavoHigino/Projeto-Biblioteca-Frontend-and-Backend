using projetobiblioteca.HATEOAS.Filters;

namespace projetobiblioteca.HATEOAS.Abstract
{
    public interface ISupportsHypermedia
    {
        List<HypermediaLink> Links { get; set; }
    }
}
