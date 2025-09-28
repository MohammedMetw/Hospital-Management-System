using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface IAIChatService
    {
        Task<string> GetTriageSuggestion(string patientSymptoms);
    }
}
