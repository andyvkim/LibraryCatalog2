using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class Hold
    {
        public int Id { get; set; }
        public virtual LibraryAsset LibrayAsset { get; set; }
        public virtual LibraryCard LibraryCard { get; set; }
        public DateTime HoldPlaced { get; set; }
    }
}