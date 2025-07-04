using System.ComponentModel.DataAnnotations;

namespace SmartPoint.Administrator.ApplicationService.Identity.Request
{
    public struct RoleRegisterRequest
    {
        [Required(ErrorMessage = "Informe o nome da Role.")]
        [StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        public string Name { get; set; }
    }
}
