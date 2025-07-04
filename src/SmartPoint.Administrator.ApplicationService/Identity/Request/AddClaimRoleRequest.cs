using System.ComponentModel.DataAnnotations;

namespace SmartPoint.Administrator.ApplicationService.Identity.Request
{
    public struct AddClaimRoleRequest
    {
        [Required(ErrorMessage = "Informe a role.")]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "Informe o nome da role.")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Informe o tipo da claim.")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        public string ClaimType { get; set; }

        [Required(ErrorMessage = "Informe o valor da claim.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        public string ClaimValue { get; set; }
    }
}
