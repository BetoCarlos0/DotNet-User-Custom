using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserCustom.Areas.Identity.Data;

namespace UserCustom.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<UserCustomUser> _userManager;
        private readonly SignInManager<UserCustomUser> _signInManager;

        public IndexModel(
            UserManager<UserCustomUser> userManager,
            SignInManager<UserCustomUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            //adicionando campos para input ao BD
            [Required]
            [DataType(DataType.Text)]
            [StringLength(20, ErrorMessage = "O {0} deve ter pelo menos {2}", MinimumLength = 3)]
            [Display(Name = "Nome de Usuário")]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
            [StringLength(20, ErrorMessage = "O {0} deve ter pelo menos {2}", MinimumLength = 3)]
            [Display(Name = "Nome")]
            public string FirstName { get; set; }

            [DataType(DataType.Text)]
            [StringLength(20, ErrorMessage = "O {0} deve ter pelo menos {2}", MinimumLength = 3)]
            [Display(Name = "Sobrenome")]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = "Telefone")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(UserCustomUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                //captura os valores dos inputs
                Name = user.Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não é possível carregar o usuário com ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não é possível carregar o usuário com ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Erro inesperado ao tentar definir o número de telefone.";
                    return RedirectToPage();
                }
            }
             //verificando se os campos foram atualizados, caso ocorra, será feito update
            if (Input.Name != user.Name)
            {
                user.Name = Input.Name;
            }
            if(Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }
            if(Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            //update dos valores alterados ou não
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Sua conta foi editada com sucesso";
            return RedirectToPage();
        }
    }
}
