using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }

        public string Local { get; set; }

        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        //MinLength(3, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres."),
        //MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")
        StringLength(50, MinimumLength = 3, ErrorMessage = "O campo {0} deve ter no mínimo {2} e no máximo {1} caracteres.")]
        public string Tema { get; set; }

        [Range(1, 120000, ErrorMessage = "O campo {0} deve ter valor entre {1} e {2}.")]
        public int QuantidadePessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Formato de arquivo inválido. Formatos permitidos: gif, jpg, bmp e png.")]
        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        Phone(ErrorMessage = "O formato do campo {0} está inválido.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        Display(Name = "E-mail"),
        EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }

        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }

        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}