﻿using ContentProccessService.Entities;
using ContentProccessService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContentProccessService.Application.Queries.GetTaskViewTrendMonth
{
    public class GetTaskViewTrendMonthHandler : IRequestHandler<GetTaskViewTrendMonthRequest, List<TaskTrendViewStaicModel>>
    {
        private readonly ContentoDbContext _context;
        public GetTaskViewTrendMonthHandler(ContentoDbContext contentodbContext)
        {
            _context = contentodbContext;
        }
        public async Task<List<TaskTrendViewStaicModel>> Handle(GetTaskViewTrendMonthRequest request, CancellationToken cancellationToken)
        {
            var lstTask = await _context.Statistics.Include(x => x.IdTaskNavigation).ThenInclude(IdTaskNavigation => IdTaskNavigation.Contents).Where(x => x.CreatedDate >= DateTime.UtcNow.AddMonths(-1) && x.CreatedDate < DateTime.UtcNow).ToListAsync();
            var lstTaskView = new List<TaskTrendViewStaicModel>();
            foreach (var item in lstTask)
            {
                if (lstTaskView.Any(x => x.IdTask == item.IdTaskNavigation.Id))
                {
                    lstTaskView.Where(x => x.IdTask == item.IdTaskNavigation.Id).FirstOrDefault().View += item.Views ?? 0;
                }
                else
                {
                    var taskView = new TaskTrendViewStaicModel();
                    taskView.Title = item.IdTaskNavigation.Contents.FirstOrDefault(x => x.IsActive == true).Name;
                    taskView.View += item.Views ?? 0;
                    taskView.IdTask = item.IdTaskNavigation.Id;
                    lstTaskView.Add(taskView);
                }
            }
            return lstTaskView.OrderByDescending(x => x.View).Take(20).ToList();
        }
    }
}
