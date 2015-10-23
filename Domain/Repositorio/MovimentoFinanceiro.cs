using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    [Table("MovimentoFinanceiro")]
    public class MovimentoFinanceiro
    {
        [Key]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo categoria é obrigatório")]
        public string Categoria { get; set; }
        [Required(AllowEmptyStrings=false,ErrorMessage="O campo valor é obrigatório")]
        public decimal Valor { get; set; }
        [Required(AllowEmptyStrings=false,ErrorMessage="O Campo data movimentação é obrigatório")]
        public DateTime DataMovimentacao { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "O Campo data cadastro é obrigatório")]
        public DateTime DataCadastro { get; set; }
    }
}
