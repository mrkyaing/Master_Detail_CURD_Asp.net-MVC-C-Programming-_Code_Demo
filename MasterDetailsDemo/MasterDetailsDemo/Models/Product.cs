using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MasterDetailsDemo.Models
{
    public class Product
    {
        [Key]
        [StringLength(36)]
        public string ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        
        [StringLength(36)]
        [ForeignKey("UnitMeasurement")]
        public string UnitMeasurementId { get; set; }
        public virtual UnitMeasurement UnitMeasurement { get; set; }
    }
}