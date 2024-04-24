namespace Message.Domain.MessageAggregate;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Queue")]
public partial class Queue
{
    [Key]
    [Column(Order = 0)]
    public int id { get; set; }

    public int? msg_target { get; set; }

    public int? prty { get; set; }

    public int? type { get; set; }

    public DateTime? dt_created { get; set; }

    [Key]
    [Column(Order = 1)]
    public bool is_acknowledged { get; set; }

    public virtual Common Common { get; set; }

    public virtual messageTypeLookup messageTypeLookup { get; set; }
}
