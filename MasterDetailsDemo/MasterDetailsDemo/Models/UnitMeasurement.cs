using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterDetailsDemo.Models
{
    public class UnitMeasurement
    {
        [Key]
        [StringLength(36)]
        public string UnitMeasurementId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
        
    }
}