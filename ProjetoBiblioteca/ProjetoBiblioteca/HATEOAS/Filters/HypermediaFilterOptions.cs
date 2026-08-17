using projetobiblioteca.HATEOAS.Abstract;

namespace projetobiblioteca.HATEOAS.Filters
{
    public class HypermediaFilterOptions
    {
        public List<IResponseEnricher>
            ContentResponseEnricherList
        { get; set; } = [];
    }
}
