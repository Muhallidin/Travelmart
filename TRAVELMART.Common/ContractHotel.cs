using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Created By:         Jefferson Bermundo
    /// Date Created:       July 27, 2012
    /// Description:        Class Model For Contract Hotel Attachments
    /// </summary>
    public class ContractHotelAttachment
    {
        public int AttachmentId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public byte[] uploadedFile { get; set; }
        public int colContractId { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
