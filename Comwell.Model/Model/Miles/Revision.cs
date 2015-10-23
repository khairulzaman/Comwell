namespace Comwell.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Revision
    {
        [Key]
        [Column("revision_id")]
        public Guid RevisionId { get; set; }

        [Column("revision")]
        public int Number { get; set; }

        [Column("timestamp_update", TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Column("timestamp_id")]
        public Guid TimestampId { get; set; }

        [Column("document_id")]
        public Guid? DocumentId { get; set; }

        public virtual Document Document { get; set; }
    }
}
