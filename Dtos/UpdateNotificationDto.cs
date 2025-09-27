namespace NotificationsApi.Dtos
{
    public class UpdateNotificationDto
    {
        public int NotificationMessageID { get; set; }  // Needed for update

        public string NotificationChannel { get; set; }
        public string NotificationHeading { get; set; }
        public string NotificationBody { get; set; }
        public string NotificationFooter { get; set; }
        public string NotificationSubject { get; set; }
        public int? RepeatEvery { get; set; }
        public int? NoOfTimesToRepeat { get; set; }
        public string RepeatNotification { get; set; }
        public string? UseDocumentTemplate { get; set; }
        public int? DocumentTemplateID { get; set; }
    }
}
