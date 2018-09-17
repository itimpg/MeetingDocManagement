using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingDoc.Api.Data.Interfaces;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.ViewModels;
using MeetingDoc.Api.ViewModels.Criterias;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MeetingDoc.Api.Managers
{
    public class MeetingScheduleManager : IMeetingScheduleManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeetingScheduleManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<MeetingScheduleViewModel>> GetAsync(MeetingScheduleCriteria criteria)
        {
            var query = _unitOfWork.MeeitngAgendaUserRepository
                .GetQuery(x => x.UserId == criteria.Model.UserId && !x.IsRemoved);

            var count = await query.CountAsync();

            var pageNumber = criteria.PageNumber;
            var pageSize = criteria.PageSize;

            var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            var paging = new PagedList<MeetingScheduleViewModel>(
                items.Select(x => new MeetingScheduleViewModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    MeetingType = x.MeetingAgenda.MeetingTime.MeetingTopic.MeetingType.Name,
                    MeetingTopic = x.MeetingAgenda.MeetingTime.MeetingTopic.Name,
                    MeetingTimeCount = x.MeetingAgenda.MeetingTime.Count,
                    MeetingFiscalYear = x.MeetingAgenda.MeetingTime.FiscalYear,
                    MeetingDateTime = x.MeetingAgenda.MeetingTime.MeetingDate,
                    MeetingPlace = x.MeetingAgenda.MeetingTime.Location,
                }).ToList(),
                count, pageNumber, pageSize);

            return paging;
        }
    }
}