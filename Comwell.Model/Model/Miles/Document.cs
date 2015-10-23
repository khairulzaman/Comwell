namespace Comwell.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            Revisions = new HashSet<Revision>();
        }

        [Key]
        [Column("document_id")]
        public Guid DocumentId { get; set; }

        [Column("title", TypeName = "text")]
        public string Title { get; set; }

        [StringLength(100)]
        [Column("number")]
        public string Number { get; set; }

        [Column("timestamp_update", TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Column("timestamp_id")]
        public Guid TimestampId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Revision> Revisions { get; set; }
    }
}
