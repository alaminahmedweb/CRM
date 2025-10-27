using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class VoucherEntryVM
    {       
        public List<VoucherEntryDetailsVM>? VoucherEntryDetailsVM { get; set; }

        public VoucherEntryMasterVM VoucherEntryMasterVM { get; set; }
    }
}
