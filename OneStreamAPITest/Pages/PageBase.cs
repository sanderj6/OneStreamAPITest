using Microsoft.AspNetCore.Components;

namespace OneStreamAPITest.Pages
{
    public class PageBase<TPage> : ComponentBase
    {
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public ILogger<TPage> Logger { get; set; }

        public void OnNavigationClicked(string destination)
        {
            NavManager.NavigateTo(destination);
        }
    }

}
