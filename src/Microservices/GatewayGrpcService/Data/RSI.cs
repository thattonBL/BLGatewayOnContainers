using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GatewayGrpcService.Data;

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

    public RSI(string collection_code, string shelfmark, string volume_number, string storage_location_code, string author, string title, DateTime? publication_date, DateTime? periodical_date, string article_line1, string article_line2, string catalogue_record_url, string further_details_url, string dt_required, string route, string reading_room_staff_area, string seat_number, string reading_category, string identifier, string reader_name, int? reader_type, string operator_information, string item_identity)
    {
        this.collection_code = collection_code;
        this.shelfmark = shelfmark;
        this.volume_number = volume_number;
        this.storage_location_code = storage_location_code;
        this.author = author;
        this.title = title;
        this.publication_date = publication_date;
        this.periodical_date = periodical_date;
        this.article_line1 = article_line1;
        this.article_line2 = article_line2;
        this.catalogue_record_url = catalogue_record_url;
        this.further_details_url = further_details_url;
        this.dt_required = dt_required;
        this.route = route;
        this.reading_room_staff_area = reading_room_staff_area;
        this.seat_number = seat_number;
        this.reading_category = reading_category;
        this.identifier = identifier;
        this.reader_name = reader_name;
        this.reader_type = reader_type;
        this.operator_information = operator_information;
        this.item_identity = item_identity;
    }
}
