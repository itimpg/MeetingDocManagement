namespace MeetingDoc.Api.ViewModels.Criterias
{
    public class MeetingScheduleCriteria : BaseCriteria<MeetingScheduleViewModel>
    {
        public int MeetingTypeId { get; set; }
        public int MeetingTopicId { get; set; }
    }
}