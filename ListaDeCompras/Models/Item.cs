using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ListaDeCompras.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [MaxLength(100, ErrorMessage = "No máximo 100 caracteres")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é requerido")]
        public string Descricao { get; set; }

        [DisplayName("Preço")]
        public decimal Preco { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "O campo {0} é requerido")]
        public string Categoria { get; set; }

        [DisplayName("Data de Compra:")]
        public DateTime DataDeCompra { get; set; }
      
        public string Imagem { get; set; }

        public byte[] ImagemDB { get; set; }

    }
}