using Microsoft.AspNetCore.Identity;
using neobooru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace neobooru.ViewModels.Forms
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime RegisteredOn { get; set; }

        public string[] UserRoles { get; set; }

        // TODO: Change to string (unique role name)
        public List<UserRoleCheckboxViewModel> AvailableRoles { get; set; }

        public EditUserViewModel(NeobooruUser usr, string[] usrRoles, IQueryable<IdentityRole> availableRoles)
        {
            Id = usr.Id;
            Username = usr.UserName;
            Email = usr.Email;
            RegisteredOn = usr.RegisteredOn;
            UserRoles = usrRoles;
            AvailableRoles = new List<UserRoleCheckboxViewModel>();
            foreach (var role in availableRoles)
                AvailableRoles.Add(new UserRoleCheckboxViewModel(role.Id, role.Name, UserRoles.Contains(role.Name)));
        }

        public EditUserViewModel()
        {

        }
    }
}
