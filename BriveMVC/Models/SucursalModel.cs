using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BriveMVC.Models
{
    public class SucursalModel
    {
        [Key]        
        public int IdSucursal { get; set; }
        [StringLength(50, MinimumLength = 2)]
        [Required(ErrorMessage = "Ingrese nombre de la sucursal")]
        [Display(Name = "Nombre de la Sucursal")]
        public string NombreSucursal { get; set; }
        [StringLength(50, MinimumLength = 2)]
        [Required(ErrorMessage = "Ingrese la dirección")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [StringLength(10, MinimumLength = 10)]
        [Required(ErrorMessage = "Ingrese el número de teléfono")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

    }
}
