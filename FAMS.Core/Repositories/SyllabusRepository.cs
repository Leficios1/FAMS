using FAMS.Api.Repositories;
using FAMS.Core.Databases;
using FAMS.Core.Repositories.Interfaces;
using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Core.Repositories
{
    public class SyllabusRepository : BaseRepository<Syllabus>, ISyllabusRepo
    {
        private readonly FamsContext _context;

        public SyllabusRepository(FamsContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Syllabus>> SearchByCodeAsync(string searchTerm)
        {
            searchTerm = searchTerm.ToUpper();

            var result = await _context.Syllabuses
                .Where(s => s.SyllabusCode.ToUpper().Contains(searchTerm))
                .ToListAsync();
            result = result.OrderByDescending(s => s.Version).Take(1).ToList();
            return result;
        }
        public async Task<IEnumerable<Syllabus>> SearchByNameAsync(string searchTerm)
        {
            searchTerm = searchTerm.ToUpper();

            var result = await _context.Syllabuses
                .Where(s => s.SyllabusName.ToUpper().Contains(searchTerm))
                .ToListAsync();
            result = result.OrderByDescending(s => s.Version).Take(1).ToList();
            return result;
        }
        //
        public async Task<IEnumerable<Syllabus>> SearchSyllabusByName(string? name = null)
        {
            var list = await _context.Syllabuses.ToListAsync();
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(s => s.SyllabusName.ToUpper().Trim().Contains(name.ToUpper().Trim())).Take(7).ToList();
            }
            return list;
        }
        //
        public async Task<IEnumerable<Syllabus>> SearchSyllabusByCode(string? code = null)
        {
            var list = await _context.Syllabuses.ToListAsync();
            if (!string.IsNullOrEmpty(code))
            {
                list = list.Where(s => s.SyllabusCode.ToUpper().Contains(code.ToUpper().Trim())).Take(7).ToList();
            }
            return list;
        }
        public async Task<int> GetSyllabusIdBySyllabusCodeAndVersion(string code, string version)
        {
            var syllabus = await _context.Syllabuses
                .Where(s => s.SyllabusCode.ToUpper().Equals(code.ToUpper().Trim()) &&
                            s.Version.ToUpper().Equals(version.ToUpper().Trim()))
                .Select(s => s.Id)
                .FirstOrDefaultAsync();
            return syllabus;
        }
        public async Task<Syllabus> GetSyllabusByIdAsync(int id)
        {
            var syllabus = await _context.Syllabuses.Where(s => s.Id == id).SingleOrDefaultAsync();
            return syllabus;
        }

        public async Task<int> GetIdByNameAsync(string name)
        {
            var entity = await _context.Set<Syllabus>().FirstOrDefaultAsync(e => e.SyllabusName == name);
            return entity?.Id ?? -1;
            //Return -1 if not found
        }

        public async Task<string> GetMaxVersionBySyllabusCode(string code)
        {
            var maxVersion = await _context.Syllabuses
                .Where(s => s.SyllabusCode == code)
                .MaxAsync(s => s.Version);
            return maxVersion;
        }

        public async Task<int> GetIdBySyllabusNameAndMaxVersion(string name)
        {
            var syllabus = await _context.Syllabuses
                .Where(s => s.SyllabusName.ToUpper().Equals(name.ToUpper()))
                .OrderByDescending(s => s.Version)
                .FirstOrDefaultAsync();
            if (syllabus == null)
            {
                return -1;
            }
            return syllabus.Id;
        }
    }
}
