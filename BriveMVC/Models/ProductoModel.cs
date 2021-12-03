using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BriveMVC.Models
{
    public class ProductoModel
    {
        [Key]
        public int IdProducto { get; set; }
        public int IdSucursal { get; set; }
        public string NombreProducto { get; set; }
        public int SKU { get; set; }
    }
}
