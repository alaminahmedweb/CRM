using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class VoucherEntryDetailsVM
    {
        public int? Id { get; set; }
        public string AcCode { get; set; } = "";
        public string? AcDesc { get; set; } = "";
        public string? Narration { get; set; } = "";
        public double Debit { get; set; } = 0;
        public double Credit { get; set; } = 0;

        

    }
}

