using System.ComponentModel.DataAnnotations;

namespace SmartPoint.Administrator.ApplicationService.Identity.Request
{
    public struct AddClaimUserRequest
    {
        [Required(ErrorMessage = "Informe o usuário.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Informe o tipo da claim.")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        public string ClaimType { get; set; }

        [Required(ErrorMessage = "Informe o valor da claim.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        public string ClaimValue { get; set; }
    }
}
