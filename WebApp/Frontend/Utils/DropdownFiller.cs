using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using System.Linq;

namespace WebApp.Frontend.Utils
{
    internal static class DropdownFiller
    {
        internal static List<SelectListItem> FillStationsDropdown(IEnumerable<StationDto> stations)
        {
            var stationList = stations.Select(i =>
                new SelectListItem
                {
                    Value = i.Name,
                    Text = i.Name
                }).ToList();

            if (stationList == null)
            {
                throw new ArgumentNullException($"List {nameof(stations)} is empty.");
            }

            return stationList;
        }

        internal static List<SelectListItem> FillTrainIdsDropdown(IEnumerable<TrainDto> trains)
        {
            var trainList = trains.Select(i =>
                new SelectListItem
                {
                    Value = i.TrainId.ToString(),
                    Text = i.TrainId.ToString()
                }).ToList();

            if (trainList == null)
            {
                throw new ArgumentNullException($"List {nameof(trains)} is empty.");
            }

            return trainList;
        }
    }
}
