using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Objects.Model
{
    public interface IFerryable
    {
        public int Id { get; set; } 
        FerryableType FerryType { get; }



    }
}
