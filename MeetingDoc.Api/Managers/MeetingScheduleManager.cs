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
                .GetQuery(x => x.UserId == criteria.Model.UserId && !x.IsRemoved)
                .Include(x => x.MeetingAgenda)
                .ThenInclude(x => x.MeetingTime)
                .ThenInclude(x => x.MeetingTopic)
                .ThenInclude(x => x.MeetingType)
                .Where(x =>
                    !x.MeetingAgenda.IsDraft
                    && !x.MeetingAgenda.IsRemoved
                    && !x.MeetingAgenda.MeetingTime.IsDraft
                    && !x.MeetingAgenda.MeetingTime.IsRemoved
                    && !x.MeetingAgenda.MeetingTime.MeetingTopic.IsDraft
                    && !x.MeetingAgenda.MeetingTime.MeetingTopic.IsRemoved
                    && !x.MeetingAgenda.MeetingTime.MeetingTopic.MeetingType.IsDraft
                    && !x.MeetingAgenda.MeetingTime.MeetingTopic.MeetingType.IsRemoved)
                .Select(x => new MeetingScheduleViewModel
                {
                    Id = x.MeetingAgenda.MeetingTime.Id,
                    UserId = x.UserId,
                    MeetingType = x.MeetingAgenda.MeetingTime.MeetingTopic.MeetingType.Name,
                    MeetingTopic = x.MeetingAgenda.MeetingTime.MeetingTopic.Name,
                    MeetingTimeCount = x.MeetingAgenda.MeetingTime.Count,
                    MeetingFiscalYear = x.MeetingAgenda.MeetingTime.FiscalYear,
                    MeetingDateTime = x.MeetingAgenda.MeetingTime.MeetingDate,
                    MeetingPlace = x.MeetingAgenda.MeetingTime.Location,
                })
                .OrderByDescending(x => x.MeetingDateTime)
                .Distinct();

            var count = await query.CountAsync();

            var pageNumber = criteria.PageNumber;
            var pageSize = criteria.PageSize;

            var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            var paging = new PagedList<MeetingScheduleViewModel>(
                await query.ToListAsync(),
                count, pageNumber, pageSize);

            return paging;
        }

        public async Task<PagedList<MeetingAgendaViewModel>> GetAgendasAsync(MeetingAgendaCriteria criteria)
        {
            var query = _unitOfWork.MeeitngAgendaUserRepository
                .GetQuery(x => x.UserId == criteria.UserId && !x.IsRemoved)
                .Include(x => x.MeetingAgenda)
                .ThenInclude(x=> x.MeetingContents)
                .Where(x =>
                    !x.MeetingAgenda.IsDraft
                    && x.MeetingAgenda.MeetingContents.Any()
                    && !x.MeetingAgenda.IsRemoved
                    && x.MeetingAgenda.MeetingTimeId == criteria.Model.MeetingTimeId);

            var count = await query.CountAsync();

            var pageNumber = criteria.PageNumber;
            var pageSize = criteria.PageSize;

            var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            var paging = new PagedList<MeetingAgendaViewModel>(
                items.Select(x => new MeetingAgendaViewModel
                {
                    Id = x.MeetingAgendaId,
                    MeetingTimeId = x.MeetingAgenda.MeetingTimeId,
                    Number = x.MeetingAgenda.Number,
                    Name = x.MeetingAgenda.Name,
                })
                .OrderBy(x => x.Number)
                .Distinct()
                .ToList(),
                count, pageNumber, pageSize);

            return paging;
        }
    }
}