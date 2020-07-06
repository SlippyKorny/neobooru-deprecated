using System.Collections.Generic;

namespace neobooru.ViewModels.Forms
{
    public class RoleCheckboxesViewModel
    {
        public string UserId { get; set; }
        
        public List<RoleCheckboxViewModel> Checkboxes { get; set; }

        public RoleCheckboxesViewModel()
        {
            Checkboxes = new List<RoleCheckboxViewModel>();
        }
    }
}
