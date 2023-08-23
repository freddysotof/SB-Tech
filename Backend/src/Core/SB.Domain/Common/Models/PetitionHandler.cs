using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain.Common.Models
{
    public class PetitionHandler<T> where T : class
    {
        [Required(ErrorMessage = "Debe proveer los datos de la petición")]
        public T Data { get; set; }

        public DateTime ReceivedDate => DateTime.Now;

        public PetitionHandler()
        {
        }

        public PetitionHandler(T data)
        {
            Data = data;
        }
    }
}
