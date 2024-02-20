using System.ComponentModel.DataAnnotations;

namespace CorpImage.Models
{
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
    }
}
