namespace Whatsapp.Domain.Enums;

public enum MessageStatus
{
    // 0 is used for filter ALL
    Sending = 1,
    Sent,
    Delivered,
    Read,
    Failed
}