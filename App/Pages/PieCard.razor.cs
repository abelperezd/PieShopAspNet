using PieShop.Models;
using Microsoft.AspNetCore.Components;

namespace PieShop.App.Pages
{
    public partial class PieCard
    {
        [Parameter]
        public Pie? Pie { get; set; }
    }
}
