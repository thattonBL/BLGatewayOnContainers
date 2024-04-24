using Message.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Domain.MessageAggregate;

public class RsiMessage : Entity
{
    //Add private RSI Message properties
    private string _collection_code;
    private string _shelfmark;
    private string _volume_number;
    private string _storage_location_code;
    private string _author;
    private string _title;
    private DateTime _publication_date;
    private DateTime _periodical_date;
    private string _article_line1;
    private string _article_line2;
    private string _catalogue_record_url;
    private string _further_details_url;
    private string _dt_required;
    private string _route;
    private string _reading_room_staff_area;
    private string _seat_number;
    private string _reading_category;
    private string _identifier;
    private string _reader_name;
    private int _reader_type;
    private string _operator_information;
    private string _item_identity;

    private bool _isDraft;

    public static RsiMessage NewDraft()
    {
        var message = new RsiMessage();
        message._isDraft = true;
        return message;
    }

    protected RsiMessage() {
        _collection_code = String.Empty;
        _shelfmark = String.Empty;
        _volume_number = String.Empty;
        _storage_location_code = String.Empty;
        _author = String.Empty;
        _title = String.Empty;
        _publication_date = new DateTime();
        _periodical_date = new DateTime();
        _article_line1 = String.Empty;
        _article_line2 = String.Empty;
        _catalogue_record_url = String.Empty;
        _further_details_url = String.Empty;
        _dt_required = String.Empty;
        _route = String.Empty;
        _reading_room_staff_area = String.Empty;
        _seat_number = String.Empty;
        _reading_category = String.Empty;
        _identifier = String.Empty;
        _reader_name = String.Empty;
        _reader_type = 0;
        _operator_information = String.Empty;
        _item_identity = String.Empty;
    }

    public RsiMessage(string collection_code, string shelfmark, string volume_number, string storage_location_code, string author, string title, DateTime publication_date, DateTime periodical_date, string article_line1, string article_line2, string catalogue_record_url, string further_details_url, string dt_required, string route, string reading_room_staff_area, string seat_number, string reading_category, string identifier, string reader_name, int reader_type, string operator_information, string item_identity)
    {
        _collection_code = collection_code;
        _shelfmark = shelfmark;
        _volume_number = volume_number;
        _storage_location_code = storage_location_code;
        _author = author;
        _title = title;
        _publication_date = publication_date;
        _periodical_date = periodical_date;
        _article_line1 = article_line1;
        _article_line2 = article_line2;
        _catalogue_record_url = catalogue_record_url;
        _further_details_url = further_details_url;
        _dt_required = dt_required;
        _route = route;
        _reading_room_staff_area = reading_room_staff_area;
        _seat_number = seat_number;
        _reading_category = reading_category;
        _identifier = identifier;
        _reader_name = reader_name;
        _reader_type = reader_type;
        _operator_information = operator_information;
        _item_identity = item_identity;
    }
}
