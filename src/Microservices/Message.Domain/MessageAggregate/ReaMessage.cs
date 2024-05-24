using Message.Domain.SeedWork;

namespace Message.Domain.MessageAggregate;


public class ReaMessage : Entity
{
    //Add private Rea Message properties
    private string _dt_of_action;
    private string _request_response_flag;
    private string _failure_code;
    private int _container_id;
    private string _text_message;
    private string _stack_identity;
    private string _tray_identity;
    private bool _isDraft;
    public static ReaMessage NewDraft()
    {
        var message = new ReaMessage();
        message._isDraft = true;
        return message;
    }
    protected ReaMessage()
    {
        _dt_of_action = String.Empty;
        _request_response_flag = String.Empty;
        _failure_code = String.Empty;
        _container_id = 0;
        _text_message = String.Empty;
        _stack_identity = String.Empty;
        _tray_identity = String.Empty;
    }

    public ReaMessage(string dt_of_action, string request_response_flag, string failure_code, int container_id, string text_message, string stack_identity, string tray_identity)
    {
        _dt_of_action = dt_of_action;
        _request_response_flag = request_response_flag;
        _failure_code = failure_code;
        _container_id = container_id;
        _text_message = text_message;
        _stack_identity = stack_identity;
        _tray_identity = tray_identity;
    }
}