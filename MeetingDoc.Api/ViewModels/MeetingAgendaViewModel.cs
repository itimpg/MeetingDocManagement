using System.Collections.Generic;

namespace MeetingDoc.Api.ViewModels
{
    public class MeetingAgendaViewModel : BaseViewModel
    {
        public MeetingAgendaViewModel()
        {
            Users = new List<MeetingAgendaUserViewModel>();
        }

        public int MeetingTimeId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public IList<MeetingAgendaUserViewModel> Users { get; set; }
    }
}