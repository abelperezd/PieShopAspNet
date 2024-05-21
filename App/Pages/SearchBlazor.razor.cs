using PieShop.Models;
using Microsoft.AspNetCore.Components;

namespace PieShop.App.Pages
{
    //This needs to be partial in order to be detected as a Blazor component
    public partial class SearchBlazor
    {
        public string SearchText = "";
        public List<Pie> FilteredPies { get; set; } = new List<Pie>();

        [Inject] //Equivalent for dependency injection in Blazor
        public IPieRepository? PieRepository { get; set; }

        private void Search()
        {
            if (PieRepository is null)
                return;
            FilteredPies.Clear();

            if (SearchText.Length >= 3)
                FilteredPies = PieRepository.SearchPies(SearchText).ToList();
        }
    }
}
