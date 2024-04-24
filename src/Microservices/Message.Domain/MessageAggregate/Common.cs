namespace Message.Domain.MessageAggregate;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Common")]
public partial class Common
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Common()
    {
        Queues = new HashSet<Queue>();
    }

    public int id { get; set; }

    [StringLength(50)]
    public string msg_status { get; set; }

    [StringLength(50)]
    public string msg_source { get; set; }

    public int msg_target { get; set; }

    [StringLength(50)]
    public string prty { get; set; }

    public int type { get; set; }

    [StringLength(50)]
    public string ref_source { get; set; }

    [StringLength(50)]
    public string ref_request_id { get; set; }

    public int? ref_seq_no { get; set; }

    public DateTime dt_created { get; set; }

    public virtual messageTypeLookup messageTypeLookup { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Queue> Queues { get; set; }
}
