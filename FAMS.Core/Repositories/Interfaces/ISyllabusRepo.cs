using FAMS.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Repositories.Interfaces
{
    public interface ISyllabusRepo
    {
        Task<IEnumerable<Syllabus>> SearchByCodeAsync(string searchTerm);
        Task<IEnumerable<Syllabus>> SearchByNameAsync(string searchTerm);
        Task<IEnumerable<Syllabus>> SearchSyllabusByCode(string code);
        Task<IEnumerable<Syllabus>> SearchSyllabusByName(string name);
        Task<int> GetSyllabusIdBySyllabusCodeAndVersion(string code, string version);
        Task<Syllabus> GetSyllabusByIdAsync(int id);
        Task<int>GetIdByNameAsync(string name);
        Task<string>GetMaxVersionBySyllabusCode(string code);
        Task<int> GetIdBySyllabusNameAndMaxVersion(string name);
    }
}
