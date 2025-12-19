namespace Web.ViewModels
{
    public class VoucherAttachmentVM
    {
        public int Id { get; set; }
        public string VoucherNo { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; }
        public string? UploadedBy { get; set; }
        public string FileBase64 { get; set; } // For displaying in view
    }
}
