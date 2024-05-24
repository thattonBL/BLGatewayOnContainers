namespace Message.Domain.MessageAggregate;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("RIR")]
public partial class RIR
{
    public int id { get; set; }

    [StringLength(50)]
    public string outcome { get; set; }

    [StringLength(50)]
    public string reason { get; set; }
}
