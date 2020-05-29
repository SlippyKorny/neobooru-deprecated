namespace neobooru.ViewModels.Forms
{
    public class RoleCheckboxViewModel
    {
        public string Role { get; set; }

        public bool Selected { get; set; }

        public RoleCheckboxViewModel()
        {

        }

        public RoleCheckboxViewModel(string name, bool selected)
        {
            Role = name;
            Selected = selected;
        }
    }
}
