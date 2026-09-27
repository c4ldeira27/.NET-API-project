using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dima.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Título inválido")]
        [MaxLength(ErrorMessage ="O título deve conter até 80 catacteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage ="Descrição inválida")]
        public string Description { get; set; } = string.Empty;
    }
}
