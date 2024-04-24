namespace Message.Domain.MessageAggregate;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("RSI")]
public partial class RSI
{
    public int id { get; set; }

    [StringLength(50)]
    public string collection_code { get; set; }

    [StringLength(50)]
    public string shelfmark { get; set; }

    [StringLength(50)]
    public string volume_number { get; set; }

    [StringLength(50)]
    public string storage_location_code { get; set; }

    [StringLength(50)]
    public string author { get; set; }

    [StringLength(50)]
    public string title { get; set; }

    public DateTime? publication_date { get; set; }

    public DateTime? periodical_date { get; set; }

    [StringLength(50)]
    public string article_line1 { get; set; }

    [StringLength(50)]
    public string article_line2 { get; set; }

    [StringLength(50)]
    public string catalogue_record_url { get; set; }

    [StringLength(50)]
    public string further_details_url { get; set; }

    [StringLength(50)]
    public string dt_required { get; set; }

    [StringLength(50)]
    public string route { get; set; }

    [StringLength(50)]
    public string reading_room_staff_area { get; set; }

    [StringLength(50)]
    public string seat_number { get; set; }

    [StringLength(50)]
    public string reading_category { get; set; }

    [StringLength(50)]
    public string identifier { get; set; }

    [StringLength(50)]
    public string reader_name { get; set; }

    public int? reader_type { get; set; }

    [StringLength(50)]
    public string operator_information { get; set; }

    [StringLength(50)]
    public string item_identity { get; set; }
}
