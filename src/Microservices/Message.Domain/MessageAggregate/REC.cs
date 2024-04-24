namespace Message.Domain.MessageAggregate;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("REC")]
public partial class REC
{
    public int id { get; set; }

    [StringLength(50)]
    public string dt_of_action { get; set; }

    [StringLength(50)]
    public string request_response_flag { get; set; }

    [StringLength(50)]
    public string failure_code { get; set; }

    [StringLength(50)]
    public string text_message { get; set; }

    [StringLength(50)]
    public string stack_identity { get; set; }

    [StringLength(50)]
    public string tray_identity { get; set; }
}
