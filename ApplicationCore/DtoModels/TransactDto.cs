using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class TransactDto
    {
        public List<TransactDetailsDto> TransactionDetails { get; set; } = new List<TransactDetailsDto>();
        public byte[]? Attachment { get; set; }
        public string? AttachmentFileName { get; set; }
        public string? AttachmentContentType { get; set; }
        public string? AttachmentDescription { get; set; }


    }
}
