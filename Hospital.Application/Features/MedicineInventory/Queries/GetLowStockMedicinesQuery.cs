using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Queries
{
    public class GetLowStockMedicinesQuery : IRequest<IEnumerable<MedicineInventoryDto>>
    {

        public int lowStockThreshold { get; set; }
    }
}
