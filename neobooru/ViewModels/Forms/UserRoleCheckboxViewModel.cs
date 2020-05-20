using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace neobooru.ViewModels.Forms
{
    public class UserRoleCheckboxViewModel
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }
    
        public bool IsChecked { get; set; }

        public UserRoleCheckboxViewModel(string roleId, string roleName, bool isChecked)
        {
            RoleId = roleId;
            RoleName = roleName;
            IsChecked = isChecked;
        }

        public UserRoleCheckboxViewModel()
        {

        }
    }
}
